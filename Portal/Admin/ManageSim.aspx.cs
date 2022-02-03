using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;


namespace avii.Admin
{
    public partial class ManageSim : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/SIMUpload/";
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
                trSKU.Visible = false;
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvSIM.DataSource = null;
            gvSIM.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

            trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                trSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = MslOperation.GetCompanySKUList(companyID, 1);
            if (skuList != null && skuList.Count > 0)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
            }
            else
            {
                trSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SIM SKU/Product are assigned to select Customer";

            }


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSIM.PageIndex = e.NewPageIndex;
            if (Session["mslesn"] != null)
            {
                List<EsnUpload> esnList = (List<EsnUpload>)Session["mslesn"];

                gvSIM.DataSource = esnList;
                gvSIM.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            string comment = txtComment.Text;
            
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            int itemCompanyGUID, insertCount, updateCount;
            itemCompanyGUID = insertCount = updateCount = 0;
            bool returnValue = false;
            string errorMessage = string.Empty;
            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            if (itemCompanyGUID > 0)
            {
                if (Session["simlist"] != null)
                {
                    List<SimList> simList = (Session["simlist"]) as List<SimList>;
                    returnValue = SimOperations.SimInsertUpdate(simList, itemCompanyGUID, userID, chkDelete.Checked, filename, comment, out insertCount, out updateCount, out errorMessage);
                    if (returnValue)
                    {
                        
                        if (!chkDelete.Checked)
                        {
                            ClearForm();
                            if (insertCount > 0 && updateCount > 0)
                                lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";

                            if (insertCount > 0 && updateCount == 0)
                                lblMsg.Text = insertCount + " records  successfully inserted.";
                            if (insertCount == 0 && updateCount > 0)
                                lblMsg.Text = updateCount + " records  successfully updated.";
                        }
                        else
                        {
                            ClearForm();
                            lblMsg.Text = updateCount + " records  successfully deleted.";
                        }

                    }
                    else
                        lblMsg.Text = errorMessage;


                }
                else
                    lblMsg.Text = "Session expire!";
            }
            else
                lblMsg.Text = "Please select SKU";
        }
        private void ClearForm()
        {
            txtComment.Text = string.Empty;
            chkDelete.Checked = false;
            trSKU.Visible = false;
            dpCompany.SelectedIndex = 0;
            gvSIM.DataSource = null;
            gvSIM.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            ValidateSIM();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ValidateSIM()
        {
            int itemCompanyGUID = 0;
            string skuText = ddlSKU.SelectedItem.Text;
            string[] str = skuText.Split('-');
            string sku = string.Empty;
            if (str != null && str.Length > 0)
                sku = str[0];
            //bool isLTE = false;
            //bool isSim = false;
            int returnValue = 0;
            //int companyID = 0;

            //lblMsg.CssClass = "errormessage";
            lblConfirm.Text = string.Empty;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            Hashtable hshSim = new Hashtable();
            //bool esnExists = false;
            //bool esnIncorrectFormat = false;
            bool columnsIncorrectFormat = false;
            //if(ddlSKU.SelectedIndex >
            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            if (itemCompanyGUID > 0)
            {
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
                            SimList simObj = null;
                            List<SimList> simList = new List<SimList>();

                            if (extension == ".csv")
                            {
                                try
                                {
                                    using (StreamReader sr = new StreamReader(fileName))
                                    {
                                        string line;
                                        string sim, esn;
                                        int i = 0;
                                        sim = esn = string.Empty;
                                        while ((line = sr.ReadLine()) != null)
                                        {

                                            if (!string.IsNullOrEmpty(line) && i == 0)
                                            {
                                                i = 1;
                                                line = line.ToLower();

                                                string[] headerArray = line.Split(',');
                                                if (headerArray.Length == 2)
                                                {
                                                    if (headerArray[0].Trim() != "sim")
                                                    {
                                                        invalidColumns = headerArray[0];
                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray[1].Trim() != "esn")
                                                    {
                                                        invalidColumns = headerArray[1];
                                                        columnsIncorrectFormat = true;
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    columnsIncorrectFormat = true;
                                                    invalidColumns = string.Empty;
                                                }


                                            }
                                            else if (!string.IsNullOrEmpty(line) && i > 0)
                                            {
                                                sim = esn = string.Empty;
                                                //poNum = sku = customerAccountNumber = fmupc = esn = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                string[] arr = line.Split(',');
                                                try
                                                {
                                                    simObj = new SimList();
                                                    //if (arr.Length > 4)
                                                    //{
                                                    sim = arr[0].Trim();
                                                    esn = arr[1].Trim();
                                                    
                                                    //}

                                                    //if (string.IsNullOrEmpty(esn) || string.IsNullOrEmpty(msl) || string.IsNullOrEmpty(meid) || string.IsNullOrEmpty(akey) || string.IsNullOrEmpty(otksl))
                                                    if (string.IsNullOrEmpty(sim))
                                                    {
                                                        
                                                            lblMsg.Text = "Missing SIM data";
                                                        
                                                    }



                                                    if (hshSim.ContainsKey(sim) && !string.IsNullOrEmpty(sim))
                                                    {
                                                        //uploadEsn = false;
                                                        throw new ApplicationException("Duplicate " + sim + " SIM(s) exists in the file");
                                                    }
                                                    else if (!hshSim.ContainsKey(sim) && !string.IsNullOrEmpty(sim))
                                                    {
                                                        hshSim.Add(sim, sim);
                                                    }

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

                                                    simObj.SIM = sim;
                                                    simObj.ESN = esn;
                                                    simObj.SKU = sku;

                                                    simList.Add(simObj);
                                                    sim = esn = string.Empty;
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

                                if (simList != null && simList.Count > 0 && columnsIncorrectFormat == false)
                                {

                                    gvSIM.DataSource = simList;
                                    gvSIM.DataBind();
                                    lblCount.Text = "Total count: " + simList.Count;
                                    Session["simlist"] = simList;

                                    int n = 0;
                                    //int poRecordCount = 0;
                                    string poErrorMessage = string.Empty;
                                    string poSimMessage = string.Empty;
                                    string duplicateESN = string.Empty;
                                    string errorMessage = string.Empty;
                                    string poEsnMessage = string.Empty;
                                    string poEsnSimMessage = string.Empty;
                                    double totalChunk = 0;
                                    try
                                    {

                                        totalChunk = (double)simList.Count / 1000;
                                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                        int esnCount = 1000;
                                        //var esnToUpload;
                                        for (int i = 0; i < n; i++)
                                        {
                                            poErrorMessage = duplicateESN = poSimMessage = poEsnMessage = poEsnSimMessage = string.Empty;
                                            //isLTE = false;
                                            //isSim = false;
                                            returnValue = 0;
                                            //esnToAdd = new List<FulfillmentAssignESN>();
                                            if (simList.Count < 1000)
                                                esnCount = simList.Count;
                                            var esnToUpload = (from item in simList.Take(esnCount) select item).ToList();

                                            //Upload/Assign ESN to POs
                                            avii.Classes.SimOperations.ValidateSIM(esnToUpload, itemCompanyGUID, out poErrorMessage, out poSimMessage, out poEsnMessage, out poEsnSimMessage, out returnValue);

                                            if (!string.IsNullOrEmpty(poErrorMessage) && returnValue == 3)
                                            {
                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = poErrorMessage;
                                                else
                                                    errorMessage = errorMessage + " <br /> " + poErrorMessage;
                                            }
                                            if (!string.IsNullOrEmpty(poSimMessage))
                                            {
                                                if (!chkDelete.Checked)
                                                {
                                                    if (string.IsNullOrEmpty(errorMessage))
                                                        errorMessage = poSimMessage + " SIM(s) can not re-assign because already assigned to fulfillment order";
                                                    else
                                                        errorMessage = errorMessage + " <br /> " + poSimMessage + " SIM(s) can not re-assign because already assigned to fulfillment order";
                                                }
                                                else
                                                {
                                                    if (string.IsNullOrEmpty(errorMessage))
                                                        errorMessage = poSimMessage + " SIM(s) can not deleted because already assigned to fulfillment order";
                                                    else
                                                        errorMessage = errorMessage + " <br /> " + poSimMessage + " SIM(s) can not deleted because already assigned to fulfillment order";
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(poEsnMessage))
                                            {

                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = poEsnMessage + " ESN(s) does not exists in the repository";
                                                else
                                                    errorMessage = errorMessage + " <br /> " + poEsnMessage + " ESN(s) does not exists in the repository";

                                            }
                                            if (!string.IsNullOrEmpty(poEsnSimMessage))
                                            {

                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = poEsnSimMessage + " ESN(s) already have SIM assigned";
                                                else
                                                    errorMessage = errorMessage + " <br /> " + poEsnSimMessage + " ESN(s) already have SIM assigned";

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
                                        return;
                                    }
                                    if (lblMsg.Text == string.Empty)
                                    {
                                        //lblMsg.CssClass = "errorGreenMsg";
                                        lblConfirm.Text = "SIM file is ready to upload";
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
                                    gvSIM.DataSource = null;
                                    gvSIM.DataBind();

                                    if (simList != null && simList.Count == 0)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                lblMsg.Text = invalidColumns + " column name is not correct";
                                            else
                                                lblMsg.Text = "File format is not correct";
                                        }
                                        else
                                            lblMsg.Text = "There is no SIM to upload";

                                    }
                                    if (simList != null)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            //lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                lblMsg.Text = invalidColumns + " column name is not correct";
                                            else
                                                lblMsg.Text = "File format is not correct";
                                        }
                                        else
                                            lblMsg.Text = "There is no SIM to upload";
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
            else
                lblMsg.Text = "Please select SKU";
        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["SIMUploadFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["SIMUploadFilesStoreage"].ToString();
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