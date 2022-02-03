using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using avii.Classes;

namespace avii.Admin
{
    public partial class AssignSIM : System.Web.UI.Page
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
            
        }
        private void ValidateSIM()
        {
            lblMsg.Text = string.Empty;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }

            Hashtable hshSIM = new Hashtable();
            //bool esnExists = false;
            bool esnIncorrectFormat = false;
            bool uploadEsn = false;
            bool columnsIncorrectFormat = false;
            string invalidColumns = string.Empty;
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
                        System.Text.StringBuilder sbEsns = new StringBuilder();
                        StringWriter stringWriter = new StringWriter();
                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                        extension = extension.ToLower();
                        
                        List<FulfillmentSim> fulfillmentSimList = new List<FulfillmentSim>();
                        FulfillmentSim fulfillmentSim = null;
                        //using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                        if (extension == ".csv")
                        {
                            try
                            {
                                //xw.WriteStartElement("purchaseorder");
                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line;
                                    string poNum, fmupc, esn, lteICCID, lteIMSI, otksl, akey, sku;
                                    double esnTest = 0;
                                    clsInventory inventory = new clsInventory();
                                    
                                    int i = 0;
                                    int podid = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                        {
                                            i = 1;
                                            line = line.ToLower();

                                            string[] headerArray = line.Split(',');
                                            if (headerArray.Length == 9)
                                            {
                                                if (headerArray[0].Trim() != "ponum")
                                                {
                                                    invalidColumns = headerArray[0];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[1].Trim() != "podid")
                                                {
                                                    invalidColumns = headerArray[1];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[2].Trim() != "itemcode")
                                                {
                                                    invalidColumns = headerArray[2];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[3].Trim() != "esn")
                                                {
                                                    invalidColumns = headerArray[3];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[4].Trim() != "fmupc")
                                                {
                                                    invalidColumns = headerArray[4];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[5].Trim() != "lteiccid")
                                                {
                                                    invalidColumns = headerArray[5];
                                                    columnsIncorrectFormat = true;
                                                } if (headerArray[6].Trim() != "lteimsi")
                                                {
                                                    invalidColumns = headerArray[6];
                                                    columnsIncorrectFormat = true;
                                                }
                                                if (headerArray[7].Trim() != "otksl")
                                                {
                                                    invalidColumns = headerArray[7];
                                                    columnsIncorrectFormat = true;
                                                } if (headerArray[8].Trim() != "akey")
                                                {
                                                    invalidColumns = headerArray[8];
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
                                            fmupc = esn = lteICCID = lteIMSI = otksl = akey = poNum = sku = string.Empty;
                                            podid = 0;
                                            string[] arr = line.Split(',');
                                            try
                                            {
                                                if (arr.Length > 0)
                                                {
                                                    poNum = arr[0].Trim();
                                                }
                                                if (arr.Length > 1)
                                                {
                                                    podid = Convert.ToInt32(arr[1].Trim());
                                                }
                                                if (arr.Length > 2)
                                                {
                                                    sku = arr[2].Trim();
                                                }
                                                if (arr.Length > 3)
                                                {
                                                    esn = arr[3].Trim();
                                                }
                                                if (string.IsNullOrEmpty(esn))
                                                {

                                                    lblMsg.Text = "Missing SIM data";

                                                }

                                                if (!string.IsNullOrEmpty(esn))
                                                {
                                                    if (hshSIM.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        uploadEsn = false;
                                                        throw new ApplicationException("Duplicate SIM(s) exists in the file");
                                                    }
                                                    else if (!hshSIM.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        hshSIM.Add(esn, esn);
                                                    }
                                                }
                                                //esnExists = clsInventoryDB.ValidateEsnExists(esn);
                                                //if (esnExists)
                                                //{
                                                //    sbEsns.Append(esn + " ");
                                                //    esnExists = false;
                                                //}
                                                //else
                                                //{
                                                if (arr.Length > 4)
                                                {
                                                    fmupc = arr[4].Trim();
                                                }
                                                if (arr.Length > 5)
                                                {
                                                    lteICCID = arr[5].Trim();
                                                }
                                                if (arr.Length > 6)
                                                {
                                                    lteIMSI = arr[6].Trim();
                                                }
                                                if (arr.Length > 7)
                                                {
                                                    otksl = arr[7].Trim();
                                                }
                                                if (arr.Length > 8)
                                                {
                                                    akey = arr[8].Trim();
                                                }
                                                //if (arr.Length > 9)
                                                //{
                                                //    simNumber = arr[9].Trim();
                                                //}


                                                uploadEsn = true;

                                                fulfillmentSim = new FulfillmentSim();
                                                fulfillmentSim.PODID = podid;
                                                fulfillmentSim.SIM = esn;
                                                fulfillmentSim.FulfillmentNumber = poNum;
                                                fulfillmentSim.SKU = sku;

                                                fulfillmentSimList.Add(fulfillmentSim);
                                                esn = string.Empty;
                                                podid = 0;
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
                        }
                        else
                        {
                            lblMsg.Text = "Invalid file!";
                        }

                        if (fulfillmentSimList != null && fulfillmentSimList.Count > 0 && uploadEsn && esnIncorrectFormat == false)
                        {
                            gvSIM.DataSource = fulfillmentSimList;
                            gvSIM.DataBind();
                            lblCount.Text = "Total count: " + fulfillmentSimList.Count;
                            Session["posimlist"] = fulfillmentSimList;

                            int n = 0;
                            int returnValue = 0;
                            string poErrorMessage = string.Empty;
                            string poSimMessage = string.Empty;
                            //string duplicateESN = string.Empty;
                            string errorMessage = string.Empty;
                            string poskuMessage = string.Empty;
                            string poEsnSimMessage = string.Empty;
                            double totalChunk = 0;
                            try
                            {

                                totalChunk = (double)fulfillmentSimList.Count / 1000;
                                n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                int esnCount = 1000;
                                //var esnToUpload;
                                for (int i = 0; i < n; i++)
                                {
                                    poErrorMessage =  poSimMessage = poskuMessage = poEsnSimMessage = string.Empty;
                                    //isLTE = false;
                                    //isSim = false;
                                    returnValue = 0;
                                    //esnToAdd = new List<FulfillmentAssignESN>();
                                    if (fulfillmentSimList.Count < 1000)
                                        esnCount = fulfillmentSimList.Count;
                                    var esnToUpload = (from item in fulfillmentSimList.Take(esnCount) select item).ToList();

                                    //Upload/Assign ESN to POs
                                    avii.Classes.SimOperations.Validate_Fulfillment_SIM(esnToUpload, out poErrorMessage, out poSimMessage, out poskuMessage, out poEsnSimMessage, out returnValue);

                                    if (!string.IsNullOrEmpty(poErrorMessage) && returnValue == 0)
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = poErrorMessage + " SIM(s) does not exists in the repository.";
                                        else
                                            errorMessage = errorMessage + " <br /> " + poErrorMessage + " SIM(s) does not exists in the repository.";
                                    }
                                    if (!string.IsNullOrEmpty(poErrorMessage) && returnValue == 3)
                                    {
                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = poErrorMessage;
                                        else
                                            errorMessage = errorMessage + " <br /> " + poErrorMessage;
                                    }
                                    if (!string.IsNullOrEmpty(poSimMessage))
                                    {

                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = poSimMessage + " SIM(s) can not re-assign because already assigned to fulfillment order";
                                        else
                                            errorMessage = errorMessage + " <br /> " + poSimMessage + " SIM(s) can not re-assign because already assigned to fulfillment order";

                                    }
                                    if (!string.IsNullOrEmpty(poskuMessage))
                                    {

                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = poskuMessage + " SIM(s) are not assigned to right Product/SKU!";
                                        else
                                            errorMessage = errorMessage + " <br /> " + poskuMessage + " SIM(s) are not assigned to right Product/SKU!";

                                    }
                                    if (!string.IsNullOrEmpty(poEsnSimMessage))
                                    {

                                        if (string.IsNullOrEmpty(errorMessage))
                                            errorMessage = poEsnSimMessage + " SIM(s) already assigned to ESN(s)";
                                        else
                                            errorMessage = errorMessage + " <br /> " + poEsnSimMessage + " SIM(s) already assigned to ESN(s)";

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

                            if (fulfillmentSimList != null && fulfillmentSimList.Count == 0)
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
                            if (fulfillmentSimList != null)
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
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSIM.PageIndex = e.NewPageIndex;
            if (Session["posimlist"] != null)
            {
                List<FulfillmentSim> esnList = (List<FulfillmentSim>)Session["posimlist"];

                gvSIM.DataSource = esnList;
                gvSIM.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            int insertCount, updateCount;
            insertCount = updateCount = 0;
            bool returnValue = false;
            string errorMessage = string.Empty;
            
            if (Session["posimlist"] != null)
            {
                List<FulfillmentSim> simList = (Session["posimlist"]) as List<FulfillmentSim>;
                returnValue = SimOperations.FulfillmentSimInsert(simList, userID, "U", out insertCount, out errorMessage);
                if (returnValue)
                {

                    //if (!chkDelete.Checked)
                    {
                        ClearForm();
                        if (insertCount > 0 && updateCount > 0)
                            lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";

                        if (insertCount > 0 && updateCount == 0)
                            lblMsg.Text = insertCount + " records  successfully inserted.";
                        if (insertCount == 0 && updateCount > 0)
                            lblMsg.Text = updateCount + " records  successfully updated.";
                    }
                    //else
                    //{
                    //    ClearForm();
                    //    lblMsg.Text = updateCount + " records  successfully deleted.";
                    //}

                }
                else
                    lblMsg.Text = errorMessage;


            }
            else
                lblMsg.Text = "Session expire!";


        }
        private void ClearForm()
        {
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