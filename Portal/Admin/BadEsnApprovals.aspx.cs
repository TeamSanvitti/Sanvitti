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
    public partial class BadEsnApprovals : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                if (Session["adm"] != null)
                {
                    BindCustomer();
                }
                else
                {
                    lblCust.Visible = false;
                    dpCompany.Visible = false;
                }
            }
        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            ValidateESNs();
        }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string reason = string.Empty;
            int companyID = 0;
            int recordCount = 0;
            bool approval = false;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Please select customer!";
                    return;
                }
                approval = true;

            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;


                    companyID = userInfo.CompanyGUID;
                    approval = false;
                }
            }

            //reason = txtReason.Text.Trim();
            List<EsnList> esnList = null;
            try
            {
                if (Session["esnapproval"] != null)
                {
                    esnList = (Session["esnapproval"]) as List<EsnList>;
                    BabEsnOperation.BadEsnApprovalUpdate(esnList, companyID, out recordCount);
                    if (recordCount > 0)
                    {
                        ClearForm();
                        lblMsg.Text = "Submitted successfully <br /> Record Count:" + recordCount;

                    }
                    else
                    {
                        lblMsg.Text = "Submitted successfully <br /> Record Count:" + recordCount;
                    }
                }
                else
                    lblMsg.Text = "Session expire!";

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }



        }
        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnIsESN = e.Item.FindControl("hdnIsESN") as HiddenField;
                if (hdnIsESN != null && hdnIsESN.Value.ToLower() == "false")
                {
                    lblMsg.Text = "Hilighted ESN(s) does not exists in repository";
                }

            }
        }
        private void ValidateESNs()
        {
            //lblMsg.CssClass = "errormessage";
            lblConfirm.Text = string.Empty;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;

            string inactiveESNMsg = string.Empty;
            string invalidESNMsg = string.Empty;
            int recordCount = 0;
            int companyID = 0;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Please select customer!";
                    return;
                }


            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;


                    companyID = userInfo.CompanyGUID;

                }
            }
            Hashtable hshESN = new Hashtable();
            //bool esnExists = false;
            //bool esnIncorrectFormat = false;
            bool columnsIncorrectFormat = false;

            //bool uploadEsn = false;
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
                        string fileName = UploadFile();
                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                        extension = extension.ToLower();
                        string invalidColumns = string.Empty;
                        EsnList assignESN = null;
                        List<EsnList> esnList = new List<EsnList>();

                        if (extension == ".csv")
                        {
                            try
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

                                            if (headerArray[0].Trim() != "esn")
                                            {
                                                invalidColumns = headerArray[0];
                                                columnsIncorrectFormat = true;
                                            }


                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            esn = string.Empty;
                                            string[] arr = line.Split(',');
                                            try
                                            {

                                                if (arr.Length > 0)
                                                {
                                                    esn = arr[0].Trim();

                                                }
                                                //if (string.IsNullOrEmpty(poNum) || string.IsNullOrEmpty(customerAccountNumber) || string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(esn))
                                                //{
                                                //    lblMsg.Text = "Missing required data";
                                                //}
                                                if (!string.IsNullOrEmpty(esn))
                                                {

                                                    assignESN = new EsnList();

                                                    if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        //uploadEsn = false;
                                                        throw new ApplicationException("Duplicate " + esn + " ESN(s) exists in the file");
                                                    }
                                                    else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        hshESN.Add(esn, esn);
                                                    }


                                                    //uploadEsn = true;
                                                    assignESN.ESN = esn;
                                                    //assignESN.AKEY = string.Empty;
                                                    //assignESN.MEID = string.Empty;

                                                    esnList.Add(assignESN);
                                                    esn = string.Empty;
                                                }
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
                            string errorMessage = string.Empty;
                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                            {
                                List<EsnInfo> esnInfoList = null;
                                esnInfoList = avii.Classes.BabEsnOperation.ValidateBadEsnApproval(esnList, companyID, out recordCount, out inactiveESNMsg, out invalidESNMsg);

                                rptESN.DataSource = esnInfoList;
                                rptESN.DataBind();
                                lblCount.Text = "Total count: " + esnInfoList.Count;
                                Session["esnapproval"] = esnList;
                                errorMessage = lblMsg.Text;

                                if (string.IsNullOrEmpty(errorMessage) && string.IsNullOrEmpty(inactiveESNMsg) && string.IsNullOrEmpty(inactiveESNMsg))
                                {
                                    //lblMsg.CssClass = "errorGreenMsg";
                                    lblConfirm.Text = "ESN file is ready to upload";
                                    btnUpload.Visible = false;
                                    btnSubmit.Visible = true;
                                    btnSubmit2.Visible = true;
                                    pnlSubmit.Visible = true;

                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                    {
                                        if (!string.IsNullOrEmpty(inactiveESNMsg))
                                        {
                                            errorMessage = inactiveESNMsg + " ESN(s) product are not active";
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(inactiveESNMsg))
                                        {
                                            errorMessage = errorMessage + "<br /> " + inactiveESNMsg + " ESN(s) product are not active";
                                        }
                                    }
                                    if (string.IsNullOrEmpty(errorMessage))
                                    {
                                        if (!string.IsNullOrEmpty(invalidESNMsg))
                                        {
                                            errorMessage = invalidESNMsg + " ESN(s) are not valid";
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(invalidESNMsg))
                                        {
                                            errorMessage = errorMessage + "<br />" + invalidESNMsg + " ESN(s) are not valid";
                                        }
                                    }

                                    btnUpload.Visible = true;
                                    btnSubmit.Visible = false;

                                    btnSubmit2.Visible = false;
                                    pnlSubmit.Visible = false;
                                    lblMsg.Text = errorMessage;


                                }

                            }
                            else
                            {
                                rptESN.DataSource = null;
                                rptESN.DataBind();
                                Session["esnapproval"] = null;

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

        protected void btnViewAssignedESN_Click(object sender, EventArgs e)
        { }
        private void ClearForm()
        {
            rptESN.DataSource = null;
            rptESN.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

            btnViewAssignedESN.Visible = false;
            //txtReason.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";

            ClearForm();
        }


    }
}