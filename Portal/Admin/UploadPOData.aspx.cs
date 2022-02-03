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
    public partial class UploadPOData : System.Web.UI.Page
    {
        protected DropDownList ddlData;
        protected FileUpload flnUpload;
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
            }
        }
        
        private void UploadRMAs(int companyID)
        {
            //string[] validFileExtensions = new string[] { "xls" };
            string fileDir;
            string filePath;
            string fileExtension;
            bool flag = false;
            int rmaStatus = 1;
            int userid = 0;
            int createdby = userid;
            int modifiedby = userid;
            string rmaNumber = string.Empty;
            string customerEmail = string.Empty;
            string overrideEmail = string.Empty;
            List<CustomerEmail> custEmail = new List<CustomerEmail>();
                
            List<avii.Classes.RMA> rmaList = new List<avii.Classes.RMA>();
            string custName, address, city, state, zip, phone, email, esn;
            DateTime rmaDate = DateTime.Now;
            
            //int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
            
            try
            {
                custEmail = CustomerEmailOperations.GetCustomerEmailList(companyID);
                var userEmail = (from item in custEmail where item.ModuleGUID.Equals(75) select item).ToList();
                //objRMAcompany = RMAUtility.getUserEmail(companyID);

                if (userEmail != null && userEmail.Count > 0)
                {
                    userid = userEmail[0].UserID;
                    customerEmail = userEmail[0].Email;
                    overrideEmail = userEmail[0].OverrideEmail;
                    if (string.IsNullOrEmpty(overrideEmail) && string.IsNullOrEmpty(customerEmail))
                        customerEmail = userEmail[0].CompanyEmail;
                    createdby = 0;
                    modifiedby = 0;
                }

                string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] as string;
                if (flnUpload.HasFile)
                {
                    fileDir = Server.MapPath("~");
                    filePath = fileDir + uploadFolder + flnUpload.FileName;
                    fileExtension = System.IO.Path.GetExtension(flnUpload.FileName).Replace(".", string.Empty).ToLower();


                    if (fileExtension == "xls" || fileExtension == "xlsx")
                        flag = true;

                    if (flag == true)
                    {
                        try
                        {
                            DataTable rmaTable = new DataTable();
                            List<StoreLocation> stores;
                            flnUpload.PostedFile.SaveAs(filePath);

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
                                objAdapter1.Fill(rmaTable);
                                objConn.Close();
                            }

                            
                            if (rmaTable.Rows.Count > 0)
                            {
                                stores = new List<StoreLocation>();
                                StringBuilder storeIdList = new StringBuilder();
                                for (int i = 0; i < rmaTable.Columns.Count; i++)
                                {
                                    rmaTable.Columns[i].ColumnName = rmaTable.Columns[i].ColumnName.Trim();
                                }
                                //int counter = 0;
                                string checkRmaNumber = string.Empty;
                                Hashtable hshEsnDuplicateCheck = new Hashtable();
                                foreach (DataRow item in rmaTable.Rows)
                                {
                                    try
                                    {
                                        rmaNumber = Convert.ToString(clsGeneral.getColumnData(item, "RmaNumber", string.Empty, false));
                                        
                                        if (!string.IsNullOrEmpty(rmaNumber) && rmaNumber.Trim().Length > 0)
                                        {
                                            //counter = +counter;
                                            rmaNumber = rmaNumber.Trim();

                                            //if (checkRmaNumber != rmaNumber)
                                            //{

                                            //    checkRmaNumber = rmaNumber;
                                            //    counter = 1;
                                            //}

                                            //if (counter > maxEsn)
                                            //{
                                            //    lblMsg.Text = string.Format("Maximum of {0} ESNs are allowed per RMA request", maxEsn);
                                            //    return;
                                            //}
                                            
                                            address = clsGeneral.getColumnData(item, "Address", string.Empty, false) as string;
                                            custName = clsGeneral.getColumnData(item, "ContactName", string.Empty, false) as string; ;
                                            city = clsGeneral.getColumnData(item, "city", string.Empty, false) as string;
                                            zip = Convert.ToString(clsGeneral.getColumnData(item, "zip", string.Empty, false));
                                            state = clsGeneral.getColumnData(item, "state", string.Empty, false) as string;
                                            phone = clsGeneral.getColumnData(item, "phone", string.Empty, false) as string;
                                            email = clsGeneral.getColumnData(item, "email", string.Empty, false) as string;
                                            rmaDate = Convert.ToDateTime(clsGeneral.getColumnData(item, "date", DateTime.Now, false) as string);
                                            esn = clsGeneral.getColumnData(item, "esn", string.Empty, false) as string;
                                            esn = esn.Trim();
                                            if (string.IsNullOrEmpty(address))
                                            {
                                                lblMsg.Text = "Address is requeired";
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(custName))
                                            {
                                                lblMsg.Text = "Contact name is requeired";
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(city))
                                            {
                                                lblMsg.Text = "City is requeired";
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(state))
                                            {
                                                lblMsg.Text = "State is requeired";
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(zip))
                                            {
                                                lblMsg.Text = "Zip is requeired";
                                                return;
                                            } 
                                            if (string.IsNullOrEmpty(phone))
                                            {
                                                lblMsg.Text = "Phone is requeired";
                                                return;
                                            } 
                                            if (string.IsNullOrEmpty(email))
                                            {
                                                lblMsg.Text = "Email is requeired";
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(esn))
                                            {
                                                lblMsg.Text = "ESN is requeired";
                                                return;
                                            }


                                            if (hshEsnDuplicateCheck.ContainsKey(esn))
                                            {

                                                lblMsg.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                                                //ViewState["duplicateESN"] = "1";

                                                return;
                                            }
                                            else
                                            {
                                                hshEsnDuplicateCheck.Add(esn, esn);
                                            }

                                            RMADetail objRMAesn = new avii.Classes.RMADetail();
                                            objRMAesn.ESN = esn;
                                            //if (ViewState["validate"] == null)
                                            //{
                                            ValidateESN(ref objRMAesn, companyID, 0);
                                            //if (objRMAesn.UPC == "RMA Exists")
                                            //{
                                            //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                            //    ViewState["ESNalreadyExists"] = "1";
                                            //}
                                            if (!objRMAesn.AllowDuplicate)
                                            {
                                                lblMsg.Text = string.Format("RMA ({0}) already exists", esn);
                                                return;
                                            }
                                            
                                            if (!objRMAesn.AllowRMA)
                                            {
                                                lblMsg.Text = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", esn, objRMAesn.UPC);
                                                return;
                                                
                                            }

                                            if (checkRmaNumber != rmaNumber)
                                            {

                                                //if (Session["rmadetaillist"] != null)
                                                //{
                                                //    List<RMADetail> rmaDetailList2 = (List<RMADetail>)Session["rmadetaillist"];
                                                //    if (rmaDetailList2.Count > 0)
                                                //    {
                                                //        if (rmaList.Count > 0)
                                                //        {
                                                //            int rmaListIndex = rmaList.Count - 1;
                                                //            rmaList[rmaListIndex].RmaDetails = rmaDetailList2;
                                                //        }
                                                //    }
                                                //}
                                                List<RMADetail> rmaDetailList = new List<RMADetail>();
                                                RMADetail rmaDetailObj = new RMADetail();
                                                rmaDetailObj.ESN = esn;
                                                rmaDetailObj.CallTime = 0;
                                                rmaDetailObj.Notes = string.Empty;
                                                rmaDetailObj.Reason = 0;
                                                rmaDetailObj.StatusID = 1;
                                                rmaDetailObj.rmaDetGUID = 0;
                                                rmaDetailList.Add(rmaDetailObj);
                                                Session["rmadetaillist"] = rmaDetailList;

                                                avii.Classes.RMA rmaObj = new avii.Classes.RMA();

                                                checkRmaNumber = rmaNumber;


                                                rmaObj.RmaNumber = rmaNumber;
                                                rmaObj.Address = address;
                                                rmaObj.RmaContactName = custName;
                                                rmaObj.City = city;
                                                rmaObj.Zip = zip;
                                                rmaObj.State = state;
                                                rmaObj.Phone = phone;
                                                rmaObj.Email = email;
                                                rmaObj.RmaDate = rmaDate;
                                                rmaObj.UserID = userid;
                                                rmaObj.RmaGUID = 0;
                                                rmaObj.RmaStatusID = rmaStatus;
                                                rmaObj.CreatedBy = createdby;
                                                rmaObj.ModifiedBy = modifiedby;
                                                rmaObj.Comment = string.Empty;
                                                rmaObj.AVComments = string.Empty;
                                                if (rmaDetailList.Count > 0)
                                                {
                                                    rmaObj.RmaDetails = rmaDetailList;
                                                    rmaList.Add(rmaObj);
                                                }
                                                //List<RMADetail> rmaDetailList2 = new List<RMADetail>();



                                            }
                                            else
                                            {
                                                List<RMADetail> rmaDetailList = new List<RMADetail>();
                                                if (Session["rmadetaillist"] != null)
                                                    rmaDetailList = (List<RMADetail>)Session["rmadetaillist"];
                                                RMADetail rmaDetailObj = new RMADetail();
                                                rmaDetailObj.ESN = esn;
                                                rmaDetailObj.CallTime = 0;
                                                rmaDetailObj.Notes = string.Empty;
                                                rmaDetailObj.Reason = 0;
                                                rmaDetailObj.StatusID = 1;
                                                rmaDetailObj.rmaDetGUID = 0;
                                                rmaDetailList.Add(rmaDetailObj);
                                                //Session["rmadetaillist"] = null;
                                                if (rmaDetailList.Count > 0)
                                                {
                                                    if (rmaList.Count > 0)
                                                    {
                                                        int rmaListIndex = rmaList.Count - 1;
                                                        rmaList[rmaListIndex].RmaDetails = rmaDetailList;
                                                    }
                                                }
                                                Session["rmadetaillist"] = rmaDetailList;
                                            }

                                            
                                            
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //errors.Append(ex.Message + "\n");
                                    }
                                }

                                if (rmaList != null && rmaList.Count > 0)
                                {
                                    //comm on 27/aug/2019
                                    //RMAUtility.UploadRMA(rmaList);
                                    

                                    //if (errors.Length == 0)
                                    //    lblMsg.Text = "Successfully uploaded";
                                    //else
                                    //    lblMsg.Text = "Successfully uploaded with " + errors.ToString();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Invalid File Extension!";
                    }
                }

            }
            catch
            {
                lblMsg.Text = "File Data not in correct format!";
            }
        }
        
        private void ValidateESN(ref RMADetail avEsn, int companyID, int rmaGUID)
        {
            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {

                List<avii.Classes.RMADetail> esnlist = RMAUtility.getRMAesn(companyID, avEsn.ESN, "", 0, rmaGUID, 0);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        avEsn.UPC = esnlist[0].UPC.ToString();
                        avEsn.rmaDetGUID = esnlist[0].rmaDetGUID;
                        avEsn.AVSalesOrderNumber = esnlist[0].AVSalesOrderNumber;
                        avEsn.PurchaseOrderNumber = esnlist[0].PurchaseOrderNumber;
                        avEsn.AllowRMA = esnlist[0].AllowRMA;
                        avEsn.AllowDuplicate = esnlist[0].AllowDuplicate;
                    }
                }
            }
        }
        
        private void UploadStores(int companyID)
        {
            //string[] validFileExtensions = new string[] { "xls" };
            string fileDir;
            string filePath;
            string fileExtension;
            bool flag = false;
            //List<StoreLocation> listObj = new List<StoreLocation>();
            List<StoreLocation> chunklistObj = new List<StoreLocation>();
            //Company companyInfo = null;
            string storeID = string.Empty;
            string storeName = string.Empty;
            string address = string.Empty;
            string city = string.Empty;
            string state = string.Empty;
            string country = string.Empty;
            string zip = string.Empty;
            bool isActive = true;
            int dataBlockSize = 30;
            StringBuilder storeIDs = new StringBuilder();
            StringBuilder errors = new StringBuilder();
            try
            {
                string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] as string;
                if (flnUpload.HasFile)
                {
                    fileDir = Server.MapPath("~");
                    filePath = fileDir + uploadFolder + flnUpload.FileName;
                    fileExtension = System.IO.Path.GetExtension(flnUpload.FileName).Replace(".", string.Empty).ToLower();

            
                    if (fileExtension == "xls" || fileExtension == "xlsx")
                        flag = true;

                    if (flag == true)
                    {
                        try
                        {
                            DataTable storeTable = new DataTable();
                            List<StoreLocation> stores;
                            flnUpload.PostedFile.SaveAs(filePath);

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
                                objAdapter1.Fill(storeTable);
                                objConn.Close();
                            }

                            //DataTable storeTable = CompanyOperations.GetImportStoreList(filePath);
                              if (storeTable.Rows.Count > 0)
                            {
                                stores = new List<StoreLocation>();
                                StringBuilder storeIdList = new StringBuilder();
                                for (int i = 0; i < storeTable.Columns.Count; i++)
                                {
                                    storeTable.Columns[i].ColumnName =  storeTable.Columns[i].ColumnName.Trim();
                                }
                                foreach (DataRow item in storeTable.Rows)
                                {
                                    try
                                    {
                                        storeID = Convert.ToString(clsGeneral.getColumnData(item, "StoreID", string.Empty, false));

                                        if (!string.IsNullOrEmpty(storeID) && storeID.Trim().Length > 0)
                                        {
                                            
                                            Address addrssObj1 = new Address();
                                            addrssObj1.AddressType = AddressType.Store;
                                            addrssObj1.Address1 = clsGeneral.getColumnData(item, "address", string.Empty, false) as string;
                                            addrssObj1.Country = clsGeneral.getColumnData(item, "country", string.Empty, false) as string; ;
                                            addrssObj1.City = clsGeneral.getColumnData(item, "city", string.Empty, false) as string;
                                            addrssObj1.Zip = Convert.ToString(clsGeneral.getColumnData(item, "zip", string.Empty, false));
                                            addrssObj1.State = clsGeneral.getColumnData(item, "state", string.Empty, false) as string;

                                            StoreLocation StoreLocationObj1 = new StoreLocation();
                                            StoreLocationObj1.StoreID = storeID;
                                            StoreLocationObj1.StoreName = clsGeneral.getColumnData(item, "StoreName", string.Empty, false) as string;
                                            StoreLocationObj1.Active = isActive;
                                            StoreLocationObj1.StoreAddress = addrssObj1;
                                            StoreLocationObj1.CompanyAddressID = 0;

                                            stores.Add(StoreLocationObj1);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errors.Append(ex.Message + "\n");
                                    }                                    
                                }

                                if (stores != null && stores.Count > 0)
                                {

                                    while (stores.Count > 0)
                                    {
                                        int count = stores.Count > dataBlockSize ? dataBlockSize : stores.Count;
                                        chunklistObj = stores.GetRange(0, count);
                                        stores.RemoveRange(0, count);
                                        if (chunklistObj.Count > 0)
                                        {
                                            CompanyOperations.UploadCompanyStores(chunklistObj, companyID);
                                            chunklistObj = null;
                                        }
                                    }

                                    
                                    if (errors.Length == 0)
                                        lblMsg.Text = "Successfully uploaded";
                                    else
                                        lblMsg.Text = "Successfully uploaded with " + errors.ToString();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Invalid File Extension!";
                    }
                }

            }
            catch
            {
                lblMsg.Text = "File Data not in correct format!";
            }
        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            try
            {

                if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                {
                    lblMsg.Text = "File is missing";
                }
                else
                {
                    string errorMessage = string.Empty;
                    //if (this.ddlData.SelectedIndex == 3)
                    //{
                    //    UploadMslInfo();
                    //}
                    //else 
                    if (this.ddlData.SelectedIndex == 1)
                    {
                        //if (dpCompany.SelectedIndex > 0)
                            UploadInventoryInfo(userID);
                        //else
                         //   lblMsg.Text = "Select a company";
                    }
                    else if (this.ddlData.SelectedIndex == 3)
                    {
                        // status id = 9 (Delete)
                        DeletePOs(999, userID);
                        //UploadInventoryInfo_New();
                        //Upload_Bulk_EsnMsl();
                    }
                    else if (this.ddlData.SelectedIndex == 4)
                    {
                        if (ViewState["userid"] == null)
                        {
                            lblMsg.Text = "No User is assign to the selected Company, please assign the user before uploading the Purchase Orders";
                        }
                        else
                        {
                            UploadPurchaseOrder();
                        }
                    }
                    else if (this.ddlData.SelectedIndex == 2)
                    {
                        // status id = 4 (Closed)
                        DeletePOs(4, userID);
                    }
                    //else if (this.ddlData.SelectedIndex == 1)
                    //{
                        
                    //    //errorMessage = UploadTrackingInfoNew();
                    //    errorMessage = UploadTrackingInfo(userID);

                    //    if (!string.IsNullOrEmpty(errorMessage) && errorMessage.Trim().Length > 0)
                    //    {
                    //        lblMsg.Text = errorMessage;
                    //    }
                    //    else
                    //    {
                    //        lblMsg.Text = "Successfully uploaded";
                    //    }
                    //}
                    else if (this.ddlData.SelectedIndex == 5)
                    {
                        // Upload company stores
                        if (dpCompany.SelectedIndex > 0)
                            UploadStores(Convert.ToInt32(dpCompany.SelectedValue));
                        else
                            lblMsg.Text = "Select a company";

                    }
                    //else if (this.ddlData.SelectedIndex == 8)
                    //{
                    //    // Upload company stores
                    //    if (dpCompany.SelectedIndex > 0)
                    //        UploadRMAs(Convert.ToInt32(dpCompany.SelectedValue));
                    //    else
                    //        lblMsg.Text = "Select a company";

                    //}
                    //else if (this.ddlData.SelectedIndex == 7)
                    //{
                    //    // Upload/update Tracking information
                    //    errorMessage = UploadTrackingInfoNew(userID);
                        

                    //    if (!string.IsNullOrEmpty(errorMessage) && errorMessage.Trim().Length > 0)
                    //    {
                    //        lblMsg.Text = errorMessage;
                    //    }
                    //    else
                    //    {
                    //        lblMsg.Text = "Successfully uploaded";
                    //    }

                    //}
                    
                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        
        private void Upload_Bulk_EsnMsl()
        {
            try
            {
                string fileName = UploadFile(0);
                if (!string.IsNullOrEmpty(fileName))
                {
                    avii.Classes.PurchaseOrder.BulkUpload_ESN(fileName);
                    lblMsg.Text = "File uploaded";
                }
                else
                {
                    lblMsg.Text = "File is missing or couldnot upload the file";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void UploadMslInfo()
        {
            try
            {            
                System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
                bool success = false;
                int insertCount = 0;
                int updateCount = 0;
                bool isLTE = false;
                string returnMessage = string.Empty;
                string fileName = UploadFile(0);
                int rowCount = 0;
                if (!string.IsNullOrEmpty(fileName) )
                {
                    if (dpItems.SelectedIndex < 0)
                        lblMsg.Text = "Please select the Phone Model";
                    else
                    {                       
                        string esnData = WriteESNXml(fileName, out rowCount);
                        if (rowCount > 0)
                        {
                            success = Classes.PurchaseOrder.SetEsnMsl(esnData, Convert.ToInt32(dpItems.SelectedValue), out insertCount, out updateCount, out returnMessage, out isLTE);
                            if (success)
                            {
                                //lblMsg.Text = "Successfully uploaded";
                                if(insertCount > 0 && updateCount > 0)
                                    lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";

                                if (insertCount > 0 && updateCount == 0)
                                    lblMsg.Text = insertCount + " records  successfully inserted.";
                                if (insertCount == 0 && updateCount > 0)
                                    lblMsg.Text = updateCount + " records  successfully updated.";

                            } 
                        }
                        
                        if (success == false || rowCount <= 0)
                        {
                            if (returnMessage != string.Empty)
                            {
                                if (returnMessage == "Duplicate ESNs Found")
                                    lblMsg.Text = "Duplicate ESNs Found";
                                else
                                {
                                    if (isLTE)
                                        lblMsg.Text = returnMessage + " ESN missing ICC_ID or LTE_IMSI data";
                                    else
                                        lblMsg.Text = "Cannot update LTEs columns data because the selected Inventory Item do not allow LTE attribute";

                                }
                            }
                            else
                                lblMsg.Text = "Could not upload ESN data, please check the file format";
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Could not upload the the file, please validate the fomart of the file or contact Administrator";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void UploadInventoryInfo(int userID)
        {
            string comment = txtComment.Text;
            string filename = string.Empty;

            
            Hashtable hshESN = new Hashtable();
            bool esnExists = false;
            bool esnIncorrectFormat = false;
            bool uploadEsn = false;
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
                        string fileName = UploadFile(1);
                        if (ViewState["filename"] != null)
                            filename = ViewState["filename"] as string;

                        System.Text.StringBuilder sbEsns = new StringBuilder();
                        StringWriter stringWriter = new StringWriter();
                        using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                        {
                            try
                            {
                                xw.WriteStartElement("purchaseorder");
                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line;
                                    string fmupc, esn, lteICCID, lteIMSI, otksl, akey, simNumber;
                                    double esnTest = 0;
                                    clsInventory inventory = new clsInventory();
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (!string.IsNullOrEmpty(line) && line.IndexOf("PoNum") < 0)
                                        {
                                            fmupc = esn = lteICCID = lteIMSI = otksl = akey = simNumber = string.Empty;
                                            string[] arr = line.Split(',');
                                            try
                                            {
                                                if (arr.Length > 3)
                                                {
                                                    esn = arr[3].Trim();
                                                }
                                                if (!string.IsNullOrEmpty(esn))
                                                {
                                                    //if (!double.TryParse(esn, out esnTest))
                                                    //{
                                                    //    sbEsns.Append(esn + " ");
                                                    //    esnIncorrectFormat = true;
                                                    //}
                                                    //else
                                                    //{
                                                        //esn = arr[3];
                                                        if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                        {
                                                            uploadEsn = false;
                                                            throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                                        }
                                                        else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                        {
                                                            hshESN.Add(esn, esn);
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
                                                            xw.WriteStartElement("item");
                                                            xw.WriteElementString("ponumber", arr[0].Trim());
                                                            xw.WriteElementString("podId", arr[1].Trim());
                                                            xw.WriteElementString("itemcode", arr[2].Trim());
                                                            xw.WriteElementString("esn", esn);
                                                            xw.WriteElementString("fmupc", fmupc);
                                                            xw.WriteElementString("lteiccid", lteICCID);
                                                            xw.WriteElementString("lteimsi", lteIMSI);
                                                            xw.WriteElementString("otksl", otksl);
                                                            xw.WriteElementString("akey", akey);
                                                            //xw.WriteElementString("simnumber", simNumber);
                                                            xw.WriteEndElement();
                                                        //}
                                                        esn = string.Empty;
                                                    //}
                                                }
                                                //}
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
                                xw.Flush();
                                xw.Close();
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }
                        }
                        if (uploadEsn && esnIncorrectFormat == false)
                        {
                            XmlDocument xdoc = new XmlDocument();
                            xdoc.LoadXml(stringWriter.ToString());
                            string invalidSkuEsnList = string.Empty;
                            string invalidEsnList = string.Empty;
                            string errorMessage = string.Empty;
                            string esnExistsMessage = string.Empty;
                            string badESNMessage = string.Empty;
                            string simNumberMsg = string.Empty;
                            int recordCount = 0;

                            string poErrorMessage = string.Empty;
                            string poSimMessage = string.Empty;
                            //string errorMessage = string.Empty;
                            string poskuMessage = string.Empty;
                            string poEsnSimMessage = string.Empty;

                            string esnList = avii.Classes.PurchaseOrder.UpLoadESNs(xdoc.OuterXml, userID,filename, comment, out invalidEsnList, out invalidSkuEsnList, out esnExistsMessage, out badESNMessage, out simNumberMsg, out recordCount, out poErrorMessage, out poSimMessage, out poskuMessage, out poEsnSimMessage);
                            if (string.IsNullOrEmpty(esnList) && string.IsNullOrEmpty(invalidEsnList) && string.IsNullOrEmpty(invalidSkuEsnList) && string.IsNullOrEmpty(esnExistsMessage) && string.IsNullOrEmpty(badESNMessage) && string.IsNullOrEmpty(simNumberMsg) && recordCount > 0 && string.IsNullOrEmpty(poErrorMessage) && string.IsNullOrEmpty(poSimMessage) && string.IsNullOrEmpty(poskuMessage) && string.IsNullOrEmpty(poEsnSimMessage))
                            {
                                trItemFile.Visible = false;
                                trComment.Visible = false;
                                lblMsg.Text = "Uploaded successfully. <br /> Record count: " + recordCount.ToString();
                                stringWriter.Close();
                                xdoc = null;
                            }
                            else
                            {
                                trItemFile.Visible = true;
                                trComment.Visible = true;

                                if (!string.IsNullOrEmpty(esnList))
                                    errorMessage = esnList + " ESN missing LTEICCID or LTEIMSI data";

                                if (!string.IsNullOrEmpty(invalidEsnList))
                                {
                                    if(string.IsNullOrEmpty(errorMessage))
                                        errorMessage = invalidEsnList + " ESN(s) does not exists!";
                                    else
                                        errorMessage = errorMessage + " <br /> " + invalidEsnList + " ESN(s) does not exists!";
                                }
                                if (!string.IsNullOrEmpty(invalidSkuEsnList))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = invalidSkuEsnList + " ESN(s) are not assigned to right Product/SKU!";
                                    else
                                        errorMessage = errorMessage + " <br /> " + invalidSkuEsnList + " ESN(s) are not assigned to right Product/SKU!";
                                }

                                if (!string.IsNullOrEmpty(esnExistsMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = esnExistsMessage + " ESN(s) are already assigned!";
                                    else
                                        errorMessage = errorMessage + " <br /> " + esnExistsMessage + " ESN(s) are already assigned!";

                                }

                                if (!string.IsNullOrEmpty(badESNMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = badESNMessage + " ESN(s) are Bad ESN!";
                                    else
                                        errorMessage = errorMessage + " <br /> " + badESNMessage + " ESN(s) are Bad ESN!";

                                }
                                if (!string.IsNullOrEmpty(simNumberMsg))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = simNumberMsg + " ESN(s) missing Sim number!";
                                    else
                                        errorMessage = errorMessage + " <br /> " + simNumberMsg + " ESN(s) missing Sim number!";

                                }
                                if (!string.IsNullOrEmpty(poErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poErrorMessage + " SIM(s) does not exists in the repository.";
                                    else
                                        errorMessage = errorMessage + " <br /> " + poErrorMessage + " SIM(s) does not exists in the repository.";
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

                                if (recordCount == 0 && string.IsNullOrEmpty(errorMessage))
                                {
                                    errorMessage = "No records to update!";
                                }

                                
                                lblMsg.Text = errorMessage;
                                
                            }
                        }
                        else if (esnIncorrectFormat)
                        {
                            lblMsg.Text = "Incorrect format " + sbEsns.ToString();
                        }

                        if (sbEsns != null && sbEsns.Length > 0)
                        {
                            if (lblMsg.Text.Trim().Length > 0)
                                lblMsg.Text = lblMsg.Text + " <br>" + "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                            else
                                lblMsg.Text = "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void UploadInventoryInfo_New()
        {
            Hashtable hshESN = new Hashtable();
            PurchaseOrderESNItem poEsnItem;
            List<PurchaseOrderESNItem> poEnsItemList = null;
            bool esnExists = false;
            bool esnIncorrectFormat = false;
            bool uploadEsn = false;

            if (flnUpload.PostedFile.FileName.Trim().Length == 0)
            {
                lblMsg.Text = "Select file to upload";
            }
            else
            {
                System.Text.StringBuilder sbErr = new StringBuilder();
                System.Text.StringBuilder sbEsns = new StringBuilder();
                if (flnUpload.PostedFile.ContentLength > 0)
                {
                    string fileName = UploadFile(0);
                    try
                    {
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            string line;
                            int podID = 0;
                            string fmupc, esn;
                            double esnTest = 0;
                            poEnsItemList = new List<PurchaseOrderESNItem>();
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (!string.IsNullOrEmpty(line) && line.IndexOf("PoNum") < 0)
                                {
                                    fmupc = esn = string.Empty;
                                    string[] arr = line.Split(',');
                                    try
                                    {
                                        if (arr.Length > 4)
                                        {
                                            esn = arr[4];
                                            if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                            {
                                                uploadEsn = false;
                                                throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                            }
                                            else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                            {
                                                hshESN.Add(esn, esn);
                                            }
                                        }

                                        if (arr.Length > 5)
                                        {
                                            fmupc = arr[5];
                                            if (hshESN.ContainsKey(fmupc) && !string.IsNullOrEmpty(fmupc))
                                            {
                                                uploadEsn = false;
                                                throw new ApplicationException("FmUPC already exist in the file");
                                            }
                                            else if (!hshESN.ContainsKey(fmupc) && !string.IsNullOrEmpty(fmupc))
                                            {
                                                hshESN.Add(fmupc, fmupc);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(esn))
                                        {
                                            if (!double.TryParse(esn, out esnTest))
                                            {
                                                sbEsns.Append(esn + " ");
                                                esnIncorrectFormat = true;
                                            }
                                            else
                                            {
                                                uploadEsn = true;
                                                if (int.TryParse(arr[1], out podID) && esnIncorrectFormat == false)
                                                {
                                                    poEsnItem = new PurchaseOrderESNItem();
                                                    poEsnItem.PodID = podID;
                                                    poEsnItem.ItemCode = arr[2];
                                                    poEsnItem.ESN = esn;
                                                    poEsnItem.FmUPC = fmupc;
                                                    poEnsItemList.Add(poEsnItem);
                                                }
                                                else
                                                {
                                                    sbEsns.Append(esn + " ");
                                                    esnIncorrectFormat = true;
                                                }
                                            }
                                        }
                                    }
                                    catch (ApplicationException ex)
                                    {
                                        throw ex;
                                    }
                                    catch (Exception exception)
                                    {
                                        lblMsg.Text = exception.Message;
                                        sbErr.Append("<br>" + exception.Message);
                                    }
                                }
                            }
                            sr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                        uploadEsn = false;
                    }
                    }

                    if (uploadEsn && esnIncorrectFormat == false)
                    {
                        avii.Classes.PurchaseOrder.UpLoadESN(poEnsItemList);
                        trItemFile.Visible = false;
                        trComment.Visible = false;
                        lblMsg.Text = "Uploaded successfully";
                    }
                    else if (esnIncorrectFormat)
                    {
                        lblMsg.Text = "Please upload file again. There is an Incorrect Format for " + sbEsns.ToString();
                    }

                    if (sbEsns != null && sbEsns.Length > 0)
                    {
                        if (lblMsg.Text.Trim().Length > 0)
                            lblMsg.Text = lblMsg.Text + " <br>" + "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                        else
                        lblMsg.Text = "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                    }
                    else if (sbErr != null && sbErr.Length > 0)
                    {
                        lblMsg.Text = sbErr.ToString();
                    }
                    
                }
            
        }

        private void PopulateInentoryObject(string esn, ref clsInventory inventory, ref System.Text.StringBuilder stringbuilder)
        {

            if (!string.IsNullOrEmpty(esn) && inventory != null)
            {
                if (!clsInventoryDB.ValidateEsnExists(esn))
                {
                    InventoryItem item = new InventoryItem();
                    item.Esn = esn;
                    inventory.InventoryItem.Add(item);
                }
                else
                {
                    stringbuilder.Append("\nEsn# " + esn + " is already issued");
                }
            }
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

        private void UploadPurchaseOrder()
        {

            StringBuilder sbPOWriteError = new StringBuilder();
            string comment = txtComment.Text;
            //string pifilename = string.Empty;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }

            
            if (dpCompany.SelectedIndex < 0)
                throw new Exception("Please select Company");
            else
            {
                bool saved = false;
                string fileName = UploadFile(4);
                //if (ViewState["filename"] != null)
                //    pifilename = ViewState["filename"] as string;

                if (!string.IsNullOrEmpty(fileName))
                {
                    List<clsPurchaseOrder> poList = new List<clsPurchaseOrder>();
                    int companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    StringBuilder sbError = WritePurchaseOrder(fileName, companyID, ref poList);
                    if (sbError != null && sbError.Length > 0)
                    {
                        lblMsg.Text = sbError.ToString();
                    }
                    else
                    {
                        if (poList != null && poList.Count > 0)
                        {
                            PurchaseOrderResponse response = null;
                            
                            foreach (clsPurchaseOrder po in poList)
                            {
                                
                                response = avii.Classes.PurchaseOrder.SaveNewPurchaseOrderCompany(po, companyID, userID, fileName, comment);
                                if (response != null && !string.IsNullOrEmpty(response.ErrorCode))
                                {
                                    sbPOWriteError.Append("\n" + po.PurchaseOrderNumber + " =>  " + response.ErrorCode );
                                }
                            }

                            if (sbPOWriteError != null && sbPOWriteError.Length > 0)
                            {
                                lblMsg.Text = sbPOWriteError.ToString();
                            }
                            else
                            {
                                lblMsg.Text = "Purcahse Orders are successfully uploaded";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Uploaded file does not have any Purchase Order to save";
                        }
                    }
                }

            }

        }

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void BindInventory()
        {
            
            InventoryList inventoryList = new InventoryList();
            inventoryList.CurrentInventory = avii.Classes.PurchaseOrder.GetInventory(0, 0).CurrentInventory;
            if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
            {
                dpItems.Items.Clear();
                foreach (avii.Classes.clsInventory invItem in avii.Classes.PurchaseOrder.GetInventory(0).CurrentInventory)
                {
                    dpItems.Items.Add(new ListItem(invItem.UPC + " - " + invItem.PhoneModel, invItem.ItemID.ToString()));
                }

                dpItems.Visible = true;
            }
            else
            {
                lblMsg.Text = "Inventory items are missing, please contact administrator";
            }
        }

        //private void UpdateTrackingInfo()
        //{
        //    List<TrackingInfo> trackingInfoList = new List<TrackingInfo>();
        //    TrackingInfo trackingInfo = new TrackingInfo();

        //    trackingInfo.PurchaseOrderNumber = "12323333";
        //    trackingInfo.ShipToTrackingNumber = "32535345345";
        //    trackingInfo.SalesOrderNumber = "123";
        //    trackingInfo.CompanyID = 2;
        //    trackingInfo.StatusID = Convert.ToInt32(PurchaseOrderStatus.Shipped);
        //    trackingInfoList.Add(trackingInfo);

        //    TrackingInfo trackingInfo1 = new TrackingInfo();

        //    trackingInfo1.PurchaseOrderNumber = "5123233335";
        //    trackingInfo1.ShipToTrackingNumber = "5325353453455";
        //    trackingInfo1.SalesOrderNumber = "5123";
        //    trackingInfo1.CompanyID = 2;
        //    trackingInfo1.StatusID = Convert.ToInt32(PurchaseOrderStatus.Shipped);
        //    trackingInfoList.Add(trackingInfo1);

        //    Classes.PurchaseOrder.UpdateTrackingInfo(trackingInfoList);
        //}
        
        private string UploadTrackingInfo(int userID)
        {
            if (dpCompany.SelectedIndex < 0)
                throw new Exception("Please select Company");
            else
            {

                System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
                bool saved = false;
                string fileName = UploadFile(0);
                
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line, poNum, trackingNumber, avOrderNumber;
                    line = poNum = trackingNumber = avOrderNumber = string.Empty;
                    int compID = Convert.ToInt32(dpCompany.SelectedValue);
                    while ((line = sr.ReadLine()) != null)
                    {

                        try
                        {
                            //           poID = 0;
                            //         int.TryParse(line.Split(DELIMITER)[0], out poID);
                            poNum = line.Split(DELIMITER)[0].Trim();
                            trackingNumber = line.Split(DELIMITER)[1].Trim();
                            avOrderNumber = line.Split(DELIMITER)[2].Trim();
                            


                            if (!string.IsNullOrEmpty(poNum) && compID > 0   && !string.IsNullOrEmpty(trackingNumber)
                                            && ValidateTrackingNumber(trackingNumber))
                            {
                                
                                
                                saved = Classes.PurchaseOrder.SetTrackingInfo(compID, poNum, trackingNumber, avOrderNumber, userID);
                            }
                            if (saved == false)
                            {
                                stringbuilder.Append(poNum + ", ");
                            }

                            line = poNum = trackingNumber = avOrderNumber = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            stringbuilder.Append(poNum + ", ");
                        }
                    }

                    sr.Close();


                }
                if (stringbuilder.ToString().Length > 0)
                {
                    return "Could not upload shipping information for Purchase Orders :\n" + stringbuilder.ToString();
                }
                else
                {
                    return "Successfully uploaded";
                }
            }

        }
        
        private string UploadTrackingInfoNew(int userID)
        {
            int poUploadedCount = 0;
            int poTotalCount = 0;
            string returnMessage = string.Empty;

            if (dpCompany.SelectedIndex < 0)
            {
                //throw new Exception("Please select Company");
                returnMessage = "Please select Company";
            }
            else
            {

                System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
                bool saved = false;
                string fileName = UploadFile(0);
                List<TrackingInfo> trackingInfoList = new List<TrackingInfo>();

                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line, poNum, trackingNumber, avOrderNumber;
                    line = poNum = trackingNumber = avOrderNumber = string.Empty;
                    int compID = Convert.ToInt32(dpCompany.SelectedValue);
                    while ((line = sr.ReadLine()) != null)
                    {

                        try
                        {
                            
                            TrackingInfo trackingInfo = new TrackingInfo();
                            //           poID = 0;
                            //         int.TryParse(line.Split(DELIMITER)[0], out poID);
                            poNum = line.Split(DELIMITER)[0];
                            trackingNumber = line.Split(DELIMITER)[1];
                            avOrderNumber = line.Split(DELIMITER)[2];
                            if (!string.IsNullOrEmpty(poNum) && poNum.ToLower() != "ponum")
                            {
                                poTotalCount++;

                                if (!string.IsNullOrEmpty(poNum) && compID > 0 && !string.IsNullOrEmpty(trackingNumber)
                                                && ValidateTrackingNumber(trackingNumber))
                                {
                                    trackingInfo.PurchaseOrderNumber = poNum.Trim();
                                    trackingInfo.ShipToTrackingNumber = trackingNumber.Trim();
                                    trackingInfo.SalesOrderNumber = avOrderNumber.Trim();
                                    //trackingInfo.CompanyID = compID;
                                    //trackingInfo.StatusID = Convert.ToInt32(PurchaseOrderStatus.Shipped);
                                    trackingInfoList.Add(trackingInfo);

                                    saved = true;
                                    //saved = Classes.PurchaseOrder.SetTrackingInfo(compID, poNum, trackingNumber, avOrderNumber);
                                }
                                if (saved == false)
                                {
                                    stringbuilder.Append(poNum + ", ");
                                }

                            }
                            line = poNum = trackingNumber = avOrderNumber = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            stringbuilder.Append(poNum + ", ");
                        }
                    }
                    sr.Close();
                    poUploadedCount = Classes.PurchaseOrder.UpdateTrackingInfo(trackingInfoList, userID);

                }
                if (poTotalCount == 0 && poUploadedCount == 0)
                    returnMessage = "There is no valid Purchase Orders in the file";
                if (poUploadedCount > 0 && poUploadedCount == poTotalCount)
                    returnMessage = "Successfully uploaded";
                else
                    returnMessage = poUploadedCount + " out of " + poTotalCount + " Purchase Orders uploaded Successfully";
                //if (stringbuilder.ToString().Length > 0)
                //{
                //    return "Could not upload shipping information for Purchase Orders :\n" + stringbuilder.ToString();
                //}
                //else
                //{
                //    return "Successfully uploaded";
                //}
            }
            return returnMessage;

        }

        private void DeletePOs(int statusID, int userID)
        {
            if (dpCompany.SelectedIndex < 0)
                throw new Exception("Please select Company");
            else
            {

                System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
                System.Text.StringBuilder sbPO = new System.Text.StringBuilder();
                bool saved = false;
                int groupSize = 50;
                string fileName = UploadFile(0);
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line, poNum;
                    line = poNum = string.Empty;
                    int compID = Convert.ToInt32(dpCompany.SelectedValue);
                    int lineCounter = 0; 
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                                    
                            poNum = line.Split(DELIMITER)[0];
                            if (!string.IsNullOrEmpty(poNum))
                            {
                                sbPO.Append(poNum.Trim());
                            }

                            if (lineCounter >= groupSize || sr.EndOfStream)
                            {
                                DeletePoDBCall(sbPO, compID, statusID, userID);
                                lineCounter = 0;
                            }
                            else
                            {
                                sbPO.Append(" ");
                            }

                            lineCounter = lineCounter + 1;
                            poNum = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            stringbuilder.Append(sbPO.ToString() + ", ");
                        }
                    }

                    sr.Close();
                    if (stringbuilder.ToString().Length > 0)
                    {
                        lblMsg.Text = "Error closing : " + stringbuilder.ToString();
                    }
                    else
                    {
                         lblMsg.Text = "Successfully uploaded";
                    }

                }
            }

        }

        private string DeletePoDBCall(StringBuilder sbPO, int companyID, int statusID, int userID)
        {

            string retMsg = string.Empty;

            string poList = sbPO.ToString();

            if (!string.IsNullOrEmpty(poList))
            {
                bool saved = Classes.PurchaseOrder.DeletePurchaseOrders(poList, companyID, statusID, userID);

                if (saved == false)
                {
                    retMsg = "Purchase Order List is empty, please check the file";
                }
                else
                {
                    retMsg = "";
                }
            }
            else
            {
                retMsg = "Purchase Order List is empty, please check the file";
            }
            
          
            return retMsg;
        }

        private string UploadFile(int uploadOption)
        {

            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (uploadOption == 1)
            {
                if (ConfigurationManager.AppSettings["EsnAssigmentFilesStoreage"] != null)
                {
                    fileStoreLocation = ConfigurationManager.AppSettings["EsnAssigmentFilesStoreage"].ToString();
                }
            }
            else
                if (uploadOption == 4)
                {
                    if (ConfigurationManager.AppSettings["FulfillmentOldFilesStoreage"] != null)
                    {
                        fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentOldFilesStoreage"].ToString();
                    }
                }
                else
                {
                    if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
                    {
                        fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
                    }
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

        protected void ddlData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnlURL.Visible = false;
            if (ddlData.SelectedIndex == 1)
            {
                trItems.Visible = false;
                trItems.Visible = false;
                trItemFile.Visible = true;
                trComment.Visible = true;
                trShipFile.Visible = false;
                trMsl.Visible = false;
                trTrack.Visible = false;
                trDp.Visible = false;
                btnMSL.Visible = false;
                trStore.Visible = false;
                trRMA.Visible = false;
            }
            else if (ddlData.SelectedIndex == 5)
            {
                bindCustomerDropDown();
                trStore.Visible = true;
                trTrack.Visible = true;
                trItems.Visible = false;
                trItemFile.Visible = false;
                trComment.Visible = false;
                trShipFile.Visible = false;
                trItems.Visible = false;
                trMsl.Visible = false;
                trDp.Visible = false;
                btnMSL.Visible = false;
                trRMA.Visible = false;
            }
            //else if (ddlData.SelectedIndex == 1 || ddlData.SelectedIndex == 7)
            //{
            //    bindCustomerDropDown();
            //    trStore.Visible = false;
            //    trTrack.Visible = true;
            //    trItems.Visible = false;
            //    trItemFile.Visible = false;
            //    trShipFile.Visible = true;
            //    trItems.Visible = false;
            //    trMsl.Visible = false;
            //    trDp.Visible = false;
            //    btnMSL.Visible = false;
            //    trRMA.Visible = false;
            //}
            //else if (ddlData.SelectedIndex == 3)
            //{
            //    trItems.Visible = true;
            //    trRMA.Visible = false;
            //    BindInventory();
            //    trStore.Visible = false;
            //    trItemFile.Visible = false;
            //    trShipFile.Visible = false;
            //    trMsl.Visible = true;
            //    btnMSL.Visible = true;
            //    trDp.Visible = false;
            //    ///trTrack.Visible = false;
            //}
            else if (ddlData.SelectedIndex == 2 || ddlData.SelectedIndex == 3)
            {
                bindCustomerDropDown();
                trRMA.Visible = false;
                trStore.Visible = false;
                trTrack.Visible = true;
                trItems.Visible = false;
                trItemFile.Visible = false;
                trComment.Visible = false;
                trShipFile.Visible = false;
                trItems.Visible = false;
                if (ddlData.SelectedIndex == 3)
                {
                    trDp.Visible = true;
                    lblFileFormat.Text = "PurchaseOrderNumber";
                }
                else
                    trMsl.Visible = false;
                btnMSL.Visible = false;
            }
            else if (ddlData.SelectedIndex == 4)
            {
                bindCustomerDropDown();
                trRMA.Visible = false;
                trTrack.Visible = true;
                trItems.Visible = false;
                trItemFile.Visible = false;
                trComment.Visible = true;
                trShipFile.Visible = false;
                trItems.Visible = false;
                trDp.Visible = true;
                lblFileFormat.Text = "PO#, PO Date, Store ID, SKU#, Quantity, Shipping By, Contact Name,	Address1, Address2, City, State, Zip";
                trMsl.Visible = false;
                btnMSL.Visible = false;
            }
            //else if (ddlData.SelectedIndex == 8)
            //{
            //    bindCustomerDropDown();
            //    trRMA.Visible = true;
            //    trStore.Visible = false;
            //    trTrack.Visible = true;
            //    trItems.Visible = false;
            //    trItemFile.Visible = false;
            //    trShipFile.Visible = false;
            //    trItems.Visible = false;
            //    trMsl.Visible = false;
            //    trDp.Visible = false;
            //    btnMSL.Visible = false;
            //}
            else
            {
                trRMA.Visible = false;
                trItemFile.Visible = false;
                trComment.Visible = false;
                trItems.Visible = false;
                trShipFile.Visible = false;
                trMsl.Visible = false;
                trItems.Visible = false;
                trTrack.Visible = false;
                trDp.Visible = false;
                btnMSL.Visible = false;
            }
        }

        protected void dpItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpItems.SelectedIndex == 0)
            {
                hnk.Visible = false;
            }
            else
            {
                hnk.Text = dpItems.SelectedItem.Text;
                hnk.Target = "new";
                hnk.NavigateUrl = string.Format("./esnlist.aspx?id={0}", dpItems.SelectedValue);
                hnk.Visible = true;
            }
        }

        protected void btnMSL_Click(object sender, EventArgs e)
        {
            int updateCount = 0;
            string returnMessage = string.Empty;
            try
            {
                avii.Classes.PurchaseOrder.AssignMSL2ESNDataNew(null, out updateCount, out returnMessage);
                if (returnMessage == string.Empty)
                    lblMsg.Text = updateCount + " records successfully assigned";
                else
                    lblMsg.Text = returnMessage;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private string WriteESNXml(string fileName, out int rowCount)
        {
            StringWriter stringWriter = new StringWriter();
            rowCount = 0;
            string upc = dpItems.SelectedItem.Text.Substring(0, dpItems.SelectedItem.Text.IndexOf('-')).Trim();
            if (!string.IsNullOrEmpty(fileName))
            {
                string esn, msl;
                esn = msl = string.Empty;
                try
                {
                    using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                    {                  
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            string line;
                            string[] tempData;
                            xw.WriteStartElement("esnlist");
                            while ((line = sr.ReadLine()) != null)
                            {
                                tempData = line.Split(',');
                                if (tempData.Length > 9)
                                {
                                    if (!string.IsNullOrEmpty(tempData[0]) && tempData[0].Trim().Length > 0 && tempData[0].Trim().ToUpper() != "ESN")
                                    {
                                        xw.WriteStartElement("esnitem");
                                        xw.WriteElementString("esn", tempData[0]);
                                        //ESN, MEID, AKEY, MSL, OTSKL, AVPO, PRO#, PALLET_ID, CARTON_ID, HEX 
                                        if (tempData.Length > 1)
                                            xw.WriteElementString("meid", (tempData[1].Trim().Length == 0 ? "00000" : tempData[1]));
                                        if (tempData.Length > 2)
                                            xw.WriteElementString("akey", (tempData[2].Trim().Length == 0 ? "00000" : tempData[2]));
                                        if (tempData.Length > 3) 
                                            xw.WriteElementString("msl", (tempData[3].Trim().Length == 0 ? "00000" : tempData[3]));
                                        if (tempData.Length > 4)
                                            xw.WriteElementString("otskl", (tempData[4].Trim().Length == 0 ? "00000" : tempData[4]));
                                        if (tempData.Length > 5)
                                            xw.WriteElementString("avpo", (tempData[5].Trim().Length == 0 ? "00000" : tempData[5]));
                                        if (tempData.Length > 6)
                                            xw.WriteElementString("pro", (tempData[6].Trim().Length == 0 ? "00000" : tempData[6]));
                                        if (tempData.Length > 7)
                                            xw.WriteElementString("pallet", (tempData[7].Trim().Length == 0 ? "00000" : tempData[7]));
                                        if (tempData.Length > 8)
                                            xw.WriteElementString("carton", (tempData[8].Trim().Length == 0 ? "00000" : tempData[8]));
                                        if (tempData.Length > 9)
                                            xw.WriteElementString("hex", (tempData[9].Trim().Length == 0 ? "00000" : tempData[9]));

                                        if (tempData.Length > 10)
                                            xw.WriteElementString("icc_id", (tempData[10].Trim().Length == 0 ? string.Empty : tempData[10]));
                                        if (tempData.Length > 11)
                                            xw.WriteElementString("lte_imsi", (tempData[11].Trim().Length == 0 ? string.Empty : tempData[11]));

                                        xw.WriteElementString("upc", upc);

                                        xw.WriteEndElement();
                                        rowCount = rowCount + 1;
                                    }
                                }
                                else
                                    throw new Exception("Incorrect Format: Please check the format of the file");
                            }
                            sr.Close();
                            xw.Flush();
                            xw.Close();
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return stringWriter.ToString();
        }

        private StringBuilder WritePurchaseOrder(string fileName, int companyID, ref List<clsPurchaseOrder> poList)
        {
            List<ShipBy> shipViaList = avii.Classes.PurchaseOrder.GetShipByList();

            StringBuilder sbError = new StringBuilder();
            InventoryList inventoryList = clsInventoryDB.GetInventory(0, companyID);
            Hashtable hshStores = new Hashtable();
            Company getCompanyInfo = CompanyOperations.getCompanyInfo(companyID, string.Empty, string.Empty);

            if (getCompanyInfo.Stores != null && getCompanyInfo.Stores.Count > 0)
            {
                foreach (StoreLocation sl in getCompanyInfo.Stores)
                {
                    hshStores.Add(sl.StoreID.ToUpper(), sl);
                }
            }
            else
            {
                throw new Exception("No <b>Store ID</b> is assigned to the selected customer. Please assign the Store ID to the selected customer");
            }

            clsPurchaseOrder po = null;

            if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
            {
                Hashtable hshPoList = new Hashtable();

                if (!string.IsNullOrEmpty(fileName))
                {
                    if (poList == null)
                    {
                        poList = new List<clsPurchaseOrder>();
                    }

                    try
                    {
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            string poNumber = string.Empty;
                            string poNum, contactName, address1, address2, city, state, zip, shipvia;
                            poNum = contactName = address1 = address2 = city = state = zip = shipvia = string.Empty;
                            int qty = 0;
                            string line, storeID;
                            string[] tempData;
                            int ctr = 0;
                            //bool addressFlag = true;
                            StoreLocation sltemp = null;
                            while ((line = sr.ReadLine()) != null)
                            {
                                qty = 0;
                                tempData = line.Split(',');
                                if (tempData[0] != "PO#")
                                {
                                    if ((int.TryParse(tempData[4], out qty)))
                                    {
                                        if (tempData.Length >= 5 && (!string.IsNullOrEmpty(tempData[0]) && tempData[0].Trim().Length > 0))
                                        {
                                            poNumber = tempData[0].Trim();
                                            storeID = tempData[2].Trim();
                                            
                                            if (!hshPoList.ContainsKey(poNumber))
                                            {
                                                po = new clsPurchaseOrder();
                                            }
                                            else
                                            {
                                                po = hshPoList[poNumber] as clsPurchaseOrder;
                                            }

                                            if (!hshStores.ContainsKey(storeID.ToUpper()))
                                                throw new Exception("Store ID is not assigned, please assigned the store id <b>" + storeID + "</b> to the customer");
                                            else
                                              {
                                                sltemp = hshStores[storeID.ToUpper()] as StoreLocation;
                                                if (sltemp != null)
                                                {
                                                    po.PurchaseOrderNumber = tempData[0];
                                                    po.StoreID = storeID;
                                                    DateTime poDate;
                                                    if (!string.IsNullOrEmpty(tempData[1]))
                                                    {
                                                        if (DateTime.TryParse(tempData[1], out poDate))
                                                        {
                                                            DateTime currentDate = DateTime.Now;
                                                                                                                       
                                                            TimeSpan diffResult = currentDate - poDate;
                                                            if (diffResult.Days > 90)
                                                                throw new Exception("Invalid Fulfillment Date! Can not create Fulfillment order before 90 days back.");
                                                            else
                                                                po.PurchaseOrderDate = poDate;
                                                        }
                                                        else
                                                            throw new Exception("PO Date does not have correct format(MM/DD/YYYY)");
                                                    }
                                                    else
                                                        po.PurchaseOrderDate = DateTime.Now;

                                                    if (poNum != tempData[0])
                                                    {
                                                        contactName = tempData.Length > 6 ? tempData[6] : sltemp.StoreContact.ContactName;
                                                        address1 = tempData.Length > 7 ? tempData[7] : sltemp.StoreAddress.Address1;
                                                        address2 = tempData.Length > 8 ? tempData[8] : sltemp.StoreAddress.Address2;
                                                        city = tempData.Length > 9 ? tempData[9] : sltemp.StoreAddress.City;
                                                        state = tempData.Length > 10 ? tempData[10] : sltemp.StoreAddress.State;
                                                        zip = tempData.Length > 11 ? tempData[11] : sltemp.StoreAddress.Zip;
                                                        //addressFlag = false;
                                                    }
                                                    shipvia =  tempData[5].Trim();
                                                    poNum = tempData[0];
                                                    po.Shipping.ContactName = string.IsNullOrEmpty(contactName.Trim()) ? sltemp.StoreContact.ContactName : contactName;// sltemp.StoreContact.ContactName;
                                                    po.Shipping.ShipToAddress = string.IsNullOrEmpty(address1.Trim()) ? sltemp.StoreAddress.Address1 : address1;
                                                    po.Shipping.ShipToAddress2 = string.IsNullOrEmpty(address2.Trim()) ? sltemp.StoreAddress.Address2 : address2;
                                                    po.Shipping.ShipToCity = string.IsNullOrEmpty(city.Trim()) ? sltemp.StoreAddress.City : city;
                                                    po.Shipping.ShipToState = string.IsNullOrEmpty(state.Trim()) ? sltemp.StoreAddress.State : state;
                                                    po.Shipping.ShipToZip = string.IsNullOrEmpty(zip.Trim()) ? sltemp.StoreAddress.Zip : zip;
                                                    po.Shipping.ShipToAttn = tempData[6];//sltemp.StoreContact.ContactName;
                                                    //po.ShipThrough = tempData[5].Trim();
                                                    shipvia = shipvia.ToUpper();
                                                    var shipViaVar = (from item in shipViaList where item.ShipByCode.ToUpper().Equals(shipvia) select item).ToList();
                                                    //var shipViaVar = (select item from shipViaList
                                                    if (!string.IsNullOrEmpty(shipvia))
                                                    {
                                                        if (shipViaVar != null && shipViaVar.Count > 0 && !string.IsNullOrEmpty(shipViaVar[0].ShipByCode))
                                                            po.ShipThrough = shipvia;
                                                        else
                                                            sbError.Append(string.Format("\nPurchaseOrder# {0} shipvia {1} are not valid", po.PurchaseOrderNumber, shipvia));
                                                    }
                                                    else
                                                        sbError.Append(string.Format("\nPurchaseOrder# {0} have empty shipvia", po.PurchaseOrderNumber));

                                                    string itemCode = string.Empty;
                                                    itemCode = tempData[3].Trim();
                                                    if (itemCode != string.Empty)
                                                    {
                                                        if (!inventoryList.Exists(itemCode))
                                                        {
                                                            sbError.Append(string.Format("\nPurchaseOrder# {0} itemcode {1} are not valid, please check the item code from catalog", po.PurchaseOrderNumber, itemCode));
                                                        }
                                                        else
                                                        {
                                                            if (qty > 0)
                                                            {
                                                                for (int counter = 0; counter < qty; counter++)
                                                                {
                                                                    po.PurchaseOrderItems.Add(new PurchaseOrderItem() { LineNo = po.PurchaseOrderItems.Count + 1, ItemCode = tempData[3], Quantity = 1 });
                                                                }
                                                            }
                                                            else
                                                            {
                                                                sbError.Append(string.Format("\nPurchaseOrder# {0} does not have Quantity assigned ", po.PurchaseOrderNumber));
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        sbError.Append(string.Format("\nPurchaseOrder# {0} have empty ItemCode ", po.PurchaseOrderNumber));
                                                    }

                                                    if (po != null && po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
                                                    {
                                                        if (hshPoList.ContainsKey(po.PurchaseOrderNumber))
                                                            hshPoList[po.PurchaseOrderNumber] = po;
                                                        else
                                                            hshPoList.Add(po.PurchaseOrderNumber, po);
                                                    }
                                                    else
                                                    { 
                                                    
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Store ID <b>" + storeID + "</b> information is missing");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Uploaded file does not have correct Format");
                                        }
                                    }
                                    else
                                    {
                                            throw new Exception("Please check the file, Quantity should be greater than 0");

                                    }
                                }

                                ctr = ctr + 1;
                            }
                            sr.Close();
                        }
                        

                        if (hshPoList != null && hshPoList.Count > 0)
                        {
                            foreach (string key in hshPoList.Keys)
                            {
                                clsPurchaseOrder purchaseOrder = hshPoList[key] as clsPurchaseOrder;
                                if (purchaseOrder != null)
                                {
                                    poList.Add(purchaseOrder);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        //lblMsg.Text = ex.Message;
                    }
                }
            }
            else
            {
                sbError.Append("\nInventory is not assigned to selected company");
            }

            return sbError;
        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpCompany.SelectedIndex > 0)
            {
                ViewState["userid"] = clsCompany.GetCompanyUser(Convert.ToInt32(dpCompany.SelectedValue));
                lnlURL.Visible = true;
                lnlURL.Text = "Company Stores";
                lnlURL.NavigateUrl = "/CompanyDisplay.aspx?cid=" + dpCompany.SelectedValue.ToString();
     
            }
            else
                ViewState["userid"] = null;
        }

    }
}

