using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace avii.Admin
{
    public partial class AssignEsnNew : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/";
        public int IncreementIndex { get; set; }

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
                IncreementIndex = 2;
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
        protected void rptScanESN_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtESN = (TextBox)e.Item.FindControl("txtESN");
                Label txtICCID = (Label)e.Item.FindControl("txtICCID");
                if (!string.IsNullOrWhiteSpace(txtESN.Text.Trim()))
                {
                    Label lblErrMsg = (Label)e.Item.FindControl("lblErrMsg");
                    if (lblErrMsg.Text == "")
                    {
                        txtESN.Enabled = false;
                        // txtICCID.Enabled = false;

                    }
                }
                List<POTracking> trackingList = new List<POTracking>();
                trackingList = Session["trackingList"] as List<POTracking>;
                DropDownList ddlTrackNo = (DropDownList)e.Item.FindControl("ddlTrackNo");
                HiddenField hdTN = (HiddenField)e.Item.FindControl("hdTN");
                if (ddlTrackNo != null)
                {
                    ddlTrackNo.DataSource = trackingList;
                    ddlTrackNo.DataTextField = "TrackingNumber";
                    ddlTrackNo.DataValueField = "TrackingNumber";
                    ddlTrackNo.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
                    ddlTrackNo.Items.Insert(0, item);

                    if (hdTN.Value != "")
                        ddlTrackNo.SelectedValue = hdTN.Value;
                    else if (trackingList.Count == 1)
                        ddlTrackNo.SelectedIndex = 1;
                }



            }
        }
        protected void lnkPoNum_OnCommand(object sender, CommandEventArgs e)
        {
            string values = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(values))
            {
                string[] arr = values.Split(',');
                string poNum = arr[0];
                string companyAccountNumber = arr[1];
                Control tmp1 = LoadControl("../controls/PODetails.ascx");
                avii.Controls.PODetails ctlPODetails = tmp1 as avii.Controls.PODetails;
                pnlPO.Controls.Clear();
                ctlPODetails.BindPO(poNum, companyAccountNumber);

                pnlPO.Controls.Add(ctlPODetails);

                RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



            }

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
            //rptESN.DataSource = null;
            //rptESN.DataBind();
            //to Bind ESN csv file to grid
            UploadInventoryInfo();
            rptESN.Visible = true;


        }
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            string ESN = string.Empty, ICCID = string.Empty;
            int itemCompanyGUID = 0;
            bool isESNToValidate = true;
            ESNAssignment esnAssignment = new ESNAssignment();
            FulfillmentAssignESN assignESN = null;
            List<FulfillmentAssignESN> esnlist = new List<FulfillmentAssignESN>();
            string customerAccountNumber = string.Empty;
            string poNum = txtPO.Text.Trim();
            int emptyESNs = 0;
            Hashtable hshESN = new Hashtable();
            string previoustrackingNumber = string.Empty, trackingNumber = string.Empty;
            int trackingNumberCount = 0;
            if (ViewState["trackingNumberCount"] != null)
                trackingNumberCount = Convert.ToInt32(ViewState["trackingNumberCount"]);
            bool IsMultiTrackingNumber = false;

            if (dpCompany.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(poNum))
                {
                    customerAccountNumber = dpCompany.SelectedValue;
                    esnAssignment.CustomerAccountNumber = customerAccountNumber;
                    esnAssignment.FulfillmentNumber = poNum;

                    foreach (RepeaterItem item in rptScanESN.Items)
                    {
                        TextBox txtESN = item.FindControl("txtESN") as TextBox;
                        DropDownList ddlTrackNo = item.FindControl("ddlTrackNo") as DropDownList;
                        //if(ddlTrackNo.SelectedIndex==0)
                        //{
                        //    lblMsg.Text = "Please select tracking number";
                        //    ddlTrackNo.Focus();
                        //    return;
                        //}
                        trackingNumber = ddlTrackNo.SelectedValue;
                        ESN = txtESN.Text.Trim();
                        Label txtICCID = item.FindControl("txtICCID") as Label;
                        ESN = txtESN.Text.Trim();

                        ICCID = txtICCID.Text.Trim();
                        if (hshESN.ContainsKey(ESN) && !string.IsNullOrEmpty(ESN))
                        {
                            //uploadEsn = false;
                            lblMsg.Text = "Duplicate ESN(s) exists";
                            return;
                            //throw new ApplicationException("Duplicate ESN(s) exists in the file");
                        }
                        else if (!hshESN.ContainsKey(ESN) && !string.IsNullOrEmpty(ESN))
                        {
                            hshESN.Add(ESN, ESN);
                        }
                        if (trackingNumberCount > 1)
                        {
                            IsMultiTrackingNumber = true;
                            if (!string.IsNullOrWhiteSpace(previoustrackingNumber))
                                if (previoustrackingNumber != trackingNumber)
                                    IsMultiTrackingNumber = false;
                        }
                        previoustrackingNumber = trackingNumber;

                        //if (!string.IsNullOrWhiteSpace(ESN))
                        {

                            assignESN = new FulfillmentAssignESN();
                            HiddenField hdSKUID = item.FindControl("hdSKUID") as HiddenField;
                            int.TryParse(hdSKUID.Value, out itemCompanyGUID);
                            assignESN.ESN = ESN;
                            assignESN.LteICCID = ICCID;
                            assignESN.ItemCompanyGUID = itemCompanyGUID;
                            assignESN.TrackingNumber = trackingNumber;

                            esnlist.Add(assignESN);
                        }
                    }
                    if (IsMultiTrackingNumber)
                    {
                        lblMsg.Text = "Multi Tracking numbers found but assigned only one!";
                        return;
                    }
                    if (esnlist.Count > 0)
                    {
                        esnAssignment.EsnList = esnlist;
                        List<FulfillmentAssignESN> esnList = avii.Classes.ESNAssignOperation.ValidateAssignESNScanNew(esnAssignment);
                        rptScanESN.DataSource = esnList;
                        Session["esns"] = esnList;

                        rptScanESN.DataBind();

                        foreach (RepeaterItem item in rptScanESN.Items)
                        {
                            TextBox txtESN = item.FindControl("txtESN") as TextBox;
                            ESN = txtESN.Text.Trim();
                            if (!string.IsNullOrWhiteSpace(ESN))
                            {
                                emptyESNs += emptyESNs;
                            }
                            {
                                if (txtESN.Enabled)
                                    isESNToValidate = false;
                            }
                        }
                        if (isESNToValidate && emptyESNs == 0)
                        {
                            btnValidate.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        else
                            if (emptyESNs > 0)
                        {
                            lblMsg.Text = "Missing " + emptyESNs + " ESN(s)";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please enter ESN(s) to validate!";
                    }
                }
                else
                    lblMsg.Text = "Please enter Fulfillment number!";

            }
            else
                lblMsg.Text = "Please select customer!";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            dpCompany.SelectedIndex = 0;
            txtPO.Text = string.Empty;
            CalearForm();
        }
        private void CalearForm()
        {

            rptScanESN.DataSource = null;
            rptScanESN.DataBind();
            rptESN.DataSource = null;
            rptESN.DataBind();
            btnValidate.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            btnCancel.Visible = false;
            btnCancel2.Visible = false;
            btnSubmit.Visible = false;
            btnUpload.Visible = false;
            lnkDownload.Visible = false;
            row1.Visible = false;
            row2.Visible = false;
            row3.Visible = false;

            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            btnSearch.Visible = true;
            btnViewAssignedESN.Visible = false;
            lblNonEsn.Text = string.Empty;
            lblEsn.Text = string.Empty;
            rptSKU.DataSource = null;
            rptSKU.DataBind();
            rptSKUESN.DataSource = null;
            rptSKUESN.DataBind();

        }

        protected void btnViewAssignedESN_Click(object sender, EventArgs e)
        {
            if (Session["esnsList"] != null)
            {
                List<FulfillmentAssignESN> esnsList = Session["esnsList"] as List<FulfillmentAssignESN>;
                rptESN.DataSource = esnsList;
                rptESN.DataBind();
                lblCount.Text = "Total count: " + esnsList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            string fulfillmentNumber = txtPO.Text.Trim();
            ESNAssignment esnAssignment = new ESNAssignment();
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            esnAssignment.CustomerAccountNumber = dpCompany.SelectedValue;
            esnAssignment.FulfillmentNumber = fulfillmentNumber;
            if (Session["esns"] != null || Session["nonesns"] != null)
            {
                List<FulfillmentAssignNonESN> nonEsnList = new List<FulfillmentAssignNonESN>();
                if (Session["nonesns"] != null)
                {
                    nonEsnList = Session["nonesns"] as List<FulfillmentAssignNonESN>;
                    nonEsnList = (from item in nonEsnList where item.IsAssign.Equals(true) select item).ToList();
                }
                List<FulfillmentAssignESN> esnsList = new List<FulfillmentAssignESN>();
                List<FulfillmentAssignESN> esnToAdd = null;
                List<FulfillmentAssignESN> esnList = null;
                if (Session["esns"] != null)
                    esnList = Session["esns"] as List<FulfillmentAssignESN>;
                int n = 0;
                int poRecordCount = 0;
                string poErrorMessage = string.Empty;
                string invalidESNMessage = string.Empty;
                string invalidSkuESNMessage = string.Empty;
                string esnExistsMessage = string.Empty;

                double totalChunk = 0;
                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;

                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        if (con != null && con.State == ConnectionState.Closed)
                            con.Open();
                        SqlTransaction sqlTrans = con.BeginTransaction();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = sqlTrans;
                        try
                        {
                            int returnValue = ESNAssignOperation.AssignNonEsnItemToFulfillmentOrder(nonEsnList, fulfillmentNumber, userID, esnAssignment.CustomerAccountNumber, con, sqlTrans, cmd);

                            if (esnList != null && esnList.Count > 0)
                            {
                                totalChunk = (double)esnList.Count / 10000;
                                n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                int esnCount = 10000;
                                string errorMessage = string.Empty;
                                //var esnToUpload;
                                for (int i = 0; i < n; i++)
                                {
                                    invalidESNMessage = invalidSkuESNMessage = esnExistsMessage = string.Empty;
                                    esnToAdd = new List<FulfillmentAssignESN>();
                                    if (esnList.Count < 10000)
                                        esnCount = esnList.Count;
                                    var esnToUpload = (from item in esnList.Take(esnCount) select item).ToList();

                                    esnAssignment.EsnList = esnToUpload;

                                    // esnAssignment.NonEsnList = nonEsnList;
                                    //Upload/Assign ESN to POs
                                    esnToAdd = avii.Classes.ESNAssignOperation.AssignEsnToFulfillmentOrder(esnAssignment, userID, "U", con, sqlTrans, cmd, out poRecordCount, out invalidESNMessage, out invalidSkuESNMessage, out esnExistsMessage);
                                    if (!string.IsNullOrEmpty(invalidESNMessage))
                                    {
                                        errorMessage = invalidESNMessage + " ESN(s) does not exists!";
                                    }
                                    if (!string.IsNullOrEmpty(invalidSkuESNMessage))
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";

                                    }
                                    if (!string.IsNullOrEmpty(esnExistsMessage))
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = esnExistsMessage + " ESN(s) are already assigned!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + esnExistsMessage + " ESN(s) are already assigned!";

                                    }
                                    if (!string.IsNullOrEmpty(errorMessage))
                                    {
                                        lblConfirm.Text = errorMessage;
                                        return;
                                    }

                                    //string poXML = clsGeneral.SerializeObject(esnToUpload);
                                    if (i != 0)
                                        poRecordCount += poRecordCount;
                                    if (esnToAdd != null)
                                    {
                                        esnsList.AddRange(esnToAdd);

                                        esnList.RemoveRange(0, esnCount);
                                    }
                                }
                            }
                            poRecordCount = poRecordCount + returnValue;
                            if (poRecordCount > 0)
                            {
                                lblMsg.Text = "Updated successfully <br /> Record count: " + poRecordCount;
                                pnlSubmit.Visible = false;
                                //esnsList = Session["esns"] as List<FulfillmentAssignESN>;
                                Session["esnsList"] = esnsList;
                                rptESN.DataSource = null;
                                rptESN.DataBind();
                                rptScanESN.DataSource = null;
                                rptScanESN.DataBind();
                                rptSKU.DataSource = null;
                                rptSKU.DataBind();
                                Session["esns"] = null;
                                Session["nonesns"] = null;
                                btnSubmit.Visible = false;
                                pnlPO.Visible = false;
                                btnUpload.Visible = false;
                                lnkDownload.Visible = false;
                                //btnViewAssignedESN.Visible = true;
                                btnCancel.Visible = true;
                                lblCount.Text = string.Empty;
                                lblNonEsn.Text = string.Empty;
                                lblEsn.Text = string.Empty;
                                rptSKU.DataSource = null;
                                rptSKU.DataBind();
                                rptSKUESN.DataSource = null;
                                rptSKUESN.DataBind();
                            }
                            sqlTrans.Commit();
                        }
                        catch (Exception e2)
                        {
                            // sqlTrans.Rollback();
                        }
                        finally
                        {

                            con.Close();
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

        private void UploadInventoryInfo()
        {
            ESNAssignment esnAssignment = new ESNAssignment();
            //lblMsg.CssClass = "errormessage";
            lblConfirm.Text = string.Empty;
            string customerAccountNumber = string.Empty, trackingNumber = string.Empty;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            //bool esnExists = false;
            //bool esnIncorrectFormat = false;
            bool columnsIncorrectFormat = false;
            string poNum = txtPO.Text.Trim();
            string invalidColumns = string.Empty;
            bool IsValid = true;
            //bool uploadEsn = false;
            if (dpCompany.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(poNum))
                {
                    customerAccountNumber = dpCompany.SelectedValue;
                    esnAssignment.CustomerAccountNumber = customerAccountNumber;
                    esnAssignment.FulfillmentNumber = poNum;
                    try
                    {
                        //foreach (RepeaterItem item in rptUpload.Items)
                        //{
                        //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        //    {
                        //        FileUpload fu = (FileUpload)item.FindControl("fu");
                        //        if (fu.HasFile)
                        //        {
                        //            string path = Server.MapPath("~/images/");
                        //            string fileName = Path.GetFileName(fu.FileName);
                        //            string fileExt = Path.GetExtension(fu.FileName).ToLower();
                        //            fu.SaveAs(path + fileName + fileExt);
                        //        }
                        //    }
                        //}
                        //if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                        //{
                        //    lblMsg.Text = "Select file to upload";
                        //}
                        //else
                        //{

                        //if (flnUpload.PostedFile.ContentLength > 0)
                        //{
                        FulfillmentAssignESN assignESN = null;
                        List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
                        string previoustrackingNumber = string.Empty;
                        int trackingNumberCount = 0, fileCount = 0;
                        if (ViewState["trackingNumberCount"] != null)
                            trackingNumberCount = Convert.ToInt32(ViewState["trackingNumberCount"]);
                        bool IsMultiTrackingNumber = false;
                        foreach (RepeaterItem item in rptUpload.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                FileUpload fu = (FileUpload)item.FindControl("fu");
                                DropDownList ddlTrackingNo = (DropDownList)item.FindControl("ddlTrackingNo");
                                if (ddlTrackingNo.SelectedIndex == 0)
                                    ddlTrackingNo.Focus();
                                //{
                                //    lblMsg.Text = "Please select tracking number";
                                //    ddlTrackingNo.Focus();
                                //    return;
                                //}
                                if (ddlTrackingNo != null)
                                    trackingNumber = ddlTrackingNo.SelectedValue;


                                if (trackingNumberCount > 1)
                                {
                                    IsMultiTrackingNumber = true;
                                    if (!string.IsNullOrWhiteSpace(previoustrackingNumber))
                                        if (previoustrackingNumber != trackingNumber)
                                            IsMultiTrackingNumber = false;
                                }
                                previoustrackingNumber = trackingNumber;
                                if (fu.HasFile)
                                {
                                    fileCount = 1;
                                    string fileName = UploadFile(fu);
                                    string extension = Path.GetExtension(fu.PostedFile.FileName);
                                    extension = extension.ToLower();
                                    if (string.IsNullOrWhiteSpace(trackingNumber))
                                    {
                                        lblMsg.Text = "please select tracking number";
                                        // ddltrackingno.focus();
                                        return;
                                    }
                                    if (extension == ".csv")
                                    {
                                        try
                                        {
                                            using (StreamReader sr = new StreamReader(fileName))
                                            {
                                                string line;
                                                string esn, ICCID;//, fmupc, msl, lteICCID, lteIMSI, otksl, akey;
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
                                                        if (headerArray.Length > 1)
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
                                                                //customerAccountNumber = arr[1].Trim().Trim();
                                                                //sku = arr[2].Trim();
                                                                //esn = arr[1].Trim();
                                                            }
                                                            if (arr.Length > 1)
                                                            {
                                                                ICCID = arr[1].Trim();

                                                            }
                                                            //if(esn.Length < 8 || esn.Length < 18)
                                                            //{

                                                            //        lblMsg.Text = "ESN length should be between 8 and 18 required data";

                                                            //}
                                                            if (string.IsNullOrEmpty(esn))
                                                            {
                                                                lblMsg.Text = "Missing required data";
                                                                IsValid = false;
                                                            }

                                                            assignESN = new FulfillmentAssignESN();

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
                                                            assignESN.FulfillmentNumber = poNum;
                                                            assignESN.CustomerAccountNumber = customerAccountNumber;
                                                            assignESN.SKU = string.Empty;
                                                            assignESN.ESN = esn;
                                                            assignESN.LteICCID = ICCID;
                                                            assignESN.TrackingNumber = trackingNumber;

                                                            esnList.Add(assignESN);
                                                            esn = string.Empty;
                                                            ICCID = string.Empty;
                                                        }
                                                        catch (ApplicationException ex)
                                                        {
                                                            IsValid = false;
                                                            throw ex;
                                                        }
                                                        catch (Exception exception)
                                                        {
                                                            IsValid = false;
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
                                            IsValid = false;
                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Invalid file!";
                                        IsValid = false;
                                    }
                                }
                                //else

                            }
                        }
                        if (fileCount == 0)
                        {
                            //if (esnList == null || (esnList != null && esnList.Count == 0))
                            {
                                lblMsg.Text = "Invalid file!";
                                IsValid = false;
                            }
                        }
                        if (IsMultiTrackingNumber)
                        {
                            lblMsg.Text = "Multi Tracking numbers found but assigned only one!";
                            return;
                        }
                        if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false && IsValid)
                        {

                            rptESN.DataSource = esnList;
                            rptESN.DataBind();
                            lblCount.Text = "Total count: " + esnList.Count;
                            Session["esns"] = esnList;

                            int n = 0;
                            int poRecordCount = 0, Maxqty = 0, statusID = 0;
                            string poErrorMessage = string.Empty;
                            string invalidLTEESNMessage = string.Empty;
                            string invalidESNMessage = string.Empty;
                            string invalidSkuESNMessage = string.Empty;
                            string esnExistsMessage = string.Empty;
                            string kitESNMessage = string.Empty;
                            string ESNquarantine = string.Empty;
                            string errorMessage = string.Empty;
                            string poStatus = string.Empty;
                            List<FulfillmentAssignESN> esnList1 = new List<FulfillmentAssignESN>();
                            double totalChunk = 0;
                            try
                            {

                                totalChunk = (double)esnList.Count / 1000;
                                n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                int esnCount = 1000;
                                //int skip = 1000;
                                int listLength = esnList.Count;
                                List<FulfillmentAssignESN> esnToUpload = null;
                                //var esnToUpload;
                                //for (int i = 0; i < n; i++)
                                for (int i = 0; i < listLength; i = i + 1000)
                                {

                                    esnToUpload = new List<FulfillmentAssignESN>();
                                    esnAssignment.EsnList = new List<FulfillmentAssignESN>();
                                    invalidLTEESNMessage = invalidESNMessage = invalidSkuESNMessage = esnExistsMessage = kitESNMessage = ESNquarantine = string.Empty;
                                    //esnToAdd = new List<FulfillmentAssignESN>();
                                    //if (esnList.Count < 1000)
                                    //{   esnCount = esnList.Count;
                                    //    skip = esnList.Count;
                                    //    if (i > 0)
                                    //        skip = skip + 1000;
                                    //}
                                    esnToUpload = (from item in esnList.Skip(i).Take(esnCount) select item).ToList();

                                    esnAssignment.EsnList = esnToUpload;
                                    //Upload/Assign ESN to POs
                                    int returnValue = 0;
                                    string AlreadyInUseICCIDMessase = string.Empty, ICCIDNotExistsMessase = string.Empty, InvalidICCIDMessase = string.Empty, AlreadyMappedESNMessase = string.Empty;

                                    List<FulfillmentAssignESN> esnList2 = avii.Classes.ESNAssignOperation.ValidateAssignESN_New(esnAssignment, out poRecordCount, out invalidLTEESNMessage, out invalidESNMessage, out invalidSkuESNMessage, out esnExistsMessage, out kitESNMessage, out returnValue, out poStatus, out statusID, out Maxqty, out ESNquarantine);
                                    esnList1.AddRange(esnList2);

                                    if (returnValue == -5)
                                    { errorMessage = "Fulfillment number does not exists!"; }

                                    //if (!string.IsNullOrEmpty(invalidLTEESNMessage))
                                    //{
                                    //    if (string.IsNullOrEmpty(errorMessage))
                                    //        errorMessage = invalidLTEESNMessage + " ESN(s) LTE data missing!";
                                    //    else
                                    //        errorMessage = errorMessage + " <br /> " + invalidLTEESNMessage + " ESN(s) LTE data missing!";
                                    //}

                                    if (statusID == 1 || statusID == 10 || statusID == 11)
                                    {
                                        if (Maxqty == 0)
                                        {
                                            if (string.IsNullOrEmpty(errorMessage))
                                            {

                                                errorMessage = "Provisioning is already done for this Fulfillment number";
                                            }
                                            else
                                                errorMessage = errorMessage + " <br /> " + " Provisioning is already done for this Fulfillment number";
                                        }
                                    }
                                    else
                                    {
                                        //if (poStatus.ToLower() == "processed" || poStatus.ToLower() == "shipped")
                                        {
                                            if (string.IsNullOrEmpty(errorMessage))
                                            {

                                                errorMessage = "Fulfillment number already " + poStatus;

                                            }
                                            else
                                                errorMessage = errorMessage + " <br /> " + " Fulfillment number already " + poStatus;
                                        }

                                    }
                                    if (!string.IsNullOrEmpty(invalidESNMessage))
                                    {
                                        //errorMessage = invalidESNMessage + " ESN(s) does not exists!";
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = invalidESNMessage + " ESN(s) does not exists!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + invalidESNMessage + " ESN(s) does not exists!";

                                    }
                                    if (!string.IsNullOrEmpty(invalidSkuESNMessage))
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";

                                    }
                                    if (!string.IsNullOrEmpty(esnExistsMessage))
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = esnExistsMessage + " ESN(s) are already assigned! <br /> Please unassign first and try again!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + esnExistsMessage + " ESN(s) are already assigned! <br /> Please unassign first and try again!";

                                    }
                                    if (!string.IsNullOrEmpty(kitESNMessage))
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = kitESNMessage + " ESN(s) are not valit KIT!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + kitESNMessage + " ESN(s) are not valit KIT!";

                                    }
                                    if (!string.IsNullOrEmpty(ESNquarantine))
                                    {
                                        if (string.IsNullOrEmpty(ESNquarantine))
                                            errorMessage = ESNquarantine + " ESN(s) are quarantined!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + ESNquarantine + " ESN(s) are quarantined!";

                                    }
                                }
                                if (esnList1 != null && esnList1.Count > 0)
                                {
                                    rptESN.DataSource = esnList1;
                                    rptESN.DataBind();
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
                                lblConfirm.Text = "ESN file is ready to upload";
                                btnUpload.Visible = false;
                                lnkDownload.Visible = false;
                                btnSubmit.Visible = true;
                                btnSubmit2.Visible = true;
                                pnlSubmit.Visible = true;
                                row1.Visible = false;
                                row2.Visible = false;
                                //btnSearch.Visible = false;

                            }
                            else
                            {
                                btnUpload.Visible = true;
                                lnkDownload.Visible = true;
                                btnSubmit.Visible = false;
                                row1.Visible = true;
                                row2.Visible = true;
                                btnSubmit2.Visible = false;
                                pnlSubmit.Visible = false;

                            }

                        }
                        else
                        {
                            rptESN.DataSource = null;
                            rptESN.DataBind();

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
                        //    }
                        //    else
                        //        lblMsg.Text = "Invalid file!";
                        //}
                        //else
                        //    lblMsg.Text = "Invalid file!";
                        //}
                        //}
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rptSKUESN.Visible = false;
            trHR.Visible = true;
            lblMsg.Text = string.Empty;
            lblNonEsn.Text = string.Empty;
            lblEsn.Text = string.Empty;
            CalearForm();
            ViewState["trackingNumberCount"] = null;
            //--//
            int IsFulfillmentNumberExists = 0, esnCount = 0;
            string fulfillmentnumber = txtPO.Text.Trim();
            string companyAccountNumber = string.Empty;
            bool IsOnlyNonEsnItems = false, IsScanESN = false;
            List<FulfillmentAssignNonESN> esnLineItems = null;
            List<FulfillmentAssignESN> esnItems = null;
            List<POTracking> trackingList = null;
            if (dpCompany.SelectedIndex > 0)
            {
                companyAccountNumber = dpCompany.SelectedValue;
                // int.TryParse(dpCompany.SelectedValue, out companyID);
                if (!string.IsNullOrWhiteSpace(fulfillmentnumber))
                {

                    List<FulfillmentAssignNonESN> fulfillmentNonEsnItems = ESNAssignOperation.GetFulfillmentNonEsnItems(fulfillmentnumber, companyAccountNumber, out IsOnlyNonEsnItems, out IsFulfillmentNumberExists, out esnLineItems, out esnItems, out trackingList);
                    if (trackingList != null && trackingList.Count > 0)
                    {
                        ViewState["trackingNumberCount"] = trackingList.Count;
                        Session["trackingList"] = trackingList;
                        rptUpload.DataSource = trackingList;
                        rptUpload.DataBind();

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
                        if (IsFulfillmentNumberExists > 0)
                        {
                            lblMsg.Text = "Please generate shipping label";
                            return;
                        }

                    }
                    if (IsFulfillmentNumberExists == 1 || IsFulfillmentNumberExists == 10 || IsFulfillmentNumberExists == 11)
                    {

                        if (esnLineItems != null && esnLineItems.Count > 0)
                        {
                            lblEsn.Text = "ESN Items";
                            rptSKUESN.DataSource = esnLineItems;
                            rptSKUESN.DataBind();
                            rptSKUESN.Visible = true;
                        }
                        else
                        {
                            lblEsn.Text = "";
                            rptSKUESN.DataSource = null;
                            rptSKUESN.DataBind();
                        }
                        if (esnItems != null && esnItems.Count > 0)
                        {
                            IsScanESN = true;
                            esnCount = esnItems.Count;

                            /// lblEsn.Text = "ESN Items";
                            /// 
                            if (esnCount <= 50)
                            {
                                IncreementIndex = esnItems[0].IsSim == true ? 2 : 1;
                                rptScanESN.DataSource = esnItems;
                                rptScanESN.DataBind();
                            }
                        }
                        else
                        {
                            //lblEsn.Text = "";
                            rptScanESN.DataSource = null;
                            rptScanESN.DataBind();
                        }
                        if (esnLineItems != null && esnLineItems.Count > 0)
                        {
                            var Errors = (from item in esnLineItems where (item.ErrorMessage != "") select item).ToList();
                            if (Errors != null && Errors.Count > 0)
                            {
                                lblMsg.Text = Errors[0].ErrorMessage;
                                btnValidate.Visible = false;
                                //btnSearch.Visible = true;
                                btnCancel.Visible = false;
                                return;
                            }
                        }
                        if (fulfillmentNonEsnItems != null && fulfillmentNonEsnItems.Count > 0)
                        {
                            
                            lblNonEsn.Text = "Non ESN Items";
                            Session["nonesns"] = fulfillmentNonEsnItems;
                            List<FulfillmentAssignNonESN> nonESNList = new List<FulfillmentAssignNonESN>();
                            FulfillmentAssignNonESN esnitems = new FulfillmentAssignNonESN();

                            string sku = string.Empty, SKUs = string.Empty;
                            int qty = 0, index = 0, CurrentStock = 0;
                            for (int i = 0; i < fulfillmentNonEsnItems.Count; i++)
                            {
                                if (!fulfillmentNonEsnItems[i].IsAssign)
                                {
                                    if (string.IsNullOrWhiteSpace(SKUs))
                                        SKUs = fulfillmentNonEsnItems[i].SKU;
                                    else
                                        SKUs = SKUs + ", " + fulfillmentNonEsnItems[i].SKU;
                                }                               

                            }
                            //var nonESnList = (from item in fulfillmentNonEsnItems.Take(esnCount) select item).ToList();//from fulfillmentNonEsnItems
                            rptSKU.DataSource = fulfillmentNonEsnItems;
                            rptSKU.DataBind();
                            rptSKU.Visible = true;
                            tblNonESN.Visible = true;
                            var nonEsnErrors = (from item in fulfillmentNonEsnItems where (item.ErrorMessage != "") select item).ToList();
                            if (nonEsnErrors != null && nonEsnErrors.Count > 0)
                            {
                                lblMsg.Text = nonEsnErrors[0].ErrorMessage;
                                btnValidate.Visible = false;
                                //btnSearch.Visible = true;
                                btnCancel.Visible = false;
                                return;
                            }
                            if (IsOnlyNonEsnItems && !IsScanESN)
                            {
                                rptSKUESN.Visible = false;
                                trHR.Visible = false;
                                row1.Visible = false;
                                row2.Visible = false;
                                row3.Visible = false;
                                btnUpload.Visible = false;
                                lnkDownload.Visible = false;
                                btnSearch.Visible = true;
                                btnSubmit2.Visible = true;
                                btnCancel2.Visible = true;
                                pnlSubmit.Visible = true;
                                btnCancel.Visible = false;
                                btnSubmit.Visible = false;
                                if (!string.IsNullOrWhiteSpace(SKUs))
                                {
                                    btnSubmit2.Visible = false;
                                    lblMsg.Text = "Insufficient stock: " + SKUs + " for listed SKS(s)"; }

                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(SKUs))
                                {
                                    if (IsScanESN && esnCount <= 50)
                                    {

                                        row1.Visible = false;
                                        row3.Visible = true;
                                        row2.Visible = false;
                                        btnUpload.Visible = false;
                                        lnkDownload.Visible = false;
                                        btnValidate.Visible = true;
                                        btnSearch.Visible = true;
                                        btnCancel.Visible = true;
                                    }
                                    else
                                    {
                                        row1.Visible = true;
                                        row2.Visible = true;
                                        row3.Visible = false;
                                        btnUpload.Visible = true;
                                        lnkDownload.Visible = true;
                                        btnSearch.Visible = true;
                                        btnCancel.Visible = true;
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "Insufficient stock: " + SKUs + " for listed SKS(s)";
                                }
                            }
                        }
                        else
                        {
                            if (IsScanESN && esnCount <= 50)
                            {
                                row1.Visible = false;
                                row3.Visible = true;
                                row2.Visible = false;
                                btnUpload.Visible = false;
                                lnkDownload.Visible = false;
                                btnValidate.Visible = true;
                                btnSearch.Visible = true;
                                btnCancel.Visible = true;
                                if (esnLineItems != null && esnLineItems.Count > 0)
                                {
                                    var Errors = (from item in esnLineItems where (item.ErrorMessage != "") select item).ToList();
                                    if (Errors != null && Errors.Count > 0)
                                    {
                                        lblMsg.Text = Errors[0].ErrorMessage;
                                        btnValidate.Visible = false;
                                        //btnSearch.Visible = true;
                                        btnCancel.Visible = false;

                                    }
                                }
                                if (fulfillmentNonEsnItems != null && fulfillmentNonEsnItems.Count > 0)
                                {
                                    var nonEsnErrors = (from item in fulfillmentNonEsnItems where (item.ErrorMessage != "") select item).ToList();
                                    if (nonEsnErrors != null && nonEsnErrors.Count > 0)
                                    {
                                        lblMsg.Text = nonEsnErrors[0].ErrorMessage;
                                        btnValidate.Visible = false;
                                        //btnSearch.Visible = true;
                                        btnCancel.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                lblNonEsn.Text = "";
                                rptSKU.DataSource = null;
                                rptSKU.DataBind();
                                rptSKU.Visible = false;
                                tblNonESN.Visible = false;
                                // lblMsg.Text = "There is no non ESN items for this order!";
                                row1.Visible = true;
                                row2.Visible = true;
                                row3.Visible = false;
                                btnUpload.Visible = true;
                                lnkDownload.Visible = true;
                                btnCancel.Visible = true;

                                if (esnLineItems != null && esnLineItems.Count > 0)
                                {
                                    var Errors = (from item in esnLineItems where (item.ErrorMessage != "") select item).ToList();
                                    if (Errors != null && Errors.Count > 0)
                                    {
                                        lblMsg.Text = Errors[0].ErrorMessage;
                                        btnUpload.Visible = false;
                                        lnkDownload.Visible = false;
                                    }
                                }
                                if (fulfillmentNonEsnItems != null && fulfillmentNonEsnItems.Count > 0)
                                {
                                    var nonEsnErrors = (from item in fulfillmentNonEsnItems where (item.ErrorMessage != "") select item).ToList();
                                    if (nonEsnErrors != null && nonEsnErrors.Count > 0)
                                    {
                                        lblMsg.Text = nonEsnErrors[0].ErrorMessage;
                                        btnUpload.Visible = false;
                                        lnkDownload.Visible = false;
                                    }
                                }
                                // btnSearch.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lblNonEsn.Text = "";
                        rptSKU.DataSource = null;
                        rptSKU.DataBind();
                        rptSKU.Visible = false;
                        tblNonESN.Visible = false;
                        if (IsFulfillmentNumberExists == 0)
                        {
                            lblMsg.Text = "Fulfillment number does not exists!";
                        }
                        else //if (IsFulfillmentNumberExists == 2 || IsFulfillmentNumberExists == 3 || IsFulfillmentNumberExists == 11)
                        {
                            if (IsFulfillmentNumberExists == 2)
                                lblMsg.Text = "Fulfillment number already Processed!";
                            else if (IsFulfillmentNumberExists == 3)
                                lblMsg.Text = "Fulfillment number already Shipped!";
                            else if (IsFulfillmentNumberExists == 11)
                                lblMsg.Text = "Fulfillment number already Partial Shipped!";
                            else
                                lblMsg.Text = "Fulfillment number does not exists!";
                        }

                    }
                    // rptSKU.DataBind();
                }
                else
                    lblMsg.Text = "Please enter Fulfillment number!";
            }
            else
                lblMsg.Text = "Please select customer!";


            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "ESN" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=B2CProvisioning.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }
    }
}