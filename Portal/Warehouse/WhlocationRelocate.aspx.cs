using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;

using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using System.Collections;

namespace avii.Warehouse
{
    public partial class WhlocationRelocate : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindCustomer();

                GetWarehouseloationDetail();

                int companyID = Convert.ToInt32(dpCompany.SelectedValue);
                BindWhLocation(companyID);
                BindUsers();
            }
        }
        private void BindUsers()
        {

            SV.Framework.Customer.CustomerOperation custOperations = SV.Framework.Customer.CustomerOperation.CreateInstance<SV.Framework.Customer.CustomerOperation>();
            List<SV.Framework.Models.Customer.Users> users = custOperations.GetInternalUsers(237);
            ddlUsers.DataSource = users;
            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataTextField = "UserName";
            ddlUsers.DataBind();

            ListItem newList = new ListItem("", "");
            ddlUsers.Items.Insert(0, newList);

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void GetWarehouseloationDetail()
        {
            List<WhLocationInfo> whLocations = Session["whLocationreplacement"] as List<WhLocationInfo>;
            int index = 0;
            if (Session["rowIndex"] != null)
            {
                index = Convert.ToInt32(Session["rowIndex"]);

                if (whLocations != null && whLocations.Count > 0)
                {
                    WhLocationInfo whLocationInfo = whLocations[index];
                    if (whLocationInfo != null)
                    {
                        dpCompany.SelectedValue = whLocationInfo.CompanyID.ToString();
                        dpCompany.Enabled = false;
                        lblWhCity.Text = whLocationInfo.WarehouseCity;
                        lblWhLocation.Text = whLocationInfo.WarehouseLocation;
                        lblCategoryName.Text = whLocationInfo.CategoryName;
                        lblSKU.Text = whLocationInfo.SKU;
                        lblProductName.Text = whLocationInfo.ItemName;
                        txtQuantity.Text = whLocationInfo.Quantity.ToString();
                        hdQty.Value = txtQuantity.Text;
                        ViewState["InventoryType"] = whLocationInfo.InventoryType.ToLower();
                        ViewState["ItemCompanyGUID"] = whLocationInfo.ItemCompanyGUID;
                        ViewState["StorageID"] = whLocationInfo.StorageID;

                        if (whLocationInfo.InventoryType.ToLower() == "esn")
                        {
                            trUpload.Visible = true;
                            btnSubmit.Visible = false;
                           //  btnValidate.Visible = false;
                            btnCancel.Visible = false;
                            txtQuantity.Enabled = false;
                        }
                        else
                        {
                            trUpload.Visible = false;
                            btnSubmit.Visible = true;
                            btnCancel.Visible = true;
                        }
                    }
                }
            }
        }
        private void BindWhLocation(int companyID)
        {
            lblMsg.Text = "";
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            List<WarehouseStorage> warehouseStorages = warehouseOperation.GetWarehouseStorage("", "", companyID);
            if (warehouseStorages != null && warehouseStorages.Count > 0)
            {
                var itemToRemove = warehouseStorages.Single(r => r.WarehouseLocation == lblWhLocation.Text);
                warehouseStorages.Remove(itemToRemove);

                ddlWhLocation.DataSource = warehouseStorages;
                ddlWhLocation.DataValueField = "WarehouseLocation";
                ddlWhLocation.DataTextField = "WarehouseLocation";

                ddlWhLocation.DataBind();
                ListItem newList = new ListItem("", "");
                ddlWhLocation.Items.Insert(0, newList);
            }
            else
            {
                ddlWhLocation.DataSource = null;
                ddlWhLocation.DataBind();
                lblMsg.Text = "No location found for this customer!";

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["InventoryType"] != null)
            {
                SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

                string InventoryType = Convert.ToString(ViewState["InventoryType"]);
                int storageID = 0, ItemCompanyGUID = 0, Qty = 0, OldQty = 0, returnResult = 0, userID = 0, loginUserID = 0;
                string whLocationOld, whLocationNew, errorMessage, Comment;
                Comment = txtComment.Text.Trim();
                List<EsnInfo> esnList = default;
                if (ddlWhLocation.SelectedIndex > 0)
                {
                    if (ddlUsers.SelectedIndex > 0)
                    {
                        whLocationOld = lblWhLocation.Text;
                        whLocationNew = ddlWhLocation.SelectedValue;
                        ItemCompanyGUID =Convert.ToInt32( ViewState["ItemCompanyGUID"]);
                        userID = Convert.ToInt32(ddlUsers.SelectedValue);
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                            loginUserID = userInfo.UserGUID;

                        if (InventoryType == "esn")
                        {
                            esnList = new List<EsnInfo>();
                            esnList = Session["loctionesns"] as List<EsnInfo>;
                            Qty = esnList.Count;
                            returnResult = warehouseOperation.WarehouseLocationESNUpdate(esnList, userID, ItemCompanyGUID, whLocationOld, whLocationNew, Qty, loginUserID, Comment, out errorMessage);
                            if (returnResult > 0)
                            {
                                lblMsg.Text = "Updated successfully";
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                lblMsg.Text = errorMessage;
                            }

                        }
                        else
                        {
                            storageID = Convert.ToInt32(ViewState["StorageID"]);
                            Qty = Convert.ToInt32(txtQuantity.Text);
                            OldQty = Convert.ToInt32(hdQty.Value);
                            if (Qty > 0)
                            {
                                if (Qty <= OldQty)
                                {

                                    returnResult = warehouseOperation.WarehouseLocationNonEsnUpdate(storageID, userID, ItemCompanyGUID, whLocationOld, whLocationNew, Qty, OldQty, loginUserID, Comment, out errorMessage);

                                    if (returnResult > 0)
                                    {
                                        lblMsg.Text = "Updated successfully";
                                        btnSubmit.Visible = false;
                                    }
                                    else
                                    {
                                        lblMsg.Text = errorMessage;
                                    }
                                }
                                else
                                    lblMsg.Text = "Quantity can not be greater than assigned quantity";
                            }
                            else
                            {
                                lblMsg.Text = "Quantity can not be 0 or less";
                            }
                        }
                    }
                    else
                        lblMsg.Text = "User is required!";
                }
                else
                {
                    lblMsg.Text = "Location is required!";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {

        }

        protected void btnUploadValidate_Click(object sender, EventArgs e)
        {
            UploadInventoryInfo();
        }
        private void UploadInventoryInfo()
        {
            SV.Framework.Inventory.WarehouseOperation Operations = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

            List<EsnInfo> esnList = new List<EsnInfo>();
            EsnInfo esnDetail = null;
            string errorMessage = string.Empty;
            int companyID = Convert.ToInt32(dpCompany.SelectedValue);
            bool IsValidate = true;
            lblCounts.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            bool IsFileSizeValid = true;
            bool columnsIncorrectFormat = false;
            string whLocation = lblWhLocation.Text;
            rptESN.DataSource = null;
            rptESN.DataBind();
            tblESN.Visible = false;

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
                        string fileName = UploadFile(out IsFileSizeValid);
                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                        extension = extension.ToLower();
                        string invalidColumns = string.Empty;

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
                                            try
                                            {

                                                if (arr.Length > 0)
                                                {
                                                    esn = arr[0].Trim();
                                                }
                                                if (string.IsNullOrEmpty(esn))
                                                {
                                                    lblMsg.Text = "Missing required data";
                                                }
                                                esnDetail = new EsnInfo();

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
                                                esnDetail.ESN = esn;
                                                esnList.Add(esnDetail);
                                                esn = string.Empty;
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
                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                            {
                                Session["loctionesns"] = esnList;
                                int n = 0;
                                double totalChunk = 0;
                                try
                                {
                                    totalChunk = (double)esnList.Count / 3000;
                                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                    int esnCount = 3000;
                                    //int skip = 1000;
                                    int listLength = esnList.Count;
                                    List<EsnInfo> esnToUpload = null;
                                    //var esnToUpload;
                                    //for (int i = 0; i < n; i++)
                                    for (int i = 0; i < listLength; i = i + 3000)
                                    {
                                        esnToUpload = new List<EsnInfo>();
                                        esnToUpload = (from item in esnList.Skip(i).Take(esnCount) select item).ToList();

                                        //int returnValue = 0;
                                        //string AlreadyInUseICCIDMessase = string.Empty, ICCIDNotExistsMessase = string.Empty, InvalidICCIDMessase = string.Empty, AlreadyMappedESNMessase = string.Empty;

                                        esnList = Operations.WarehouseLocationEsnValidate(esnList, companyID, whLocation);
                                        if (esnList != null && esnList.Count > 0)
                                        {
                                            var Errors = (from item in esnList where (item.ErrorMessage != "") select item).ToList();
                                            if (Errors != null && Errors.Count > 0)
                                            {
                                                errorMessage = Errors[0].ErrorMessage;
                                                IsValidate = false;
                                            }

                                            rptESN.DataSource = esnList;
                                            rptESN.DataBind();
                                            Session["esnList"] = esnList;
                                            tblESN.Visible = true;
                                            lblCounts.Text = "Total count: " + esnList.Count;

                                            if (IsValidate && string.IsNullOrEmpty(errorMessage))
                                            {
                                                btnSubmit.Visible = true;
                                                btnCancel.Visible = true;
                                                trUpload.Visible = false;
                                            }
                                            else
                                            {
                                                if (!IsValidate)
                                                {
                                                    //btnValidate.Visible = false;
                                                    trUpload.Visible = true;
                                                }

                                                if (!string.IsNullOrEmpty(errorMessage))
                                                    lblMsg.Text = errorMessage;// + " Customer order number already exists!";
                                            }
                                        }
                                        else
                                        {
                                            // gvEsn.DataSource = null;
                                            // gvEsn.DataBind();
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

                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                // gvEsn.DataSource = null;
                                //gvEsn.DataBind();

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

        private string UploadFile(out bool IsValid)
        {
            IsValid = true;
            string fileStoreLocation = "~/UploadedData/";
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["FulfillmentFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentFilesStoreage"].ToString();
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
                        int fileSize = maxFileSize / (1024 * 1024);
                        //lblMsg.Text = "File size is greater than " + fileSize + " MB";
                        IsValid = false;
                        //throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

    }

}