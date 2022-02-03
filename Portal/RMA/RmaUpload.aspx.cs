using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Web.UI;

namespace avii.RMA
{
    public partial class RmaUpload : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/BulkRMA/";
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!this.IsPostBack)
            {
                DateTime warrantyExpieryDate = DateTime.MinValue;
                                            
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnReason_Click(object sender, EventArgs e)
        {

            RegisterStartupScript("jsUnblockDialog", "unblockReasonDialog();");
        }
        protected void btnState_Click(object sender, EventArgs e)
        {

            RegisterStartupScript("jsUnblockDialog", "unblockStateDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkRmaNum = (LinkButton)e.Item.FindControl("lnkRmaNum");
                lnkRmaNum.OnClientClick = "openDialogAndBlock('RMA Detail', '" + lnkRmaNum.ClientID + "')";

            }
        }
        protected void lnkRmaNum_OnCommand(object sender, CommandEventArgs e)
        {
            if (dpCompany.SelectedIndex > 0)
            {
                bool temp = false;
                string rmas = e.CommandArgument.ToString();
                string[] array = rmas.Split(',');
                string rmaNumber = array[0];
                string tempRmaNumber = array[1];
                if ("none" == rmaNumber.ToLower())
                {
                    rmaNumber = tempRmaNumber;
                    temp = true;
                }
                string companyAccountNumber = string.Empty;//dpCompany.SelectedValue;


                if (!string.IsNullOrEmpty(rmaNumber))
                {

                    Control tmp1 = LoadControl("../controls/RmaItems.ascx");
                    avii.Controls.RmaItems ctlRmaDetails = tmp1 as avii.Controls.RmaItems;
                    pnlRMA.Controls.Clear();
                    ctlRmaDetails.BindRMA(rmaNumber, temp);

                    pnlRMA.Controls.Add(ctlRmaDetails);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



                }
            }
            else
                lblMsg.Text = "Select a customer!";
        }
        protected void btnViewAssignedRmas_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            if (Session["rmas"] != null)
            {
                List<Classes.RMA> rmasList = Session["rmas"] as List<Classes.RMA>;
                rptRMA.DataSource = rmasList;
                rptRMA.DataBind();
                lblCount.Text = "Total count: " + rmasList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string comment = txtComment.Text;
            string filename = string.Empty;

            if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            Session["rmas"] = null;
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (Session["rmalist"] != null)
            {
                List<Classes.RMA> rmasList = new List<Classes.RMA>();
                List<Classes.RMA> rmaToAdd = null;

                List<Classes.RMA> rmaList = Session["rmalist"] as List<Classes.RMA>;
                int n = 0;
                int poRecordCount = 0;
                int returnValue = 0;
                string poRmaErrorMessage, poEmailErrorMessage, poEsnErrorMessage, poReasonErrorMessage, errorMessage;
                string companyAccountNumber = string.Empty;
                companyAccountNumber = dpCompany.SelectedValue;
                errorMessage = poRmaErrorMessage = poEmailErrorMessage = poEsnErrorMessage = poReasonErrorMessage = string.Empty;
                double totalChunk = 0;
                StringBuilder sbRmaNumberError = new StringBuilder();
                try
                {

                    totalChunk = (double)rmaList.Count / 5;
                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                    int rmaCount = 5;

                    //var esnToUpload;

                    for (int i = 0; i < n; i++)
                    {
                        rmaToAdd = new List<Classes.RMA>();
                        if (rmaList.Count < 5)
                            rmaCount = rmaList.Count;
                        var rmaToUpload = (from item in rmaList.Take(rmaCount) select item).ToList();

                        errorMessage = poRmaErrorMessage = poEmailErrorMessage = poEsnErrorMessage = poReasonErrorMessage = string.Empty;
                
                        //Upload RMAs
                        rmaToAdd = avii.Classes.RmaOperations.SaveBulkRMA(rmaToUpload, companyAccountNumber, userID, "U", filename, comment, out poRecordCount, out poRmaErrorMessage, out errorMessage);

                        sbRmaNumberError.Append(poRmaErrorMessage);

                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            if (returnValue == -2)
                            {
                                lblMsg.Text = "User not assigned to this customer please contact to aerovoice administrator";
                                return;
                            }

                            errorMessage = poRmaErrorMessage = poEmailErrorMessage = poEsnErrorMessage = poReasonErrorMessage = string.Empty;
                

                            rmasList.AddRange(rmaToAdd);
                            if (i != 0)
                                poRecordCount += poRecordCount;

                            rmaList.RemoveRange(0, rmaCount);
                        }
                        else
                        {
                            lblMsg.Text = errorMessage;
                            return;
                        }
                    }

                    
                    if (returnValue == 0 && poRecordCount > 0)
                    {
                        lblMsg.Text = "Uploaded successfully <br /> Record count: " + poRecordCount;
                        pnlSubmit.Visible = false;
                        Session["rmas"] = rmasList;
                        rptRMA.DataSource = null;
                        rptRMA.DataBind();
                        Session["rmalist"] = rmasList;
                        btnSubmit.Visible = false;
                        btnUpload.Visible = false;
                        btnView.Visible = true;
                        lblCount.Text = string.Empty;
                    }
                    else
                    {
                        if (returnValue == -2)
                        {
                            lblMsg.Text = "User not assigned to this customer please contact to aerovoice administrator";

                        }
                        else
                        {
                            string returnErrorMessage = string.Empty;
                            if (poRecordCount > 0)
                            {

                                if (sbRmaNumberError.Length == 0)
                                    returnErrorMessage = "Uploaded successfully";
                                else
                                    if (sbRmaNumberError.Length > 0)
                                        returnErrorMessage = sbRmaNumberError.ToString() + "RMA not uploaded";
                                
                                returnErrorMessage = returnErrorMessage + "<br /> Record count: " + poRecordCount;

                            }
                            else
                            {


                                if (sbRmaNumberError.Length > 0)
                                    returnErrorMessage = sbRmaNumberError.ToString() + "RMA not uploaded";
                                returnErrorMessage = returnErrorMessage + "<br /> Record count: " + poRecordCount;

                            
                            }
                            pnlSubmit.Visible = false;
                            Session["rmas"] = rmasList;
                            rptRMA.DataSource = null;
                            rptRMA.DataBind();
                            Session["rmalist"] = rmasList;
                            btnSubmit.Visible = false;
                            btnUpload.Visible = false;
                            btnView.Visible = true;
                            lblCount.Text = string.Empty;
                            lblMsg.Text = returnErrorMessage;

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
                lblMsg.Text = "session expire!";
            }

        }


        protected void btnValidates_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            if (Session["rmalist"] != null)
            {
                List<Classes.RMA> rmaList = Session["rmalist"] as List<Classes.RMA>;
                int poRecordCount = 0;
                int returnValue = 0;
                string poRmaErrorMessage, poEmailErrorMessage, poEsnErrorMessage, poReasonErrorMessage, errorMessage;
                string companyAccountNumber = string.Empty;
                companyAccountNumber = dpCompany.SelectedValue;
                errorMessage = poRmaErrorMessage = poEmailErrorMessage = poEsnErrorMessage = poReasonErrorMessage = string.Empty;

                try
                {
                    List<RMADetail> rmaDetailList = new List<RMADetail>();
                    var emailList = rmaList.Select(m => new RmaValidation { RmaNumber = m.RmaNumber, Email = m.Email, StateCode = m.State }).Distinct().ToList();
                    foreach (Classes.RMA rma in rmaList)
                    {
                        rmaDetailList.AddRange(rma.RmaDetails);
                    }
                    var esnList = rmaDetailList.Select(m => new EsnValidation { ESN = m.ESN, Reason = m.Reason }).Distinct().ToList();

                    
                    errorMessage = poRmaErrorMessage = poEmailErrorMessage = poEsnErrorMessage = poReasonErrorMessage = string.Empty;
                    //validate RMAs
                    returnValue = avii.Classes.RmaOperations.ValidateRMA(emailList, esnList, companyAccountNumber, out poRecordCount, out poRmaErrorMessage, out poEmailErrorMessage, out poEsnErrorMessage, out poReasonErrorMessage, out errorMessage);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        //if (returnValue == 0 && string.IsNullOrEmpty(poRmaErrorMessage) && string.IsNullOrEmpty(poEmailErrorMessage) && string.IsNullOrEmpty(poEsnErrorMessage) && string.IsNullOrEmpty(poReasonErrorMessage))
                        if (returnValue == 0)
                        {
                            lblConfirm.Text = "Fulfillment file is ready to upload";
                            btnSubmit.Visible = true;
                            btnSubmit2.Visible = true;
                            pnlSubmit.Visible = true;
                            btnUpload.Visible = false;
                            btnValidate.Visible = false;
                            btnValidate2.Visible = false;


                        }
                        else
                        {
                            if (returnValue == -2)
                            {
                                lblMsg.Text = "User not assigned to this customer please contact to aerovoice administrator";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(poRmaErrorMessage))
                                    errorMessage = poRmaErrorMessage + " invalid state code!";

                                if (!string.IsNullOrEmpty(poEmailErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poEmailErrorMessage + " invalid Email address";
                                    else
                                        errorMessage = errorMessage + "<br />" + poEmailErrorMessage + " invalid Email address";
                                } if (!string.IsNullOrEmpty(poEsnErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poEsnErrorMessage + "ESN(s) RMA already exists";
                                    else
                                        errorMessage = errorMessage + "<br />" + poEsnErrorMessage + " ESN(s) RMA already exists";
                                }
                                if (!string.IsNullOrEmpty(poReasonErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poReasonErrorMessage + " invalid reason";
                                    else
                                        errorMessage = errorMessage + "<br />" + poReasonErrorMessage + " invalid reason";
                                }
                                lblMsg.Text = errorMessage;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
            {
                lblMsg.Text = "session expire!";
            }
        }

        private void UploadRMA()
        {
            Session["rmalist"] = null;
            StringBuilder sbPOWriteError = new StringBuilder();
            //int companyID = 0;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            string companyInfo = string.Empty;
            string companyAccountNumber = string.Empty;
            bool columnsIncorrectFormat = false;
            string invalidColumns = string.Empty;

            if (dpCompany.SelectedIndex < 1)
            {
                lblMsg.Text = "Please select Company";
                // throw new Exception("Please select Company");

            }
            else
            {
                //bool saved = false;
                if (flnUpload.HasFile)
                {
                    //companyInfo = dpCompany.SelectedValue;
                    //string[] array = companyInfo.Split(',');
                    //companyID = Convert.ToInt32(array[0]);
                    //companyAccountNumber = array[1];
                    companyAccountNumber = dpCompany.SelectedValue;
                    string fileName = UploadFile();
                    string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                    extension = extension.ToLower();
                    if (extension == ".csv")
                    {

                        if (!string.IsNullOrEmpty(fileName))
                        {
                            List<Classes.RMA> rmaList = new List<Classes.RMA>();
                            //int companyID = Convert.ToInt32(dpCompany.SelectedValue);
                            StringBuilder sbError = WriteRMA(fileName, ref rmaList, out columnsIncorrectFormat, out invalidColumns);
                            //if (lblMsg.Text != string.Empty)
                            //{

                            //}
                            //else 
                            if (sbError != null && sbError.Length > 0)
                            {
                                lblMsg.Text = sbError.ToString();
                                if (rmaList != null && rmaList.Count > 0 && !columnsIncorrectFormat)
                                {
                                    rptRMA.DataSource = rmaList;
                                    rptRMA.DataBind();
                                    Session["rmalist"] = rmaList;
                                }
                            }
                            else if (rmaList != null && rmaList.Count > 0 && !columnsIncorrectFormat)
                            {
                                rptRMA.DataSource = rmaList;
                                rptRMA.DataBind();
                                Session["rmalist"] = rmaList;
                                lblCount.Text = "Record count: " + rmaList.Count;
                                if (lblMsg.Text == string.Empty)
                                {
                                    lblConfirm.Text = "Fulfillment order file is ready to validate";
                                    btnUpload.Visible = false;
                                    btnValidate.Visible = true;
                                    btnValidate2.Visible = true;

                                    btnSubmit.Visible = false;
                                    btnSubmit2.Visible = false;
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

                                rptRMA.DataSource = null;
                                rptRMA.DataBind();
                                Session["rmalist"] = null;
                                lblCount.Text = string.Empty;
                                if (columnsIncorrectFormat)
                                {
                                    if (!string.IsNullOrEmpty(invalidColumns))
                                    {
                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                    }
                                    else
                                    {
                                        lblMsg.Text = "File format not matching missing required columns";

                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "Uploaded file does not have any Purchase Order to save";
                                }

                            }

                        }

                    }
                    else
                        lblMsg.Text = "Invalid file!";
                }
                else
                {
                    lblMsg.Text = "Select a file!";
                }
            }

        }

        private bool ValidateColumns(string[] headerArray, out string invalidColumns)
        {
            bool columnsIncorrectFormat = false;
            invalidColumns = string.Empty;
            if (headerArray[0].Trim() != "rmanumber")
            {
                invalidColumns = headerArray[0];
                columnsIncorrectFormat = true;
            }
            if (headerArray[1].Trim() != "rmadate")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[1];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[1];
                columnsIncorrectFormat = true;
            }
            if (headerArray[2].Trim() != "customername")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[2];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[2];
                columnsIncorrectFormat = true;
            }
            if (headerArray[3].Trim() != "address")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[3];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[3];
                columnsIncorrectFormat = true;
            }
            if (headerArray[4].Trim() != "city")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[4];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[4];
                columnsIncorrectFormat = true;
            }
            if (headerArray[5].Trim() != "state")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[5];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[5];
                columnsIncorrectFormat = true;
            }
            if (headerArray[6].Trim() != "zip")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[6];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[6];
                columnsIncorrectFormat = true;
            }
            if (headerArray[7].Trim() != "email")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[7];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[7];
                columnsIncorrectFormat = true;
            }
            if (headerArray[8].Trim() != "phone")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[8];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[8];

                columnsIncorrectFormat = true;
            }
            if (headerArray[9].Trim() != "esn")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[9];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[9];
                columnsIncorrectFormat = true;
            }
            if (headerArray[10].Trim() != "receivedon")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[10];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[10];
                columnsIncorrectFormat = true;
            }
            if (headerArray[11].Trim() != "reason")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[11];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[11];
                columnsIncorrectFormat = true;
            }
            return columnsIncorrectFormat;

        }
        
        private StringBuilder WriteRMA(string fileName, ref List<avii.Classes.RMA> rmaList, out bool columnsIncorrectFormat, out string invalidColumns)
        {
            int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["bulkrmaesn"]);
            //bulkmaxesn
            StringBuilder sbError = new StringBuilder();
            invalidColumns = string.Empty;
            columnsIncorrectFormat = false;
            avii.Classes.RMA rma = null;
            //RMADetail rmaDetail = null;
            Hashtable hshRmaList = new Hashtable();
            Hashtable hshEsnDuplicateCheck = new Hashtable();
            if (!string.IsNullOrEmpty(fileName))
            {
                if (rmaList == null)
                {
                    rmaList = new List<avii.Classes.RMA>();
                }
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string companyName = dpCompany.SelectedItem.Text;
                        string rmaNumber = string.Empty;
                        string rmaNum, contactName, address1, city, state, zip, email, phone, reason, esn, comment, notes;
                        rmaNum = contactName = address1 = city = state = zip = email = phone = reason = esn = comment = notes = string.Empty;
                        int warranty = 0;
                        int callTime = 0;
                        string line;
                        string[] tempData;
                        int ctr = 0;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            callTime = 0;

                            if (!string.IsNullOrEmpty(line) && i == 0)
                            {
                                
                                line = line.ToLower();
                                line = line.Trim();
                                string[] headerArray = line.Split(',');
                                if (headerArray.Length < 12)
                                {
                                    lblMsg.Text = "File format not matching missing required columns";
                                    columnsIncorrectFormat = true;
                                }
                                else
                                {
                                    i = 1;

                                    columnsIncorrectFormat = ValidateColumns(headerArray, out invalidColumns);
                                }
                            }
                            else if (!string.IsNullOrEmpty(line) && i > 0 && columnsIncorrectFormat == false)
                            {
                                line = "@" + line;
                                
                                line = line.Replace(@"\,", "||");


                                tempData = line.Split(',');
                                if (tempData.Length >= 12)
                                {

                                    if ((!string.IsNullOrEmpty(tempData[0]) && tempData[0].Trim().Length > 0))
                                    {
                                        rmaNumber = tempData[0].Trim().Replace("@","");
                                        
                                        if (!hshRmaList.ContainsKey(rmaNumber))
                                        {
                                            rma = new Classes.RMA();

                                        }
                                        else
                                        {
                                            rma = hshRmaList[rmaNumber] as Classes.RMA;
                                        }

                                        if (!string.IsNullOrEmpty(rmaNumber))
                                        {
                                            rma.RmaNumber = rmaNumber;
                                            rma.TempRmaNumber = "None";
                                            DateTime currentDate = DateTime.Now;

                                            DateTime rmaDate;
                                            DateTime receivedOnDate;
                                            if (!string.IsNullOrEmpty(tempData[1]))
                                            {
                                                if (DateTime.TryParse(tempData[1], out rmaDate))
                                                {
                                                    TimeSpan diffResult = currentDate - rmaDate;
                                                    //int days = diffResult.Days;

                                                    if (diffResult.Days > 90)
                                                    {
                                                        sbError.Append("\nInvalid RMA Date! Can not create RMA before 90 days from today date.");

                                                        throw new Exception("Invalid RMA Date! Can not create RMA before 90 days from today date.");
                                                    }
                                                    else
                                                        rma.RmaDate = rmaDate;


                                                }
                                                else
                                                {
                                                    sbError.Append("\nRMA Date does not have correct format(MM/DD/YYYY)");
                                                    throw new Exception("RMA Date does not have correct format(MM/DD/YYYY)");

                                                }
                                            }
                                            else
                                                rma.RmaDate = DateTime.Now;



                                            if (rmaNum != rmaNumber)
                                            {
                                                contactName = tempData.Length > 2 ? tempData[2].Trim() : string.Empty;
                                                address1 = tempData.Length > 3 ? tempData[3].Trim() : string.Empty;
                                                city = tempData.Length > 4 ? tempData[4].Trim() : string.Empty;
                                                state = tempData.Length > 5 ? tempData[5].Trim().ToUpper() : string.Empty;
                                                zip = tempData.Length > 6 ? tempData[6].Trim() : string.Empty;
                                                email = tempData.Length > 7 ? tempData[7].Trim() : string.Empty;
                                                phone = tempData.Length > 8 ? tempData[8].Trim() : string.Empty;
                                                comment = tempData.Length > 12 ? tempData[12].Trim() : string.Empty;
                                                ctr = 0;

                                                if (string.IsNullOrEmpty(rmaNumber) || string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(city)
                                                    || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zip) || string.IsNullOrEmpty(email)
                                                    || string.IsNullOrEmpty(phone))
                                                { 
                                                    sbError.Append("\nMissing required data! ");
                                                }
                                                if (state.Length != 2)
                                                {
                                                    sbError.Append("\n " + state + " Invalid state code! ");
                                                }


                                            }
                                            ctr = ctr + 1;

                                            if (ctr > maxEsn)
                                            {
                                                lblMsg.Text = rmaNumber + " RMA# have more than " + maxEsn + " ESNs, maximum of " + maxEsn + " ESNs are allowed in one RMA request.";
                                                sbError.Append(rmaNumber + " RMA# have more than " + maxEsn + " ESNs, maximum of " + maxEsn + " ESNs are allowed in one RMA request.");
                                                throw new Exception(rmaNumber + " RMA# have more than " + maxEsn + " ESNs, maximum of " + maxEsn + " ESNs are allowed in one RMA request.");
                                            }
                                            rmaNum = rmaNumber;
                                            
                                            
                                            rma.RmaContactName = contactName.Trim();// 
                                            rma.Address = address1.Trim().Replace("||",",");
                                            //rma.City = city.Trim();
                                            rma.City = city.Trim();
                                            rma.State = state.Trim();
                                            rma.Zip = zip.Trim();
                                            rma.Phone = phone.Trim();
                                            rma.Email = email;
                                            rma.Comment = comment.Trim().Replace("||", ",");
                                            rma.RMAUserCompany.CompanyName = companyName;
                                            esn = tempData[9].Trim();

                                            reason = tempData.Length > 11 ? tempData[11].Trim() : string.Empty;
                                            notes = tempData.Length > 16 ? tempData[16].Trim() : string.Empty;
                                            if (tempData.Length > 13)
                                            {
                                                if (!string.IsNullOrEmpty(tempData[13].Trim()))
                                                {
                                                    if (int.TryParse(tempData[13].Trim(), out callTime))
                                                    {

                                                    }
                                                    else
                                                        callTime = 0;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(tempData[10]))
                                            {
                                                if (DateTime.TryParse(tempData[10], out receivedOnDate))
                                                {
                                                    TimeSpan diffResult2 = currentDate - receivedOnDate;
                                                    //int receivedOndays = diffResult2.Days;

                                                    if (diffResult2.Days > 90)
                                                    {
                                                        sbError.Append("\nInvalid received on date! Can not create RMA before 90 days from today date.");
                                                        //throw new Exception("Invalid received on date! Can not create RMA after 30 from today date.");
                                                    }
                                                }
                                                else
                                                    receivedOnDate = currentDate;
                                            }
                                            else
                                                receivedOnDate = currentDate;

                                            if (string.IsNullOrEmpty(esn))
                                            {
                                                sbError.Append("\nMissing ESN! ");

                                            }
                                            if (string.IsNullOrEmpty(reason))
                                            {
                                                sbError.Append("\nMissing reason! ");

                                            }
                                            if (string.IsNullOrEmpty(receivedOnDate.ToString()))
                                            {
                                                sbError.Append("\nMissing received on date! ");

                                            }

                                            DateTime warrantyExpieryDate = Convert.ToDateTime("1/1/1900");
                                            if (tempData.Length > 15)
                                            {
                                                if (!string.IsNullOrEmpty(tempData[15]))
                                                {
                                                    if (DateTime.TryParse(tempData[15], out warrantyExpieryDate))
                                                    {

                                                    }
                                                    else
                                                        warrantyExpieryDate = Convert.ToDateTime("1/1/1900");
                                                }
                                                else
                                                    warrantyExpieryDate = Convert.ToDateTime("1/1/1900");
                                            }
                                            else
                                                warrantyExpieryDate = Convert.ToDateTime("1/1/1900");
                                            if (tempData.Length > 14)
                                            {
                                                if ("warranty" == tempData[14].Trim().ToLower())
                                                    warranty = 1;
                                                if ("out of warranty" == tempData[14].Trim().ToLower())
                                                    warranty = 2;

                                            }

                                            
                                            if (hshEsnDuplicateCheck.ContainsKey(esn))
                                            {
                                                //lblMsg.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                                                sbError.Append("\n" + string.Format("Duplicate ESN ({0}) is found", esn));
                                                //throw new Exception("\n" + string.Format("Duplicate ESN ({0}) is found", esn));
                                            }
                                            else
                                            {
                                                hshEsnDuplicateCheck.Add(esn, esn);

                                            }
                                            rma.RmaDetails.Add(new RMADetail() { ESN = esn, CallTime = callTime, Notes = notes.Replace("||",","), Reason = reason, RecievedOn = receivedOnDate, Warranty = 1, WarrantyExpieryDate = warrantyExpieryDate, ReasonTxt = reason });
                                            
                                        }
                                        else
                                        {
                                            sbError.Append(string.Format("\nRMA# {0} have empty ESN ", rma.RmaNumber));
                                        }

                                        if (rma != null && rma.RmaDetails != null && rma.RmaDetails.Count > 0)
                                        {
                                            if (hshRmaList.ContainsKey(rma.RmaNumber))
                                                hshRmaList[rma.RmaNumber] = rma;
                                            else
                                                hshRmaList.Add(rma.RmaNumber, rma);
                                        }
                                    }

                                    else
                                    {
                                        throw new Exception("Uploaded file does not have correct Format");
                                    }

                                }
                                else
                                {
                                    lblMsg.Text = "File format not matching";
                                    sbError.Append("\nFile format not matching");
                                }
                            }
                            //ctr = ctr + 1;
                        }
                        sr.Close();
                    }
                    if (hshRmaList != null && hshRmaList.Count > 0 && !columnsIncorrectFormat)
                    {
                        foreach (string key in hshRmaList.Keys)
                        {
                            Classes.RMA rmaObj = hshRmaList[key] as Classes.RMA;
                            if (rmaObj != null)
                            {
                                rmaList.Add(rmaObj);
                            }
                        }
                    }
                    else
                    {
                        if (columnsIncorrectFormat)
                        {
                            if (!string.IsNullOrEmpty(invalidColumns))
                            {
                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                            }
                            else
                            {
                                lblMsg.Text = "File format not matching missing required columns";

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    lblMsg.Text = ex.Message;
                    //throw ex;
                }
            }
            return sbError;
        }
        
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadRMA();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["rmalist"] = null;
            Session["rmas"] = null;
            rptRMA.DataSource = null;
            rptRMA.DataBind();
            //rptRMA.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            btnValidate.Visible = false;
            btnView.Visible = false;
            dpCompany.SelectedIndex = 0;

        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["BulkRMAFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["BulkRMAFilesStoreage"].ToString();
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