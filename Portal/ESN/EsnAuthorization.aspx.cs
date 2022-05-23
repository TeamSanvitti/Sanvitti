using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
//using avii.Classes;
using SV.Framework.Models.Inventory;

namespace avii.ESN
{
    public partial class EsnAuthorization : System.Web.UI.Page
    {
        SV.Framework.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.Inventory.EsnAuthorizationOperation>();

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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                if (Session["ESNHeaderID"] == null)
                {
                    BindCustomer();

                    plnFSearch.Visible = false;
                    pnlUpload.Visible = true;

                }
                else
                {
                    BindSKU();

                    plnFSearch.Visible = true;
                    pnlUpload.Visible = false;
                }
            }
        }
        protected void BindSKU()
        { 
            int ESNHeaderID = Convert.ToInt32(Session["ESNHeaderID"]);
            Session["ESNHeaderID"] = null;
            ViewState["ESNHeaderID"] = ESNHeaderID;
            lblSKU.Text = "";
            lblMsg.Text = "";
            btnDownload.Visible = false;

            List<KittedSKUs> skuList = esnAuthorizationOperation.GetAuthorizationKittedSKU(ESNHeaderID);
            if (skuList != null && skuList.Count > 0)
            {
                lblSKU.Text = skuList[0].SKU;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemCompanyGUID";
                ddlSKU.DataTextField = "KittedSKU";
                ddlSKU.DataBind();
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "0");
                ddlSKU.Items.Insert(0, item);
                btnDownload.Visible = true;
                if (skuList.Count == 1)
                    ddlSKU.SelectedIndex = 1;
                //Session["ESNHeaderID"] = null;
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "Kitted SKU no exists for this product!";

            }           

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            int ESNHeaderID = 0, ItemCompanyGUID = 0;
            string SequenceNumber = "", productType = "";
            if (ViewState["ESNHeaderID"] != null)
            {
                ESNHeaderID = Convert.ToInt32(ViewState["ESNHeaderID"]);
                if(ddlSKU.SelectedIndex > 0)
                {
                    ItemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

                    List<ESNAuthorization> ESNs = esnAuthorizationOperation.GetESNAuthorizations(ESNHeaderID, ItemCompanyGUID, 0, out SequenceNumber, out productType);

                //List<ESNAuthorization> ESNs = Session["ESNs"] as List<ESNAuthorization>;
                var memoryStream = new MemoryStream();
                //   System.Xml.XmlWriter write =  new   ;
                string fileName;
                string filePrefix = "spdish";
                //string filePrefix = "spappledsh";
                string transDate;

                // string fileName = filePrefix + "_" + transDate + "_" + edfFileInfo.fileSequence.ToString() + ".xml";
                DateTime dt = DateTime.Now;
                string currentDate = dt.ToString("yyyy-MM-dd");

               // Int64 fileSequence = dt.Ticks;

                string filePath = Server.MapPath("~/UploadedData/");

                    if (ESNs != null && ESNs.Count > 0)
                    {

                        transDate = currentDate.Replace("-", "");
                        fileName = filePrefix + "_" + transDate + "_" + SequenceNumber.ToString() + ".xml";
                        filePath = filePath + fileName;

                        XElement xmlElement = esnAuthorizationOperation.CreateAuthorizationFile(ESNs, SequenceNumber, currentDate, productType);

                        xmlElement.Save(filePath);

                        // tring strFullPath = Server.MapPath("~/temp.xml");
                        string strContents = null;
                        System.IO.StreamReader objReader = default(System.IO.StreamReader);
                        objReader = new System.IO.StreamReader(filePath);
                        strContents = objReader.ReadToEnd();
                        objReader.Close();

                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.ContentType = "application/xml";
                        Response.AddHeader("content-disposition", attachment);
                        Response.Write(strContents);
                        Response.End();
                    }
                    else
                    {
                        lblMsg.Text = "No data found.";
                    }
                }
                else
                {
                    lblMsg.Text = "Kitted SKU is required!";
                    
                }
            }
        }
        
        protected void btnCSV_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "ESN" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EsnFile.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlSKU.SelectedIndex = 0;
            //rptESNs.DataSource = null;
            //rptESNs.DataBind();
            //lblCount.Text = "";
            lblMsg.Text = "";
            btnDownload.Visible = false;
        }

        protected void btnUploadValidate_Click(object sender, EventArgs e)
        {
            ValidateMslESN();
        }
        private void ValidateMslESN()
        {
            string PlannedProvisioingDate = null;
            if (!string.IsNullOrEmpty(txtShipDate.Text.Trim()))
                PlannedProvisioingDate = txtShipDate.Text.Trim();
            btnPOSLabel.Visible = false;
            int itemCompanyGUID = 0;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            string sku = "", SWVersion = "", ManufactureCode = "", DisplayName = "", productType="";
            bool columnsIncorrectFormat = false;
            int esnLength = 0, decLength = 0;
            string runNumber = txtRunNumber.Text.Trim();
            int kittedItemCompanyGUID = 0;
            int userID = 0;
           
            userID = Convert.ToInt32(Session["UserID"]);
            if (!string.IsNullOrEmpty(PlannedProvisioingDate))
            {
                if (ViewState["sku"] != null)
                {
                    if (ddlKitSKU.SelectedIndex > 0)
                    {
                        sku = ddlKitSKU.SelectedItem.Text;
                        kittedItemCompanyGUID = Convert.ToInt32(ddlKitSKU.SelectedValue);
                    }
                    else
                        sku = Convert.ToString(ViewState["sku"]);
                    SWVersion = Convert.ToString(ViewState["SWVersion"]);
                    ManufactureCode = ViewState["ManufactureCode"] as string;
                    DisplayName = ViewState["DisplayName"] as string;


                    itemCompanyGUID = Convert.ToInt32(ddlRawSKU.SelectedValue);
                    if (itemCompanyGUID > 0)
                    {
                        if (!string.IsNullOrEmpty(runNumber))
                        {
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
                                        System.Text.StringBuilder sbInvalidColumns = new System.Text.StringBuilder();
                                       // System.Text.StringBuilder sbESN = new System.Text.StringBuilder();
                                       // System.Text.StringBuilder sbHex = new System.Text.StringBuilder();
                                       // System.Text.StringBuilder sbDec = new System.Text.StringBuilder();
                                       // System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();
                                        EsnUploadNew assignESN = null;
                                        List<EsnUploadNew> esnList = new List<EsnUploadNew>();

                                        if (extension == ".csv")
                                        {
                                            using (StreamReader sr = new StreamReader(fileName))
                                            {
                                                string line;
                                                string SNo, esn, imei2, batch, lteICCID, MeidHex, MeidDec, Location, MSL, OTKSL, SerialNumber, BoxID;//, uploaddate;
                                                int i = 0;
                                                while ((line = sr.ReadLine()) != null)
                                                {
                                                    if (!string.IsNullOrEmpty(line) && i == 0)
                                                    {
                                                        i = 1;
                                                        line = line.ToLower();

                                                        string[] headerArray = line.Split(',');
                                                        if (headerArray.Length == 2 || headerArray.Length <= 11)
                                                        {
                                                            if (headerArray[0].Trim() != "seq.no.")
                                                            {
                                                                invalidColumns = headerArray[0];
                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray[1].Trim() != "batch")
                                                            {
                                                                invalidColumns = headerArray[1];
                                                                columnsIncorrectFormat = true;
                                                            }

                                                            if (headerArray[2].Trim() != "esn1")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[2] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 3 && headerArray[3].Trim() != "" && headerArray[3].Trim() != "esn2")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[3] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 4 && headerArray[4].Trim() != "" && headerArray[4].Trim() != "meidhex")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[4] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 5 && headerArray[5].Trim() != "" && headerArray[5].Trim() != "meiddec")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[5] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            

                                                            if (headerArray.Length > 6 && headerArray[6].Trim() != "" && headerArray[6].Trim() != "location")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[6] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 7 && headerArray[7].Trim() != "" && headerArray[7].Trim() != "msl")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[7] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }

                                                            if (headerArray.Length > 8 && headerArray[8].Trim() != "" && headerArray[8].Trim() != "otksl")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[8] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 9 && headerArray[9].Trim() != "" && headerArray[9].Trim() != "serialnumber")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[9] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 10 && headerArray[10].Trim() != "" && headerArray[10].Trim() != "boxid")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[10] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }

                                                            invalidColumns = sbInvalidColumns.ToString();


                                                        }
                                                        else
                                                        {
                                                            columnsIncorrectFormat = true;
                                                            invalidColumns = string.Empty;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(line) && i > 0)
                                                    {
                                                        SNo = esn = imei2 = batch = lteICCID = MeidHex = MeidDec = Location = MSL = OTKSL = SerialNumber = BoxID = string.Empty;
                                                        //poNum = sku = customerAccountNumber = fmupc = esn = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                        string[] arr = line.Split(',');
                                                        try
                                                        {
                                                            assignESN = new EsnUploadNew();
                                                            //if (arr.Length > 4)
                                                            //{
                                                            SNo = arr[0].Trim();
                                                            batch = arr[1].Trim();
                                                            //if (arr.Length > 2)
                                                                
                                                            //if (!string.IsNullOrEmpty(esn) && !IsWholeNumber(esn))
                                                            //{
                                                            //    sbESN.Append(esn + ",");
                                                            //}
                                                            if (arr.Length > 2)
                                                            {
                                                                    esn = arr[2].Trim();
                                                                    //MeidHex = arr[2].Trim();
                                                                //ViewState["HEX"] = 1;
                                                                //if (!string.IsNullOrEmpty(MeidHex) && !IsWholeNumber(MeidHex))
                                                                //{
                                                                //    sbHex.Append(MeidHex + ",");
                                                                //}
                                                            }
                                                            if(arr.Length > 3)
                                                            {
                                                                imei2 = arr[3].Trim();
                                                            }
                                                            if (arr.Length > 4)
                                                            {
                                                                MeidHex = arr[4].Trim();                                                                
                                                            }
                                                            if (arr.Length > 5)
                                                            {
                                                                MeidDec = arr[5].Trim();
                                                            }
                                                            if (arr.Length > 6)
                                                            {
                                                                Location = arr[6].Trim();
                                                            }
                                                            if (arr.Length > 7)
                                                            {
                                                                MSL = arr[7].Trim();
                                                            }
                                                            if (arr.Length > 8)
                                                            {
                                                                OTKSL = arr[8].Trim();
                                                            }
                                                            if (arr.Length > 9)
                                                            {
                                                                SerialNumber = arr[9].Trim();
                                                            }

                                                            if (arr.Length > 10)
                                                            {
                                                                BoxID = arr[10].Trim();
                                                            }

                                                            if (string.IsNullOrEmpty(esn))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                assignESN.SNo = SNo;
                                                                assignESN.ESN = esn;
                                                                assignESN.IMEI2 = imei2;
                                                                assignESN.MslNumber = batch;
                                                                assignESN.ICC_ID = lteICCID;
                                                                assignESN.MeidHex = MeidHex;
                                                                assignESN.MeidDec = MeidDec;
                                                                assignESN.Location = Location;
                                                                assignESN.MSL = MSL;
                                                                assignESN.OTKSL = OTKSL;
                                                                assignESN.SerialNumber = SerialNumber;
                                                                assignESN.BoxID = BoxID;

                                                                esnList.Add(assignESN);
                                                                SNo = string.Empty;
                                                                esn = string.Empty;
                                                                imei2 = string.Empty;
                                                                MeidHex = string.Empty;
                                                                MeidDec = string.Empty;
                                                                Location = string.Empty;
                                                                MSL = string.Empty;
                                                                OTKSL = string.Empty;
                                                                SerialNumber = string.Empty;
                                                                BoxID = string.Empty;
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

                                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                            {
                                                esnLength = Convert.ToInt32(ViewState["EsnLength"]);
                                                decLength = Convert.ToInt32(ViewState["DecLength"]);
                                                if (esnList.Count <= 5000)
                                                {   //lblCount.Text = "Total count: " + esnList.Count;

                                                    List<ESNAuthorization> ESNs = new List<ESNAuthorization>();
                                                    ESNAuthorization esnInfo = null;
                                                    foreach (EsnUploadNew ITEM in esnList)
                                                    {
                                                        esnInfo = new ESNAuthorization();
                                                       // esnInfo.ESN = ITEM.ESN;
                                                        esnInfo.ESN = ITEM.ESN;
                                                        esnInfo.IMEI2 = ITEM.IMEI2;
                                                        esnInfo.MeidDec = ITEM.MeidDec;
                                                        esnInfo.MeidHex = ITEM.MeidHex;
                                                        esnInfo.MSL = string.IsNullOrEmpty(ITEM.MSL) ? "000000" : ITEM.MSL;
                                                        esnInfo.OTKSL = string.IsNullOrEmpty(ITEM.OTKSL) ? "000000" : ITEM.OTKSL;
                                                        esnInfo.SKU = sku;
                                                        esnInfo.SKUName = sku;
                                                        esnInfo.SWVersion = SWVersion;
                                                        esnInfo.ManfName = DisplayName;
                                                        esnInfo.ManfId = ManufactureCode;

                                                        ESNs.Add(esnInfo);
                                                    }
                                                    List<EsnUploadNew> esnauthList = new List<EsnUploadNew>();
                                                    EsnUploadNew esnAuthInfo = null;

                                                    foreach (EsnUploadNew ITEM in esnList)
                                                    {
                                                        esnAuthInfo = new EsnUploadNew();
                                                        esnAuthInfo.SNo = ITEM.SNo;
                                                        esnAuthInfo.ESN = ITEM.ESN;
                                                        esnAuthInfo.IMEI2 = ITEM.IMEI2;
                                                        esnAuthInfo.MeidDec = ITEM.MeidDec;
                                                        esnAuthInfo.MeidHex = ITEM.MeidHex;
                                                        esnAuthInfo.MSL = ITEM.MSL;
                                                        esnAuthInfo.OTKSL = ITEM.OTKSL;
                                                        esnAuthInfo.Location = ITEM.Location;
                                                        esnAuthInfo.MslNumber = ITEM.MslNumber;
                                                        esnAuthInfo.SerialNumber = ITEM.SerialNumber;
                                                        esnAuthInfo.BoxID = ITEM.BoxID;
                                                        esnauthList.Add(esnAuthInfo);
                                                    }

                                                    Session["esnauthlist"] = esnauthList;


                                                    string validateReturnMessage = "";
                                                    if (esnAuthorizationOperation.ValidateESNs(ESNs, esnLength, decLength, out validateReturnMessage))
                                                    {
                                                        Session["Esns"] = ESNs;
                                                        // lblMsg.Text = "File is ready to generate";
                                                        btnGenerate.Visible = true;
                                                        btnPOSLabel.Visible = true;
                                                        btnGenerateCSV.Visible = true;
                                                        btnUploadValidate.Visible = false;
                                                        int ESNAuthorizationID = 0;
                                                        int SequenceNumber = esnAuthorizationOperation.ESNAuthorizationInsert(ESNs, esnauthList, itemCompanyGUID, userID, kittedItemCompanyGUID, runNumber, PlannedProvisioingDate, out ESNAuthorizationID, out productType);
                                                        ViewState["SequenceNumber"] = SequenceNumber;
                                                        ViewState["productType"] = productType;
                                                        ViewState["ESNAuthorizationID"] = ESNAuthorizationID;

                                                    }
                                                    else
                                                    {
                                                        lblMsg.Text = validateReturnMessage;
                                                    }



                                                }
                                                else
                                                {
                                                    lblMsg.Text = "ESN cannot be greater than 5000";
                                                }

                                            }
                                            else
                                            {
                                                if (esnList != null && esnList.Count == 0)
                                                {
                                                    if (columnsIncorrectFormat)
                                                    {
                                                        if (!string.IsNullOrEmpty(invalidColumns))
                                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                        else
                                                            lblMsg.Text = "File format is not correct";
                                                    }
                                                    else
                                                        lblMsg.Text = "There is no ESN to upload";

                                                }
                                                else
                                                {
                                                    if (esnList != null && esnList.Count > 5000)
                                                    {
                                                        lblMsg.Text = "ESN cannot be greater than 5000";
                                                    }
                                                    if (esnList != null)
                                                    {
                                                        if (columnsIncorrectFormat)
                                                        {
                                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                            //else
                                                            //    lblMsg.Text = "File format is not correct";
                                                        }
                                                        else
                                                            lblMsg.Text = "There is no ESN to upload";
                                                    }
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
                            lblMsg.Text = "Run number is required!";
                    }
                }
                else
                    lblMsg.Text = "Please select SKU";
            }
            else
                lblMsg.Text = "Planned provisioing date!";
        }
        private void ValidateMslESNNew()
        {
            string PlannedProvisioingDate = null;
            if (!string.IsNullOrEmpty(txtShipDate.Text.Trim()))
                PlannedProvisioingDate = txtShipDate.Text.Trim();
            btnPOSLabel.Visible = false;
            int itemCompanyGUID = 0;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            string sku = "", SWVersion = "", ManufactureCode = "", DisplayName = "", PRODUCTtYPE="";
            bool columnsIncorrectFormat = false;
            int esnLength = 0, decLength = 0;
            string runNumber = txtRunNumber.Text.Trim();
            int kittedItemCompanyGUID = 0;
            int userID = 0;
            userID = Convert.ToInt32(Session["UserID"]);
            if (!string.IsNullOrEmpty(PlannedProvisioingDate))
            {
                if (ViewState["sku"] != null)
                {
                    if (ddlKitSKU.SelectedIndex > 0)
                    {
                        sku = ddlKitSKU.SelectedItem.Text;
                        kittedItemCompanyGUID = Convert.ToInt32(ddlKitSKU.SelectedValue);
                    }
                    else
                        sku = Convert.ToString(ViewState["sku"]);
                    SWVersion = Convert.ToString(ViewState["SWVersion"]);
                    ManufactureCode = ViewState["ManufactureCode"] as string;
                    DisplayName = ViewState["DisplayName"] as string;

                    itemCompanyGUID = Convert.ToInt32(ddlRawSKU.SelectedValue);
                    if (itemCompanyGUID > 0)
                    {
                        if (!string.IsNullOrEmpty(runNumber))
                        {
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
                                        System.Text.StringBuilder sbInvalidColumns = new System.Text.StringBuilder();
                                        // System.Text.StringBuilder sbESN = new System.Text.StringBuilder();
                                        // System.Text.StringBuilder sbHex = new System.Text.StringBuilder();
                                        // System.Text.StringBuilder sbDec = new System.Text.StringBuilder();
                                        // System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();
                                        EsnUploadNew assignESN = null;
                                        List<EsnUploadNew> esnList = new List<EsnUploadNew>();

                                        if (extension == ".csv")
                                        {
                                            string[] lines = File.ReadAllLines(fileName);
                                            string row1 = lines[0];
                                            if (!string.IsNullOrEmpty(row1))
                                            {
                                                // i = 1;
                                                row1 = row1.ToLower();

                                                string[] headerArray = row1.Split(',');
                                                if (headerArray.Length == 2 || headerArray.Length <= 10)
                                                {
                                                    if (headerArray[0].Trim() != "seq.no.")
                                                    {
                                                        invalidColumns = headerArray[0];
                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray[1].Trim() != "batch")
                                                    {
                                                        invalidColumns = headerArray[1];
                                                        columnsIncorrectFormat = true;
                                                    }

                                                    if (headerArray[2].Trim() != "esn")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[2] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }

                                                    if (headerArray.Length > 2 && headerArray[3].Trim() != "" && headerArray[3].Trim() != "meidhex")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[3] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray.Length > 4 && headerArray[4].Trim() != "" && headerArray[4].Trim() != "meiddec")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[4] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }


                                                    if (headerArray.Length > 5 && headerArray[5].Trim() != "" && headerArray[5].Trim() != "location")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[5] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray.Length > 6 && headerArray[6].Trim() != "" && headerArray[6].Trim() != "msl")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[6] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }

                                                    if (headerArray.Length > 7 && headerArray[7].Trim() != "" && headerArray[7].Trim() != "otksl")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[7] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray.Length > 8 && headerArray[8].Trim() != "" && headerArray[8].Trim() != "serialnumber")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[8] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray.Length > 9 && headerArray[9].Trim() != "" && headerArray[9].Trim() != "boxid")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[9] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }

                                                    invalidColumns = sbInvalidColumns.ToString();


                                                }
                                                else
                                                {
                                                    columnsIncorrectFormat = true;
                                                    invalidColumns = string.Empty;
                                                }
                                            }
                                            var list = new List<string>(lines);
                                            list.RemoveAt(0);
                                            lines = list.ToArray();
                                            Parallel.ForEach(lines, line =>
                                            {
                                                // string line;
                                                string SNo, esn, batch, lteICCID, MeidHex, MeidDec, Location, MSL, OTKSL, SerialNumber, BoxID;//, uploaddate;
                                                int i = 0;

                                                // while ((line = sr.ReadLine()) != null)
                                                {
                                                    //else if (!string.IsNullOrEmpty(line) && i > 0)
                                                    {
                                                        SNo = esn = batch = lteICCID = MeidHex = MeidDec = Location = MSL = OTKSL = SerialNumber = BoxID = string.Empty;
                                                        //poNum = sku = customerAccountNumber = fmupc = esn = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                        string[] arr = line.Split(',');
                                                        try
                                                        {
                                                            assignESN = new EsnUploadNew();
                                                            //if (arr.Length > 4)
                                                            //{
                                                            SNo = arr[0].Trim();
                                                            batch = arr[1].Trim();
                                                            //if (arr.Length > 2)

                                                            //if (!string.IsNullOrEmpty(esn) && !IsWholeNumber(esn))
                                                            //{
                                                            //    sbESN.Append(esn + ",");
                                                            //}
                                                            if (arr.Length > 2)
                                                            {
                                                                esn = arr[2].Trim();
                                                                //MeidHex = arr[2].Trim();
                                                                //ViewState["HEX"] = 1;
                                                                //if (!string.IsNullOrEmpty(MeidHex) && !IsWholeNumber(MeidHex))
                                                                //{
                                                                //    sbHex.Append(MeidHex + ",");
                                                                //}
                                                            }
                                                            if (arr.Length > 3)
                                                            {
                                                                MeidHex = arr[3].Trim();
                                                                //if (!string.IsNullOrEmpty(MeidDec) && !IsWholeNumber(MeidDec))
                                                                //{
                                                                //    sbDec.Append(MeidDec + ",");
                                                                //}
                                                            }
                                                            if (arr.Length > 4)
                                                            {
                                                                MeidDec = arr[4].Trim();
                                                            }
                                                            if (arr.Length > 5)
                                                            {
                                                                Location = arr[5].Trim();
                                                            }
                                                            if (arr.Length > 6)
                                                            {
                                                                MSL = arr[6].Trim();
                                                            }
                                                            if (arr.Length > 7)
                                                            {
                                                                OTKSL = arr[7].Trim();
                                                            }
                                                            if (arr.Length > 8)
                                                            {
                                                                SerialNumber = arr[8].Trim();
                                                            }

                                                            if (arr.Length > 9)
                                                            {
                                                                BoxID = arr[9].Trim();
                                                            }

                                                            if (string.IsNullOrEmpty(esn))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                assignESN.SNo = SNo;
                                                                assignESN.ESN = esn;
                                                                assignESN.MslNumber = batch;
                                                                assignESN.ICC_ID = lteICCID;
                                                                assignESN.MeidHex = MeidHex;
                                                                assignESN.MeidDec = MeidDec;
                                                                assignESN.Location = Location;
                                                                assignESN.MSL = MSL;
                                                                assignESN.OTKSL = OTKSL;
                                                                assignESN.SerialNumber = SerialNumber;
                                                                assignESN.BoxID = BoxID;

                                                                esnList.Add(assignESN);
                                                                SNo = string.Empty;
                                                                esn = string.Empty;
                                                                MeidHex = string.Empty;
                                                                MeidDec = string.Empty;
                                                                Location = string.Empty;
                                                                MSL = string.Empty;
                                                                OTKSL = string.Empty;
                                                                SerialNumber = string.Empty;
                                                                BoxID = string.Empty;
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

                                                //sr.Close();
                                            });

                                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                            {
                                                esnLength = Convert.ToInt32(ViewState["EsnLength"]);
                                                decLength = Convert.ToInt32(ViewState["DecLength"]);
                                                if (esnList.Count <= 5000)
                                                {   //lblCount.Text = "Total count: " + esnList.Count;

                                                    List<ESNAuthorization> ESNs = new List<ESNAuthorization>();
                                                    ESNAuthorization esnInfo = null;
                                                    foreach (EsnUploadNew ITEM in esnList)
                                                    {
                                                        esnInfo = new ESNAuthorization();
                                                        // esnInfo.ESN = ITEM.ESN;
                                                        esnInfo.ESN = ITEM.ESN;
                                                        esnInfo.MeidDec = ITEM.MeidDec;
                                                        esnInfo.MeidHex = ITEM.MeidHex;
                                                        esnInfo.MSL = string.IsNullOrEmpty(ITEM.MSL) ? "000000" : ITEM.MSL;
                                                        esnInfo.OTKSL = string.IsNullOrEmpty(ITEM.OTKSL) ? "000000" : ITEM.OTKSL;
                                                        esnInfo.SKU = sku;
                                                        esnInfo.SKUName = sku;
                                                        esnInfo.SWVersion = SWVersion;
                                                        esnInfo.ManfName = DisplayName;
                                                        esnInfo.ManfId = ManufactureCode;

                                                        ESNs.Add(esnInfo);
                                                    }
                                                    List<EsnUploadNew> esnauthList = new List<EsnUploadNew>();
                                                    EsnUploadNew esnAuthInfo = null;

                                                    foreach (EsnUploadNew ITEM in esnList)
                                                    {
                                                        esnAuthInfo = new EsnUploadNew();
                                                        esnAuthInfo.SNo = ITEM.SNo;
                                                        esnAuthInfo.ESN = ITEM.ESN;
                                                        esnAuthInfo.MeidDec = ITEM.MeidDec;
                                                        esnAuthInfo.MeidHex = ITEM.MeidHex;
                                                        esnAuthInfo.MSL = ITEM.MSL;
                                                        esnAuthInfo.OTKSL = ITEM.OTKSL;
                                                        esnAuthInfo.Location = ITEM.Location;
                                                        esnAuthInfo.MslNumber = ITEM.MslNumber;
                                                        esnAuthInfo.SerialNumber = ITEM.SerialNumber;
                                                        esnAuthInfo.BoxID = ITEM.BoxID;
                                                        esnauthList.Add(esnAuthInfo);
                                                    }

                                                    Session["esnauthlist"] = esnauthList;


                                                    string validateReturnMessage = "";
                                                    if (esnAuthorizationOperation.ValidateESNs(ESNs, esnLength, decLength, out validateReturnMessage))
                                                    {
                                                        Session["Esns"] = ESNs;
                                                        // lblMsg.Text = "File is ready to generate";
                                                        btnGenerate.Visible = true;
                                                        btnPOSLabel.Visible = true;
                                                        btnGenerateCSV.Visible = true;
                                                        btnUploadValidate.Visible = false;
                                                        int ESNAuthorizationID = 0;
                                                        int SequenceNumber = esnAuthorizationOperation.ESNAuthorizationInsert(ESNs, esnauthList, itemCompanyGUID, userID, kittedItemCompanyGUID, runNumber, PlannedProvisioingDate, out ESNAuthorizationID, out PRODUCTtYPE);
                                                        ViewState["SequenceNumber"] = SequenceNumber;
                                                        ViewState["ESNAuthorizationID"] = ESNAuthorizationID;

                                                    }
                                                    else
                                                    {
                                                        lblMsg.Text = validateReturnMessage;
                                                    }



                                                }
                                                else
                                                {
                                                    lblMsg.Text = "ESN cannot be greater than 5000";
                                                }

                                            }
                                            else
                                            {
                                                if (esnList != null && esnList.Count == 0)
                                                {
                                                    if (columnsIncorrectFormat)
                                                    {
                                                        if (!string.IsNullOrEmpty(invalidColumns))
                                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                        else
                                                            lblMsg.Text = "File format is not correct";
                                                    }
                                                    else
                                                        lblMsg.Text = "There is no ESN to upload";

                                                }
                                                else
                                                {
                                                    if (esnList != null && esnList.Count > 5000)
                                                    {
                                                        lblMsg.Text = "ESN cannot be greater than 5000";
                                                    }
                                                    if (esnList != null)
                                                    {
                                                        if (columnsIncorrectFormat)
                                                        {
                                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                            //else
                                                            //    lblMsg.Text = "File format is not correct";
                                                        }
                                                        else
                                                            lblMsg.Text = "There is no ESN to upload";
                                                    }
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
                            lblMsg.Text = "Run number is required!";
                    }
                }
                else
                    lblMsg.Text = "Please select SKU";
            }
            else
                lblMsg.Text = "Planned provisioing date!";
        }


        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            ddlKitSKU.Items.Clear();
            ddlRawSKU.Items.Clear();
            dpCompany.SelectedIndex = 0;
            txtRunNumber.Text = "";
            btnGenerate.Visible = false;
            btnGenerateCSV.Visible = false;
            btnUploadValidate.Visible = true;
        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKitSKU.Items.Clear();
            ddlRawSKU.Items.Clear();
            lblMsg.Text = "";
            int companyID = 0;
            bool IsESNRequired = true;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                List<RawSKUInfo> skuList = esnAuthorizationOperation.GetCompanyRawSKUs(companyID, IsESNRequired);
                if(skuList != null && skuList.Count > 0)
                {

                    ddlRawSKU.DataSource = skuList;
                    ddlRawSKU.DataTextField = "SKU";
                    ddlRawSKU.DataValueField = "ItemCompanyGUID";
                    ddlRawSKU.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "0");
                    ddlRawSKU.Items.Insert(0, item);

                    ViewState["sku"] = skuList[0].SKU;

                    ViewState["SWVersion"] = skuList[0].SWVersion;
                    ViewState["EsnLength"] = skuList[0].EsnLength;
                    ViewState["DecLength"] = skuList[0].DecLength;
                }
            }
        }

        protected void ddlRawSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKitSKU.Items.Clear();
            lblKittedSKU.Text = "Kitted SKU:";

            // ddlRawSKU.Items.Clear();
            lblMsg.Text = "";
            int ItemCompanyGUID = 0;
            //bool IsESNRequired = true;
            if (ddlRawSKU.SelectedIndex > 0)
            {
                ItemCompanyGUID = Convert.ToInt32(ddlRawSKU.SelectedValue);
                List<KittedSKUs> skuList = esnAuthorizationOperation.GetKittedSKUByRawSKU(ItemCompanyGUID);
                if (skuList != null && skuList.Count > 0)
                {
                    ddlKitSKU.DataSource = skuList;
                    ddlKitSKU.DataTextField = "SKU";
                    ddlKitSKU.DataValueField = "ItemCompanyGUID";
                    ddlKitSKU.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "0");
                    ddlKitSKU.Items.Insert(0, item);

                    if (skuList.Count == 1)
                        ddlKitSKU.SelectedIndex = 1;

                    ViewState["SWVersion"] = skuList[0].SWVersion;
                    ViewState["sku"] = skuList[0].SKU;
                    ViewState["ManufactureCode"] = skuList[0].ManufactureCode;
                    ViewState["DisplayName"] = skuList[0].DisplayName;
                    
                }
                else
                {
                    lblKittedSKU.Text = "";

                }
            }

        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSVFile();
        }

        private void GenerateCSVFile()
        {
            lblMsg.Text = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("Seq.No.,BATCH,ESN1,ESN2,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber,BoxID" + Environment.NewLine);
            int itr = 1;
            int totalrows = 10;
            for (int i=1;i<=totalrows;i++)
            {
                sb.Append(i + "," + "" + "," + "," + "" + "," + "" + "," + "" + "," + "" + "," + "" + "," + "" + "," + "" + "," + "" + Environment.NewLine);
            }
            //string string2CSV = "Seq.No.,BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber,BoxID" + Environment.NewLine;
            string string2CSV = sb.ToString(); //"Seq.No.,BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber,BoxID" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventoryReceive.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }

        private string fileStoreLocation = "~/UploadedData/";
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

        protected void btnGenerateCSV_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            string fileName = "nms_bounce_dish_to_tmo_";
            string runNumber = txtRunNumber.Text.Trim();
            DateTime currentDate = DateTime.Now;

           string date =  Convert.ToDateTime(currentDate).ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(runNumber))
            {
                if (Session["Esns"] != null)
                {
                    List<ESNAuthorization> ESNs = Session["Esns"] as List<ESNAuthorization>;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    fileName = fileName + runNumber + "_" + ESNs.Count + "_" + date + ".txt";

                    //string string2CSV = "";// "BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber" + Environment.NewLine;
                    //sb.Append("ESN" + Environment.NewLine);
                    foreach (ESNAuthorization item in ESNs)
                    {
                        sb.Append(item.ESN + Environment.NewLine);
                    }

                    string string2CSV = sb.ToString();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();

                }
                else
                {
                    lblMsg.Text = "No data found.";
                }
            }
            else
                lblMsg.Text = "Run number is required!";
        }
        
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (Session["Esns"] != null)
            {
                string productType = "";
                List<ESNAuthorization> ESNs = Session["Esns"] as List<ESNAuthorization>;
                List<EsnUploadNew> esnauthList = Session["esnauthlist"] as List<EsnUploadNew>;
                int ItemCompanyGUID = 0, userID = 0, KittedItemCompanyGUID=0;
                userID = Convert.ToInt32(Session["UserID"]);

                if (ddlRawSKU.SelectedIndex > 0)
                {
                    string validateReturnMessage = "";
                   // if (EsnAuthorizationOperation.ValidateFile(ESNs, out validateReturnMessage))
                    {

                        //ItemCompanyGUID = Convert.ToInt32(ddlRawSKU.SelectedValue);
                        //if(ddlKitSKU.SelectedIndex > 0)
                        //{
                        //    KittedItemCompanyGUID = Convert.ToInt32(ddlKitSKU.SelectedValue);
                        //}

                        int SequenceNumber = 0; //EsnAuthorizationOperation.ESNAuthorizationInsert(ESNs, esnauthList, ItemCompanyGUID, userID, KittedItemCompanyGUID);

                        if (ViewState["SequenceNumber"] != null)
                            SequenceNumber = Convert.ToInt32(ViewState["SequenceNumber"]);
                        if (ViewState["productType"] != null)
                            productType = Convert.ToString(ViewState["productType"]);
                        var memoryStream = new MemoryStream();
                        //   System.Xml.XmlWriter write =  new   ;
                        string fileName;
                        string filePrefix = "spdish";
                        //string filePrefix = "spappledsh";
                        string transDate;
                        DateTime currentUtcDateTime = DateTime.UtcNow;
                        DateTime currentCSTDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

                        transDate = currentCSTDateTime.ToString("yyyy-MM-dd");

                        // string fileName = filePrefix + "_" + transDate + "_" + edfFileInfo.fileSequence.ToString() + ".xml";
                        DateTime dt = DateTime.Now;
                        string currentDate = dt.ToString("yyyy-MM-dd");

                        // Int64 fileSequence = dt.Ticks;

                        string filePath = Server.MapPath("~/UploadedData/");

                        if (ESNs != null && ESNs.Count > 0)
                        {
                            if (esnAuthorizationOperation.ValidateUEDFFileData(ESNs, SequenceNumber, out validateReturnMessage))
                            {   // transDate = currentDate.Replace("-", "");
                                transDate = transDate.Replace("-", "");
                                fileName = filePrefix + "_" + transDate + "_" + SequenceNumber.ToString() + ".xml";
                                filePath = filePath + fileName;

                                XElement xmlElement = esnAuthorizationOperation.CreateAuthorizationFile(ESNs, SequenceNumber.ToString(), transDate, productType);

                                xmlElement.Save(filePath);

                                ddlKitSKU.Items.Clear();
                                ddlRawSKU.Items.Clear();
                                dpCompany.SelectedIndex = 0;

                                btnGenerate.Visible = false;
                                btnGenerateCSV.Visible = false;
                                btnUploadValidate.Visible = true;

                                lblMsg.Text = "Generated successfully";
                                // tring strFullPath = Server.MapPath("~/temp.xml");
                                string strContents = null;
                                System.IO.StreamReader objReader = default(System.IO.StreamReader);
                                objReader = new System.IO.StreamReader(filePath);
                                strContents = objReader.ReadToEnd();
                                objReader.Close();

                                string attachment = "attachment; filename=" + fileName;
                                Response.ClearContent();
                                Response.ContentType = "application/xml";
                                Response.AddHeader("content-disposition", attachment);
                                Response.Write(strContents);
                                Response.End();
                            }
                            else
                                lblMsg.Text = validateReturnMessage;

                        }
                        else
                        {
                            lblMsg.Text = "No data found.";
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                lblMsg.Text = "No data found.";
            }
        }

        protected void btnPOSLabel_Click(object sender, EventArgs e)
        {
            if (ViewState["ESNAuthorizationID"] != null)
            {
                SV.Framework.Fulfillment.DishLabelOperations DishLabelOperations = SV.Framework.Fulfillment.DishLabelOperations.CreateInstance<SV.Framework.Fulfillment.DishLabelOperations>();

                int ESNAuthorizationID = Convert.ToInt32(ViewState["ESNAuthorizationID"]);
                int companyID = Convert.ToInt32(dpCompany.SelectedValue);

                List<EsnUploadNew> esnauthList = Session["esnauthlist"] as List<EsnUploadNew>;
                DataTable dt = DishLabelOperations.ESNDataNew(esnauthList);
                List<SV.Framework.LabelGenerator.PosKitInfo> posKITs = new List<SV.Framework.LabelGenerator.PosKitInfo>();
                SV.Framework.LabelGenerator.PosKitInfo posKitInfo = default;
                List<SV.Framework.LabelGenerator.KitBoxInfo> kitBoxInfos = default;
                SV.Framework.LabelGenerator.KitBoxInfo kitBoxInfo = default;

                List<SV.Framework.Models.Fulfillment.PosKitInfo> posKITsdb = DishLabelOperations.GetPosLabels(companyID, ESNAuthorizationID, dt);
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                if (posKITsdb != null && posKITsdb.Count > 0)
                {
                    foreach (SV.Framework.Models.Fulfillment.PosKitInfo item in posKITsdb)
                    {
                        kitBoxInfos = new List<SV.Framework.LabelGenerator.KitBoxInfo>();

                        posKitInfo = new SV.Framework.LabelGenerator.PosKitInfo();
                        posKitInfo.CompanyName = item.CompanyName;
                        posKitInfo.ESN = item.ESN;
                        posKitInfo.IMEI2 = item.IMEI2;
                        posKitInfo.HEX = item.HEX;
                        posKitInfo.HWVersion = item.HWVersion;
                        posKitInfo.ICCID = item.ICCID;
                        posKitInfo.ItemName = item.ItemName;
                        posKitInfo.MEID = item.MEID;
                        posKitInfo.OSType = item.OSType;
                        posKitInfo.ProductType = item.ProductType;
                        posKitInfo.SerialNum = item.SerialNum;
                        posKitInfo.ShipDate = item.ShipDate;
                        posKitInfo.SKU = item.SKU;
                        posKitInfo.SWVersion = item.SWVersion;
                        posKitInfo.UPC = item.UPC;
                        foreach (SV.Framework.Models.Fulfillment.KitBoxInfo kitBoxInfodb in item.KitBoxList)
                        {
                            kitBoxInfo = new SV.Framework.LabelGenerator.KitBoxInfo();
                            kitBoxInfo.DisplayName = kitBoxInfodb.DisplayName;
                            kitBoxInfo.OriginCountry = kitBoxInfodb.OriginCountry;
                            kitBoxInfos.Add(kitBoxInfo);
                        }
                        posKitInfo.KitBoxList = kitBoxInfos;

                        posKITs.Add(posKitInfo);
                    }
                    if (posKITs != null && posKITs.Count > 0)
                    {
                        string ProductType = posKITs[0].ProductType;
                        MemoryStream memSt = null;// = new MemoryStream();

                        if (ProductType.ToUpper() == "H5")
                        {
                            SV.Framework.LabelGenerator.H5LabelOperation h5LabelOperation = new SV.Framework.LabelGenerator.H5LabelOperation();

                            memSt = h5LabelOperation.POSKITLabelPdfTarCode(posKITs);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                        }
                        else  if (posKITs[0].OSType.ToUpper() == "ANDROID")
                        {
                            SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();

                            memSt = h3LabelOperation.POSKITLabelPdfTarCode(posKITs);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            //var memSt = slabel.ExportToPDF(models);
                            
                        }
                        else
                        {
                            memSt = dishLabelOperation.POSKITLabelPdfTarCode(posKITs);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            //var memSt = slabel.ExportToPDF(models);
                            //if (memSt != null)
                            //{
                            //    string fileType = ".pdf";
                            //    string filename = DateTime.Now.Ticks + fileType;
                            //    Response.Clear();
                            //    //Response.ContentType = "application/pdf";
                            //    Response.ContentType = "application/octet-stream";
                            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            //    Response.Buffer = true;
                            //    Response.Clear();
                            //    var bytes = memSt.ToArray();
                            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                            //    Response.OutputStream.Flush();
                            //    lblMsg.Text = "Label generated successfully.";
                            //}
                        }
                        if (memSt != null)
                        {
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();
                            lblMsg.Text = "Label generated successfully.";
                        }

                    }
                    else
                        lblMsg.Text = "No record found";
                }
                else
                    lblMsg.Text = "No record found";
            }
        }
    }
    
}