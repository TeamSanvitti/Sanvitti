using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace avii.Admin
{
    public partial class frmMSL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                pnlUploadESN.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtMsl.Text = string.Empty;
           // txtAKEY.Text = string.Empty;
           // txtMEID.Text = string.Empty;
            txtESN.Text = string.Empty;
            lblMsg.Text = string.Empty;
            btnDelete.Visible = false;
            btnExport.Visible = false;
            gvMSL.DataSource = null;
            gvMSL.DataBind();
            rptESN.DataSource = null;
            rptESN.DataBind();
            dpMslSelect.SelectedIndex = 0;
            lblUploadMsg.InnerText = string.Empty;
            pnlUploadESN.Visible = false;
            //chkRepository.Visible = true;
            chkRepository.Checked = false;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = false;
            string ESN = string.Empty;
            int rmaGUID, POID;
            List<clsEsnxml> esnList = new List<clsEsnxml>();
            clsEsnxml esnData = null;
            Label lblESN;
            Label lblPO;
            Label lblRMA;
            foreach (RepeaterItem item in rptESN.Items)
            {
                lblESN = item.FindControl("lblESN") as Label;
                lblPO = item.FindControl("lblPO") as Label;
                lblRMA = item.FindControl("lblRma") as Label;
                
                if (((CheckBox)item.FindControl("chkESN")).Checked)
                {
                    int.TryParse(lblPO.Text.Trim(), out POID);
                    int.TryParse(lblRMA.Text.Trim(), out rmaGUID);
                    if (POID == 0 && rmaGUID == 0)
                    {
                        bSelected = true;
                        esnData = new clsEsnxml();
                        esnData.esn = lblESN.Text.Trim();
                        esnData.FmUpc = string.Empty;
                        esnData.ifMissing = 0;
                        esnData.MDN = string.Empty;
                        esnData.MSID = string.Empty;
                        esnData.MslNumber = string.Empty;
                        esnData.Pod_id = 0;
                        esnList.Add(esnData);
                    }
                }
            }
            if (bSelected)
            {
                inventoryReportOperation.DeleteEsnsWithXls(esnList);
                string filepath = string.Empty;
                if (ViewState["filepath"] != null)
                {
                    filepath = ViewState["filepath"].ToString();

                    BindEsnExcel(filepath);
                }
                lblMsg.Text = "Deleted successfuly";
            }
            else
            {
                lblMsg.Text = "Can not delete ESN(s) having Fulfillment order or RMA.";
            }
        }
        private void BindEsnExcel(string filePath)
        {
            try
            {
                SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

                List<SV.Framework.Models.Fulfillment.EsnList> esnList = new List<SV.Framework.Models.Fulfillment.EsnList>();
                SV.Framework.Models.Fulfillment.EsnList esnData = null;

                DataTable dataTable = new DataTable();
                String sConnectionString =
                                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                    "Data Source=" + filePath + ";" +
                                    "Extended Properties=Excel 12.0;";

                using (OleDbConnection objConn = new OleDbConnection(sConnectionString))
                {
                    objConn.Open();
                    OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
                    OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                    objAdapter1.SelectCommand = objCmdSelect;
                    objAdapter1.Fill(dataTable);
                    objConn.Close();
                }
                if (dataTable.Columns.Count <= 3)
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow item in dataTable.Rows)
                        {
                            if (item["esn"].ToString().Trim() != null && item["esn"].ToString().Trim() != string.Empty)
                            {
                                esnData = new SV.Framework.Models.Fulfillment.EsnList();
                                esnData.ESN = item["esn"].ToString().Trim();
                                //esnData.MEID = dataTable.Columns.Count > 1 ? item["meid"].ToString().Trim() : string.Empty;
                                //esnData.AKEY = dataTable.Columns.Count > 2 ? item["akey"].ToString().Trim() : string.Empty;

                                esnList.Add(esnData);
                            }
                        }


                        //dataTable = avii.Classes.clsInventoryDB.GetESN_XML(esnList);
                        //if (dataTable.Rows.Count > 0)
                        //{
                        //    rptESN.DataSource = dataTable;
                        //    btnDelete.Visible = true;
                        //}
                        //else
                        //{
                        //    rptESN.DataSource = null;
                        //    pnlUploadESN.Visible = false;
                        //    lblMsg.Text = "No records exists";
                        //}
                        //rptESN.DataBind();
                        //gvMSL.DataSource = null;
                        //gvMSL.DataBind();

                        ///new 27 MARCH 2020
                        ///
                        List<EsnInfo> esnInfoList = null;
                        esnInfoList = inventoryReportOperation.GetESNQuery(esnList);

                        //DataTable dataTable = avii.Classes.clsInventoryDB.GetMslList(avii.Classes.ESNSearch.ESN, esn, multiESN, meid, akey);
                        //if (dataTable != null && dataTable.Rows.Count > 0)
                        //{
                        //    gvMSL.DataSource = dataTable;
                        //    btnDelete.Visible = true;
                        //}
                        if (esnInfoList != null && esnInfoList.Count > 0)
                        {
                            gvMSL.DataSource = esnInfoList;
                            btnDelete.Visible = false;
                            btnExport.Visible = true;
                            Session["esnInfoList"] = esnInfoList;
                        }
                        else
                        {
                            btnExport.Visible = false;
                            btnDelete.Visible = false;
                            gvMSL.DataSource = null;
                            lblMsg.Text = "No record exists";
                        }
                        gvMSL.DataBind();
                        rptESN.DataSource = null;
                        rptESN.DataBind();
                    }
                    else
                    {
                        gvMSL.DataSource = null;
                        gvMSL.DataBind();

                        rptESN.DataSource = null;
                        rptESN.DataBind();
                        pnlUploadESN.Visible = false;
                        btnDelete.Visible = false;
                        lblMsg.Text = "No records exists";
                    }
                }
                else
                    lblMsg.Text = "Uploaded file does not have correct Format";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Sheet1$"))
                    lblMsg.Text = "File sheet name is not valid, please use Sheet1";
                else
                    lblMsg.Text = ex.Message;
            }
        }
        private void BindEsn(string filePath)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            List<clsEsnxml> esnList = new List<clsEsnxml>();
            clsEsnxml esnData = null;
            
            DataTable dataTable = new DataTable();
            String sConnectionString =
                                "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=" + filePath + ";" +
                                "Extended Properties=Excel 8.0;";

            using (OleDbConnection objConn = new OleDbConnection(sConnectionString))
            {
                objConn.Open();
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                objAdapter1.SelectCommand = objCmdSelect;
                objAdapter1.Fill(dataTable);
                objConn.Close();
            }
            if (dataTable.Rows.Count > 0)
            {

                foreach (DataRow item in dataTable.Rows)
                {
                    if (item["esn"].ToString().Trim() != null && item["esn"].ToString().Trim() != string.Empty)
                    {
                        esnData = new clsEsnxml();
                        esnData.esn = item["esn"].ToString().Trim();
                        esnData.FmUpc = string.Empty;
                        esnData.ifMissing = 0;
                        esnData.MDN = string.Empty;
                        esnData.MSID = string.Empty;
                        esnData.MslNumber = string.Empty;
                        esnData.Po_id = 0;
                        esnData.Pod_id = 0;
                        esnList.Add(esnData);
                    }
                }


                dataTable = inventoryReportOperation.GetESNList(esnList);
                if (dataTable.Rows.Count > 0)
                {
                    rptESN.DataSource = dataTable;
                }
                else
                {
                    rptESN.DataSource = null;
                    pnlUploadESN.Visible = false;
                    lblMsg.Text = "No records exists";
                }
                rptESN.DataBind();
                gvMSL.DataSource = null;
                gvMSL.DataBind();
            }
        }
        private void GetESNfromXLS()
        {
            try
            {
                string fileDir;
                string filePath;
                string fileExtension;
                bool flag = false;
                string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] as string;


                fileDir = Server.MapPath("~");
                filePath = fileDir + uploadFolder + flUpload.FileName;
                fileExtension = System.IO.Path.GetExtension(flUpload.FileName).Replace(".", string.Empty).ToLower();


                if (fileExtension == "xls" || fileExtension == "xlsx")
                    flag = true;



                if (flag)
                {

                    try
                    {
                        ViewState["filepath"] = filePath;
                        flUpload.PostedFile.SaveAs(filePath);
                        BindEsnExcel(filePath);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid File Extension!";

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
           // return dataTable;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            try
            {
                List<SV.Framework.Models.Fulfillment.EsnList> esnsList = new List<SV.Framework.Models.Fulfillment. EsnList>();

                lblMsg.Text = string.Empty;
                gvMSL.DataSource = null;
                gvMSL.DataBind();
                string esn;
                esn = (txtESN.Text.Trim().Length > 0 ? txtESN.Text.Trim() : string.Empty);
                esn = esn.Replace(" ", "");
                string[] esnArr = esn.Split(',');                

                if (dpMslSelect.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(esn))// && string.IsNullOrEmpty(meid) && string.IsNullOrEmpty(akey))
                    {
                        lblMsg.Text = "Please select the search criteria";
                        pnlUploadESN.Visible = false;
                        return;

                    }
                }
                else
                {
                    if (!flUpload.HasFile)
                    {
                        pnlUploadESN.Visible = true;

                        lblMsg.Text = "Please select the file";
                        return;

                    }
                }
                string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] as string;

                if (flUpload.HasFile)
                {
                    pnlUploadESN.Visible = true;
                    if (!UploadESNs(out esnsList))
                    {
                        return;
                    }
                }
                else
                {
                    pnlUploadESN.Visible = false;

                    SV.Framework.Models.Fulfillment.EsnList esnObj;
                    if (esnArr.Length > 0)
                    {
                        for (int i = 0; i < esnArr.Length; i++)
                        {

                            esnObj = new SV.Framework.Models.Fulfillment.EsnList();
                            //esnObj.AKEY = akey;
                            esnObj.ESN = esnArr[i].Trim();
                            //esnObj.MEID = meid;
                            esnsList.Add(esnObj);
                        }
                    }
                    else
                    {

                        esnObj = new SV.Framework.Models.Fulfillment.EsnList();
                        //esnObj.AKEY = akey;
                        esnObj.ESN = esn;
                        //esnObj.MEID = meid;
                        esnsList.Add(esnObj);
                    }
                }
                    
                    List<EsnInfo> esnInfoList = null;
                    if (chkRepository.Checked)
                        esnInfoList = inventoryReportOperation.GetESNQueryWithRepository(esnsList);
                    else
                        esnInfoList = inventoryReportOperation.GetESNQuery(esnsList);

                    if (esnInfoList != null && esnInfoList.Count > 0)
                    {
                        gvMSL.DataSource = esnInfoList;
                        btnDelete.Visible = false;
                        btnExport.Visible = true;
                        Session["esnInfoList"] = esnInfoList;
                    }
                    else
                    {
                        btnExport.Visible = false;
                        btnDelete.Visible = false;
                        gvMSL.DataSource = null;
                        lblMsg.Text = "No record exists";
                    }
                    gvMSL.DataBind();
                    rptESN.DataSource = null;
                    rptESN.DataBind();
                
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }
        protected void imgViewRMA_Click(object sender, CommandEventArgs e)
        {
            Control tmp = LoadControl("~/controls/RMADetails.ascx");
            avii.Controls.RMADetails ctlRMADetail = tmp as avii.Controls.RMADetails;
            pnlRMA.Controls.Clear();
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
           // ViewState["rmaGUID"] = rmaGUID;
            if (tmp != null)
            {

                ctlRMADetail.BindRMA(rmaGUID, false);
            }
            pnlRMA.Controls.Add(ctlRMADetail);
            ModalPopupExtender2.Show();
        }
        protected void imgViewPO_Click(object sender, CommandEventArgs e)
        {
            Control tmp2 = LoadControl("~/controls/PODetails.ascx");
            avii.Controls.PODetails ctlPODetails = tmp2 as avii.Controls.PODetails;
            pnlPO.Controls.Clear();
            int poID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {
                
                ctlPODetails.BindPO(poID, false);
            }
            pnlPO.Controls.Add(ctlPODetails);
            ModalPopupExtender1.Show();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            if (Session["esnInfoList"] != null)
            {
                List<EsnInfoToCSV> esnCSVList = null;
                EsnInfoToCSV csvESN = null;
                List<EsnInfo> esnInfoList = Session["esnInfoList"] as List<EsnInfo>;
                if(esnInfoList!=null && esnInfoList.Count > 0)
                {
                    esnCSVList = new List<EsnInfoToCSV>();
                    foreach (EsnInfo item in esnInfoList)
                    {
                        csvESN = new EsnInfoToCSV();
                        csvESN.BatchNumber = item.BatchNumber;
                        csvESN.CustomerName = item.CustomerName;
                        csvESN.ESN = item.ESN;
                        csvESN.ICC_ID = item.ICC_ID;
                        csvESN.NewSKU = item.NewSKU;
                        csvESN.ProductCode = item.Item_Code;
                        csvESN.PurchaseOrderNumber = item.PurchaseOrderNumber;
                        csvESN.RmaNumber = item.RmaNumber;
                        csvESN.SalesOrderNumber = item.AerovoiceSalesOrderNumber;
                        csvESN.SKU = item.SKU;
                        csvESN.ContainerID = item.ContainerID;
                        esnCSVList.Add(csvESN);
                    }
                    string string2CSV = esnCSVList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ESNQuery.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
            else
                lblMsg.Text = "Session expired!";
        }
        private bool UploadESNs(out List<SV.Framework.Models.Fulfillment.EsnList> esnsList)
        {
            esnsList = new List<SV.Framework.Models.Fulfillment.EsnList>();
            SV.Framework.Models.Fulfillment.EsnList esnObj;
            string customerAccountNumber = string.Empty, trackingNumber = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            bool columnsIncorrectFormat = false;
            string invalidColumns = string.Empty;
            bool IsValid = true;
            if (flUpload.HasFile)
            {
                string fileName = UploadFile(flUpload);
                string extension = Path.GetExtension(flUpload.PostedFile.FileName);
                extension = extension.ToLower();

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
                                        if (string.IsNullOrEmpty(invalidColumns))
                                            invalidColumns = headerArray[0];
                                        else
                                            invalidColumns = invalidColumns + ", " + headerArray[0];
                                        columnsIncorrectFormat = true;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(line) && i > 0)
                                {
                                    esn = string.Empty;
                                    string[] arr = line.Split(',');

                                    if (arr.Length > 0)
                                    {
                                        esn = arr[0].Trim();
                                    }
                                    if (string.IsNullOrEmpty(esn))
                                    {
                                        lblMsg.Text = "Missing required data";
                                        IsValid = false;
                                    }
                                    esnObj = new SV.Framework.Models.Fulfillment.EsnList();

                                    //if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                    //{
                                    //    //uploadEsn = false;
                                    //    throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                    //}
                                    //else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                    //{
                                    //    hshESN.Add(esn, esn);
                                    //}

                                    esnObj.ESN = esn;

                                    esnsList.Add(esnObj);
                                    esn = string.Empty;


                                }
                            }
                            sr.Close();
                            if (columnsIncorrectFormat)
                            {
                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                IsValid = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                        IsValid = false;
                        return IsValid;
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid file!";
                    IsValid = false;
                    return IsValid;
                }
            }
            else
            {
                lblMsg.Text = "Invalid file!";
                IsValid = false;
                return IsValid;
            }
            return IsValid;
        }

        string fileStoreLocation;
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

    }
}
