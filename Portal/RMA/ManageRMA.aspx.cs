using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.RMA;

namespace avii.RMA
{
    public partial class ManageRMA : System.Web.UI.Page
    {
        protected int RmaGUID;
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
                ReadOlnyAccess();
                BindCompany();
                BindStates();
                BindRMAStatuses(0);
                BindRmaDetailStatuses();
               // ddlStatus.SelectedIndex = 1;
                lblRmaNumber.Text = "To be assigned";
                txtRMADate.Text = DateTime.Now.ToShortDateString();
                //btnSubmitRMA.Text = "Save Changes";
                int userID = 0, companyID = 0;
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;

                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                    ViewState["userid"] = userInfo.UserGUID;
                    //txtLocationCode.Enabled = false;
                }
                tdReceiveRma.Visible = false;
                ddlReceive.Visible = false;

                if (Session["adm"] == null)
                {
                    trStatus.Visible = false;
                    txtLanComments.Enabled = false;
                    dpCompany.SelectedValue = userInfo.CompanyGUID.ToString();
                    //trRmaStatus.Visible = false;
                    tdReceiveRma.Visible = false;
                    ddlReceive.Visible = false;

                    dpCompany.Enabled = false;

                }
                //RmaGUID = 10;
                if (Session["rmaguid"] != null)
                {
                    tdRMA.InnerHtml = "&nbsp;Edit RMA";
                    companyID = Convert.ToInt32(Session["companyID"]);
                    RmaGUID = Convert.ToInt32(Session["rmaguid"]);
                    Session["rmaGUID"] = null;
                    ViewState["rmaGUID"] = RmaGUID;
                    trSearch.Visible = false;
                    trExistingRma.Visible = false;
                    pnlUpload.Visible = false;
                    lblCreatedBy.Visible = false;
                    ddlUser.Visible = false;
                    BindReceiveStatuses();
                    BindUserStores(0, companyID);
                    //BindUser(companyID);

                    GetRMADetail(RmaGUID, userID);

                    if (Session["adm"] != null)
                    {
                        trRmaStatus.Visible = true;
                        tdReceiveRma.Visible = true;
                        ddlReceive.Visible = true;

                    }
                }
                else
                {
                    ddlStatus.SelectedIndex = 1;
                }
            }
        }

        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;
                        btnSubmit.Visible = false;
                        btnUpload.Visible = false;
                    }
                }

            }

        }
        //To obtain RMA and RMA Detail records while editing
        private void GetRMADetail(int rmaGUID, int userID)
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            //// List<RMADetail> rmaDetaillist = new List<RMADetail>();
            //List<RMAEsnLookUp> esnLookup = new List<RMAEsnLookUp>();
            // RMADetail objRMADETAIL = new RMADetail();
            RmaModel RmaInfo = rmaOperation.GetRMAInfo(rmaGUID);

            if (RmaInfo != null)
            {
                //hdnShipDate.Value = RmaInfo.MaxShipmentDate.ToShortDateString();
                ViewState["ShipDate"] = RmaInfo.MaxShipmentDate.ToShortDateString();
                txtAddress.Text = RmaInfo.Address1;
                txtCity.Text = RmaInfo.City;
                txtCustName.Text = RmaInfo.ContactName;
                //txtState.Text = RmaInfo.State;
                dpState.SelectedValue = RmaInfo.State;
                txtZip.Text = RmaInfo.ZipCode;
                lblRmaNumber.Text = RmaInfo.RmaNumber;
                txtRMACustomerNumber.Text = RmaInfo.CustomerRMANumber;
                txtRMACustomerNumber.Enabled = false;
                //lblRMACustNo.Visible = false;
                txtRMADate.Text = RmaInfo.RmaDate.ToShortDateString();
                //txtRemarks.Text = RmaInfo.Comment;
                ddlStatus.SelectedValue = RmaInfo.RmaStatusID.ToString();
                //txtAVComments.Text = RmaInfo.AVComments;
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                txtPhone.Text = RmaInfo.ContactNumber;
                txtEmail.Text = RmaInfo.Email;

                if (RmaInfo.ReceiveStatusID > 0)
                    ddlReceive.SelectedValue = RmaInfo.ReceiveStatusID.ToString();
                if (RmaInfo.TriageStatusID > 0)
                    ddlTriageStatus.SelectedValue = RmaInfo.TriageStatusID.ToString();

                if (!string.IsNullOrEmpty(RmaInfo.StoreID))
                    ddlStoreID.SelectedValue = RmaInfo.StoreID;

                //             hdncompanyname.Value = RmaInfo.RMAUserCompany.CompanyName;
                trRMA.Visible = true;


            }


            Session["rmadetaillist"] = RmaInfo.RmaDetail;// esnLookup;

            rptRma.DataSource = RmaInfo.RmaDetail;
            rptRma.DataBind();
            Session["rmaDocList"] = RmaInfo.RmaDocumentList;// esnLookup;
            if (RmaInfo.RmaDocumentList != null && RmaInfo.RmaDocumentList.Count > 0)
            {
                for (int i = 0; i < RmaInfo.RmaDocumentList.Count; i++)
                {
                    //if (userID == 0 && RmaInfo.RmaDocumentList[i].DocType.ToUpper() == "RL")
                    //{
                    //    lblPrint.Visible = true;
                    //    chkPrint.Visible = true;

                    //}
                    if (lblRmaDocs.Text == string.Empty)
                        lblRmaDocs.Text = RmaInfo.RmaDocumentList[i].RmaDocName;
                    else
                        lblRmaDocs.Text = lblRmaDocs.Text + ", " + RmaInfo.RmaDocumentList[i].RmaDocName;
                }
            }


            // hdnRmaItemcount.Value = DlRma.Items.Count.ToString();
            //ddlStatus.SelectedValue = RmaInfo.RmaStatusID.ToString();

            if (userID > 0)
            {
                if (Session["adm"] == null)
                {
                        txtLanComments.Enabled = false;
                    
                    DropDownList ddl_Status1 = (DropDownList)rptRma.Items[rptRma.Items.Count - 1].FindControl("ddlStatus");
                    ddl_Status1.Visible = false;
                    ddlStatus.Visible = false;
                    lblStatus.Visible = true;
                    //hdnStatus.Value = "0";
                }
                else
                    txtComments.Enabled = false;

            }
        }

        private void BindCompany()
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
                dpCompany.DataValueField = "companyID";
                dpCompany.DataTextField = "companyName";
                dpCompany.DataBind();
                //ListItem item = new ListItem("", "0");
                //ddlCompany.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        private void BindStates()
        {
            DataTable dataTable = avii.Classes.clsCompany.GetState(0);

            dpState.DataSource = dataTable;
            dpState.DataTextField = "StateCodeName";
            dpState.DataValueField = "Statecode";
            dpState.DataBind();
            ListItem item = new ListItem("", "");
            dpState.Items.Insert(0, item);

            // dpState.SelectedValue = "CA";

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            // RmaGUID = 10;
            int companyID = 0;
            string poNumber, trackingNumber;
            poNumber = trackingNumber = string.Empty;
            txtLanComments.Text = string.Empty;
            txtComments.Text = string.Empty;
            
            if (Session["adm"] == null)
            {
                txtLanComments.Enabled = false;
                trStatus.Visible = false;
            }
            else
            {
                txtComments.Enabled = false;
            }

            lblMsg.Text = string.Empty;
            trRMA.Visible = false;
            trExistingRma.Visible = false;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                poNumber = txtPO.Text.Trim();
                trackingNumber = txtTrackingNo.Text.Trim();
                if (string.IsNullOrEmpty(poNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select second search criteria!";
                }
                else
                {
                    RmaModel rmaInfo = rmaOperation.GetRMASearch(companyID, poNumber, trackingNumber);
                    if (rmaInfo != null)
                    {
                        if (rmaInfo.PoStatus.ToLower() == "shipped")
                        {
                            ViewState["POID"] = rmaInfo.POID;

                            txtAddress.Text = rmaInfo.Address1 + " " + rmaInfo.Address2;
                            txtCity.Text = rmaInfo.City;
                            txtCustName.Text = rmaInfo.ContactName;
                            txtPhone.Text = rmaInfo.ContactNumber;
                            //txtRMADate.Text = "";
                            txtZip.Text = rmaInfo.ZipCode;
                            dpState.SelectedValue = rmaInfo.State;

                            if (rmaInfo.ExistingRmaDetail.Count > 0)
                            {
                                trExistingRma.Visible = true;
                                rptExistingRma.DataSource = rmaInfo.ExistingRmaDetail;
                                rptExistingRma.DataBind();
                            }
                            else
                            {
                                trExistingRma.Visible = false;
                                rptExistingRma.DataSource = null;
                                rptExistingRma.DataBind();
                            }

                            if (rmaInfo.RmaDetail.Count > 0)
                            {
                                trRMA.Visible = true;
                                BindUserStores(0, companyID);
                                BindUser(companyID);
                                ddlStoreID.SelectedValue = rmaInfo.StoreID;

                                if (rmaInfo.RmaDetail.Count <= 50)
                                {
                                    pnlESN.Visible = true;
                                    pnlUpload.Visible = false;
                                    rptRma.DataSource = rmaInfo.RmaDetail;
                                    rptRma.DataBind();
                                }
                                else
                                {
                                    pnlESN.Visible = false;
                                    pnlUpload.Visible = true;
                                    rptRma.DataSource = null;
                                    rptRma.DataBind();
                                }
                            }
                            else
                            {
                                // trExistingRma.Visible = false;
                                rptRma.DataSource = null;
                                rptRma.DataBind();
                            }


                        }
                        else
                            lblMsg.Text = "RMA cannot be created with Fulfillment status <strong>" + rmaInfo.PoStatus + "</strong>";
                    }
                    else
                        lblMsg.Text = "No record found";
                }
            }
            else
            {

                lblMsg.Text = "Customer is required!";
            }


        }
        private void BindUser(int companyID)
        {
            string sortExpression = "UserName";
            string sortDirection = "ASC";
            string sortBy = sortExpression + " " + sortDirection;
            int statusID = 2;
            List<avii.Classes.UserRegistration> userList = avii.Classes.RegistrationOperation.GetUserInfoList(companyID, string.Empty, sortBy, statusID);
            if (userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserID";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                //avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                //if (userInfo != null)
                //    ddlUser.SelectedValue = userInfo.UserGUID.ToString();

            }
            else
            {
                lblMsg.Text = "No user found";
            }

        }
        private void BindUserStores(int userID, int companyID)
        {
            List<SV.Framework.Admin.StoreLocation> storeList = SV.Framework.Admin.UserStoreOperation.GetUserStoreLocationList(companyID, userID);
            if (storeList != null && storeList.Count > 0)
            {
                lblMsg.Text = string.Empty;
                ddlStoreID.Visible = true;
                Session["userstore"] = storeList;
                ddlStoreID.DataSource = storeList;
                ddlStoreID.DataValueField = "StoreID";
                ddlStoreID.DataTextField = "StoreName";
                ddlStoreID.DataBind();
                ddlStoreID.Items.Insert(0, new ListItem("", ""));
                //ddlStoreID.SelectedIndex = 1;
                //lblStore.Visible = true;
            }
            else
            {
                Session["userstore"] = null;
                ddlStoreID.Items.Clear();
                ddlStoreID.DataSource = null;
                ddlStoreID.DataBind();
               // lblStore.Visible = false;
                ddlStoreID.Visible = false;
                lblMsg.Text = "No store assigned to this user, please contact administrator to get more information.";

            }
        }
        //To autogenerate new RMA Number
        private string GenerateRMA()
        {
            string rmaNumber = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            string st = ConfigurationManager.AppSettings["av_rma"].ToString();
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            try
            {
                // rmaGUID = RMAUtility.getRMAguid(0);
                month = dt.Month.ToString();
                day = dt.Day.ToString();
                if (month.Length < 2)
                    month = "0" + month;
                if (day.Length < 2)
                    day = "0" + day;
                rmaNumber = st + dt.Year.ToString() + month + day; //+ "-" + rmaGUID;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            return rmaNumber;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            lblMsg.Text = string.Empty;
            string successMessageAdmin = "RMA is successfully updated.";
            string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>Lan Global Returns Department</b>.";

            RmaModel rmaModel = new RmaModel();
            RmaDetailModel rmaDetail = null;
            List<RmaDetailModel> rmaDetails = new List<RmaDetailModel>();
            List<RmaDocument> rmaDocList = new List<RmaDocument>(); //RmaDocumentsList();

            string rmaNumber = GenerateRMA();
            int rmaGUID = 0, poid = 0, statusId = 1, receiveStatus = 0, triageStatus = 0;
            int companyID = 0;
            int userid = 0, loginUserID = 0;

           // userid = 496;
            //loginUserID = Convert.ToInt32(ViewState["userid"]);
            int maxshipmentdays = 10;
            bool IsRmaComplete = false;
            string rma_status = "Pending";
            string dateMsg = "Invalid RMA Date! Can not create RMA before 365 days back.";
            string custName, address, city, state, zip, phone, email;
            DateTime currentDate = DateTime.Now;
            DateTime rmaDate = new DateTime();
            DataTable rmaDT = new DataTable();
            if (ViewState["POID"] != null)
                poid = Convert.ToInt32(ViewState["POID"]);

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            if (ViewState["userid"] != null)
                int.TryParse(ViewState["userid"].ToString(), out loginUserID);
            int.TryParse(ConfigurationSettings.AppSettings["maxshipmentdays"].ToString(), out maxshipmentdays);

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //isEmail = userInfo.IsEmail;
                    loginUserID = userInfo.UserGUID;
                    userid = loginUserID;
                }
            }
            else
            {
                if (rmaGUID == 0)
                    if (ddlUser.SelectedIndex > 0)
                        userid = Convert.ToInt32(ddlUser.SelectedValue);
                    else
                    {
                        lblMsg.Text = "Created by is required!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Created by is required!')</script>", false);

                        return;
                    }
            }
            if (txtRMADate.Text != string.Empty && txtRMADate.Text != null)
            {
                rmaDate = Convert.ToDateTime(txtRMADate.Text);
                TimeSpan diffResult = currentDate - rmaDate;
                if (diffResult.Days > 365)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + dateMsg + "')</script>", false);
                    return;
                }
            }
            if (ddlStatus.SelectedIndex > 0)
                statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            custName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
            address = (txtAddress.Text.Trim().Length > 0 ? txtAddress.Text.Trim() : null);
            city = (txtCity.Text.Trim().Length > 0 ? txtCity.Text.Trim() : null);
            state = (dpState.SelectedIndex > 0 ? dpState.SelectedValue : null);
            zip = (txtZip.Text.Trim().Length > 0 ? txtZip.Text.Trim() : null);
            phone = (txtPhone.Text.Trim().Length > 0 ? txtPhone.Text.Trim() : null);
            email = (txtEmail.Text.Trim().Length > 0 ? txtEmail.Text.Trim() : null);
            rmaModel.Address1 = address;
            rmaModel.City = city;
            rmaModel.ContactName = custName;
            rmaModel.Email = email;

            rmaModel.ContactNumber = phone;
            rmaModel.POID = poid;
            rmaModel.RmaGUID = rmaGUID;
            rmaModel.RmaDate = rmaDate;
            rmaModel.RmaNumber = rmaNumber;
            rmaModel.CustomerRMANumber = txtRMACustomerNumber.Text.Trim();
            rmaModel.State = state;
            rmaModel.RmaStatusID = statusId;
            rmaModel.UserID = userid;
            rmaModel.StoreID = ddlStoreID.SelectedValue;
            rmaModel.ZipCode = zip;
            rmaModel.Comment = txtComments.Text.Trim();
            rmaModel.LanComment = txtLanComments.Text.Trim();
            rmaModel.MaxShipmentDate = DateTime.Now.AddDays(maxshipmentdays);
            rmaModel.Status = ddlStatus.SelectedItem.Text;

            if (rmaGUID > 0)
            {
                if (ddlStatus.SelectedItem.Text.ToLower() == "completed")
                    IsRmaComplete = true;
                rmaModel.MaxShipmentDate = Convert.ToDateTime(ViewState["ShipDate"]);

                if (ddlReceive.SelectedIndex > 0)
                    receiveStatus = Convert.ToInt32(ddlReceive.SelectedValue);
                if (ddlTriageStatus.SelectedIndex > 0)
                    triageStatus = Convert.ToInt32(ddlTriageStatus.SelectedValue);
            }
            rmaModel.ReceiveStatusID = receiveStatus;
            rmaModel.ReceiveStatus = string.Empty;
            if (ddlReceive.SelectedIndex > 0)
                rmaModel.ReceiveStatus = ddlReceive.SelectedItem.Text;

            rmaModel.TriageStatusID = triageStatus;
            int skuQty = 0, rmaQty = 0;
            foreach (RepeaterItem item in rptRma.Items)
            {
                CheckBox chkItem = item.FindControl("chkItem") as CheckBox;
                Label lblESN = item.FindControl("lblESN") as Label;
                TextBox txtQty = item.FindControl("txtQty") as TextBox;
                HiddenField hdQty = item.FindControl("hdQty") as HiddenField;
                TextBox txtNotes = item.FindControl("txtNotes") as TextBox;
                DropDownList dpWarranty = item.FindControl("dpWarranty") as DropDownList;
                DropDownList ddReason = item.FindControl("ddReason") as DropDownList;
                DropDownList dpDisposition = item.FindControl("dpDisposition") as DropDownList;
                DropDownList dpStatus = item.FindControl("ddlStatus") as DropDownList;
                HiddenField hdRmaDetGUID = item.FindControl("hdRmaDetGUID") as HiddenField;
                //HiddenField hdPOID = item.FindControl("hdPOID") as HiddenField;
                HiddenField hdPODID = item.FindControl("hdPODID") as HiddenField;
                //HiddenField hdQty = item.FindControl("hdQty") as HiddenField;
                HiddenField hdItemCompanyGUID = item.FindControl("hdItemCompanyGUID") as HiddenField;
                HiddenField hdTriageNotes = item.FindControl("hdTriageNotes") as HiddenField;
                HiddenField hdTriageStatusID = item.FindControl("hdTriageStatusID") as HiddenField;

                if(dpStatus.SelectedItem.Text.ToLower() == "pending" && IsRmaComplete)
                {
                    lblMsg.Text = "RMA cannot be completed with pending line item(s)";
                    dpStatus.Focus();
                    return;
                }

                if (dpStatus.SelectedItem.Text.ToLower() == "rts (return to stock)" && !IsRmaComplete)
                {
                    lblMsg.Text = "RMA should be completed for line item(s) status  "+ dpStatus.SelectedItem.Text;
                    ddlStatus.Focus();
                    ///TESTing
                    return;
                }


                if (hdRmaDetGUID.Value == "")
                    hdRmaDetGUID.Value = "0";
                if (hdTriageStatusID.Value == "")
                    hdTriageStatusID.Value = "0";
                if (chkItem.Checked)
                {
                    rmaDetail = new RmaDetailModel();
                    rmaQty = Convert.ToInt32(txtQty.Text);
                    rmaDetail.ESN = lblESN.Text;

                    if(string.IsNullOrEmpty(rmaDetail.ESN))
                    {
                        if (rmaQty > 0)
                        {
                            skuQty = Convert.ToInt32(hdQty.Value);

                            if (rmaQty > skuQty)
                            {
                                lblMsg.Text = "Quantity cannot be greater than " + skuQty;
                                txtQty.Focus();
                                return;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Quantity cannot be 0";
                            txtQty.Focus();
                            return;
                        }
                    }
                    rmaDetail.Notes = txtNotes.Text.Trim();
                    rmaDetail.Quantity = rmaQty;
                    rmaDetail.RmaDetGUID = Convert.ToInt32(hdRmaDetGUID.Value);
                    rmaDetail.POD_ID = Convert.ToInt32(hdPODID.Value);
                    rmaDetail.ItemCompanyGUID = Convert.ToInt32(hdItemCompanyGUID.Value);
                    rmaDetail.TriageNotes = hdTriageNotes.Value;
                    rmaDetail.TriageStatusID = Convert.ToInt32(hdTriageStatusID.Value);
                    // rmaDetail.Quantity = Convert.ToInt32(txtQty.Value);

                    if (dpWarranty.SelectedIndex > 0)
                        rmaDetail.Warranty = Convert.ToInt32(dpWarranty.SelectedValue);
                    else
                    {
                        dpWarranty.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Warranty is required!')</script>", false);

                        return;
                    }
                    if (ddReason.SelectedIndex > 0)
                        rmaDetail.Reason = Convert.ToInt32(ddReason.SelectedValue);
                    else
                    {
                        ddReason.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Reason is required!')</script>", false);

                        return;
                    }
                    if (dpDisposition.SelectedIndex > 0)
                        rmaDetail.DispositionID = Convert.ToInt32(dpDisposition.SelectedValue);
                    else
                    {
                        if (rmaDetail.RmaDetGUID > 0)
                        {
                            dpDisposition.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Disposition is required!')</script>", false);

                            return;
                        }
                    }
                    if (dpStatus.SelectedIndex > 0)
                    {
                        rmaDetail.StatusID = Convert.ToInt32(dpStatus.SelectedValue);
                        rmaDetail.Status = dpStatus.SelectedItem.Text;
                    }
                    else
                    {
                        if (rmaDetail.RmaDetGUID == 0)
                        {
                            rmaDetail.StatusID = 1;
                            //  dpDisposition.Focus();
                            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Disposition is required!')</script>", false);

                            //return;
                        }
                    }
                    rmaDetails.Add(rmaDetail);
                }
            }
            if (Session["rmaDocList"] != null)
                rmaDocList = (List<RmaDocument>)Session["rmaDocList"];
            rmaModel.RmaDocumentList = rmaDocList;
            

            string returnResult = rmaOperation.ValidateRmaFields(rmaModel);
            if (!string.IsNullOrEmpty(returnResult))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + returnResult + "')</script>", false);
                lblMsg.Text = returnResult;
                //return;

            }
            else
            {
                if (rmaDetails != null && rmaDetails.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Please select atleast one line item!')</script>", false);

                    return;
                }
                rmaModel.RmaDetail = rmaDetails; 
                returnResult = string.Empty;
                returnResult = rmaOperation.ValidateRmaStatusWithLineItemsStatuses(rmaModel);
                if (!string.IsNullOrEmpty(returnResult))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + returnResult + "')</script>", false);
                    lblMsg.Text = returnResult;
                }
                else
                {
                    rmaModel.RMANUMBERAPI = string.Empty;
                    rmaModel.IsAPI = false;
                    rmaModel.UserID = userid;
                    rmaModel.LoginUserID = loginUserID;
                    string RMASource = "RMAWithPO";
                    rmaModel.RMASource = RMASource;

                    RmaResponse response = rmaOperation.RMAInsertUpdate(rmaModel);
                    if (response.ReturnCode == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(response.CustomerRMANumberExists))
                        {
                            lblMsg.Text = response.CustomerRMANumberExists + " Customer RMA Number already exists!";
                            return;
                        }
                        else
                        {
                            btnSubmit.Enabled = false;

                            lblRmaNumber.Text = response.RmaNumber;
                            if (Session["adm"] != null)
                                lblMsg.Text = string.Format(successMessageAdmin, rmaNumber);
                            else
                                lblMsg.Text = string.Format(successMessage, rmaNumber);
                        }
                    }
                    else
                    {
                        lblMsg.Text = "RMA not saved!";
                        return;
                    }
                }
            }
        }
        private void BindRMAStatuses(int companyID)
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetCustomerRMAStatusList(companyID, true);
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    Session["customerRMAStatusList"] = customerRMAStatusList;
                    ddlStatus.DataSource = customerRMAStatusList;
                    ddlStatus.DataValueField = "StatusID";
                    ddlStatus.DataTextField = "StatusDescription";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindRmaDetailStatuses()
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetRmaDetailStatusList();
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    Session["RMAdetailStatusList"] = customerRMAStatusList;                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if(ViewState["rmaGUID"]!=null)
                Response.Redirect("~/RMA/RMAQueryNew.aspx?search=rma", false);
            else
            {
                Reset();
            }
        }
        private void Reset()
        {
            Session["rmaDocList"] = null;
           // lblConfirm.Text = string.Empty;
            //ViewState["validate"] = null;
            //ViewState["allowRMA"] = null;
            int companyID = 0;
            int userID = 0;
            int rmaGUID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            txtRMADate.Text = DateTime.Now.ToShortDateString();
            txtComments.Text = "";
            //txtPo_num.Text = "";
            txtEmail.Text = string.Empty;
            txtPO.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            // txtState.Text = string.Empty;
            dpState.SelectedIndex = 0;
            txtZip.Text = string.Empty;
            //chkAll.Checked = false;

            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 1;
                txtLanComments.Text = string.Empty;
                
                if (rmaGUID == 0)
                {
                    companyID = 0;
                   // ViewState["companyID"] = companyID;
                    //hdncompanyname.Value = string.Empty;
                }
            }
            else
            { txtLanComments.Enabled = false; }
            trRMA.Visible = false;
            tblTriage.Visible = false;
            trExistingRma.Visible = false;
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ValidateESN();
        }

        private void ValidateESN()
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            rptRma.DataSource = null;
            rptRma.DataBind();
            int POID = 0;
            string errorMessage = string.Empty;
            pnlESN.Visible = false;
            pnlUpload.Visible = true;

            POID = Convert.ToInt32(ViewState["POID"]);
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();

            bool columnsIncorrectFormat = false;            
            try
            {
                if (fu.PostedFile.FileName.Trim().Length == 0)
                {
                    lblMsg.Text = "Select file to upload";
                }
                else
                {
                    if (fu.PostedFile.ContentLength > 0)
                    {
                        string fileName = UploadFile();
                        string extension = Path.GetExtension(fu.PostedFile.FileName);
                        extension = extension.ToLower();
                        string invalidColumns = string.Empty;
                        ESNList assignESN = null;
                        List<ESNList> esnList = new List<ESNList>();

                        if (extension == ".csv")
                        {
                            using (StreamReader sr = new StreamReader(fileName))
                            {
                                string line;
                                string esn;//;
                                int i = 0;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (!string.IsNullOrEmpty(line) && i == 0)
                                    {
                                        i = 1;
                                        line = line.ToLower();
                                        string[] headerArray = line.Split(',');
                                        if (headerArray.Length > 0)
                                        {
                                            if (headerArray[0].Trim() != "esn")
                                            {
                                                invalidColumns = headerArray[0];
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
                                        esn = string.Empty;
                                        string[] arr = line.Split(',');
                                        try
                                        {
                                            assignESN = new ESNList();
                                            esn = arr[0].Trim();
                                            if (string.IsNullOrEmpty(esn))
                                            { }
                                            else
                                            {
                                                assignESN.ESN = esn;
                                                if (string.IsNullOrEmpty(esn))
                                                {
                                                    if (string.IsNullOrEmpty(esn))
                                                        lblMsg.Text = "Missing ESN data";
                                                }
                                                if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                {
                                                    lblMsg.Text = "Duplicate " + esn + " ESN(s) exists in the file";

                                                }
                                                else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                {
                                                    hshESN.Add(esn, esn);
                                                }
                                                esnList.Add(assignESN);
                                                esn = string.Empty;
                                            }
                                        }
                                        //catch (ApplicationException ex)
                                        //{
                                        //    throw ex;
                                        //}
                                        catch (Exception exception)
                                        {
                                            lblMsg.Text = exception.Message;
                                        }
                                    }
                                }
                                sr.Close();
                            }

                            int InValid = 0;
                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                            {
                                List<RmaDetailModel> rmaList = rmaOperation.ValidateESN(POID, esnList);
                                if (rmaList != null && rmaList.Count > 0)
                                {
                                    rptRma.DataSource = rmaList;
                                    rptRma.DataBind();

                                    foreach (RmaDetailModel item in rmaList)
                                    {
                                        if (!string.IsNullOrEmpty(item.ErrorMessage))
                                        {
                                            InValid = 1;
                                        }
                                    }

                                    if (InValid == 1)
                                    {
                                        errorMessage = "Invalid ESN(s)";
                                        lblMsg.Text = "Invalid ESN(s)";
                                    }
                                }
                                if (!string.IsNullOrEmpty(errorMessage))
                                {
                                    lblMsg.Text = errorMessage;
                                    return;
                                }
                                if (lblMsg.Text == string.Empty)
                                {
                                    pnlESN.Visible = true;
                                    pnlUpload.Visible = false;

                                }
                                else
                                {
                                    pnlESN.Visible = false;
                                    pnlUpload.Visible = true;

                                }

                            }
                            else
                            {

                                rptRma.DataSource = null;
                                rptRma.DataBind();


                                if (esnList != null && esnList.Count == 0)
                                {
                                    if (columnsIncorrectFormat)
                                    {
                                        if (!string.IsNullOrEmpty(invalidColumns))
                                            lblMsg.Text = invalidColumns + " column name is not correct";
                                        else
                                            lblMsg.Text = "File format is not correct";
                                    }
                                    else
                                        lblMsg.Text = "There is no ESN to upload";

                                }
                                if (esnList != null)
                                {
                                    if (columnsIncorrectFormat)
                                    {
                                        if (!string.IsNullOrEmpty(invalidColumns))
                                            lblMsg.Text = invalidColumns + " column name is not correct";
                                        else
                                            lblMsg.Text = "File format is not correct";
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
        private void BindReceiveStatuses()
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<RmaReceiveStatus> statusList = new List<RmaReceiveStatus>();// avii.Classes.CompanyOperations.GetReceiveRMAStatusList();
                if (Session["receivestatusList"] != null)
                {
                    statusList = Session["receivestatusList"] as List<RmaReceiveStatus>;
                }
                else
                {
                    statusList = rmaOperation.GetReceiveRMAStatusList();
                }
                if (statusList != null && statusList.Count > 0)
                {
                    Session["receivestatusList"] = statusList;
                    ddlReceive.DataSource = statusList;
                    ddlReceive.DataValueField = "ReceiveStatusID";
                    ddlReceive.DataTextField = "ReceiveStatus";
                    ddlReceive.DataBind();
                    ddlReceive.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
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
            Int32 maxFileSize = 3145728;
            actualFilename = System.IO.Path.GetFileName(fu.PostedFile.FileName);
            
            string fileStoreLocation = Server.MapPath("~/UploadedData/");

            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            fu.PostedFile.SaveAs(fileStoreLocation + actualFilename);


            ViewState["filename"] = actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than 3 MB");// + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

        protected void rptRma_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddReason = (DropDownList)e.Item.FindControl("ddReason");
                DropDownList dpWarranty = (DropDownList)e.Item.FindControl("dpWarranty");
                DropDownList dpDisposition = (DropDownList)e.Item.FindControl("dpDisposition");
                DropDownList ddl_RmaItemStatus = (DropDownList)e.Item.FindControl("ddlStatus");
                HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
                HiddenField hdnReason = (HiddenField)e.Item.FindControl("hdnReason");
                HiddenField hdnWarranty = (HiddenField)e.Item.FindControl("hdnWarranty");
                HiddenField hdnDisposition = (HiddenField)e.Item.FindControl("hdnDisposition");

                if (Session["RMAdetailStatusList"] != null)
                {
                    List<CustomerRMAStatus> customerRMAStatusList = Session["RMAdetailStatusList"] as List<CustomerRMAStatus>;
                    ddl_RmaItemStatus.DataSource = customerRMAStatusList;
                    ddl_RmaItemStatus.DataValueField = "StatusID";
                    ddl_RmaItemStatus.DataTextField = "StatusDescription";
                    ddl_RmaItemStatus.DataBind();
                    ddl_RmaItemStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
                ddl_RmaItemStatus.SelectedValue = hdnStatus.Value;
                ddReason.SelectedValue = hdnReason.Value;
                dpWarranty.SelectedValue = hdnWarranty.Value;
                dpDisposition.SelectedValue = hdnDisposition.Value;

            }
        }


        protected void btnRmaDocUpload_Click(object sender, EventArgs e)
        {
            RmaDocumentsList();
        }
        private List<RmaDocument> RmaDocumentsList()
        {
            List<RmaDocument> rmaDocList = new List<RmaDocument>();

            int rmaDocID = 0;
            int rmaDocIDA1 = 0;
            int rmaDocIDA2 = 0;
            int rmaDocIDA3 = 0;
            int rmaDocIDA4 = 0;
            string files = string.Empty;
            string custFileName = string.Empty;
            string fileName1 = string.Empty;
            string fileName2 = string.Empty;
            string fileName3 = string.Empty;
            string fileName4 = string.Empty;

            string custImagePath = string.Empty;
            string imagePath1 = string.Empty;
            string imagePath2 = string.Empty;
            string imagePath3 = string.Empty;
            string imagePath4 = string.Empty;
            string extension = string.Empty;
            string targetFolder = Server.MapPath("~");
            int randomNo = GetRandomNumber(1, 99999);

            if (Session["rmaDocList"] != null)
            {
                List<RmaDocument> rmaDocList2 = new List<RmaDocument>();
                rmaDocList2 = Session["rmaDocList"] as List<RmaDocument>;
                string docType = string.Empty;
                if (rmaDocList2 != null && rmaDocList2.Count > 0)
                {
                    var doc = (from item in rmaDocList2 where item.DocType.Equals("C") select item).ToList();
                    if (doc != null && doc.Count > 0)
                        rmaDocID = Convert.ToInt32(doc[0].RmaDocID);

                    var doc1 = (from item in rmaDocList2 where item.DocType.Equals("A1") select item).ToList();
                    if (doc1 != null && doc1.Count > 0)
                        rmaDocIDA1 = Convert.ToInt32(doc1[0].RmaDocID);

                    var doc2 = (from item in rmaDocList2 where item.DocType.Equals("A2") select item).ToList();
                    if (doc2 != null && doc2.Count > 0)
                        rmaDocIDA2 = Convert.ToInt32(doc2[0].RmaDocID);

                    var doc3 = (from item in rmaDocList2 where item.DocType.Equals("A3") select item).ToList();
                    if (doc3 != null && doc3.Count > 0)
                        rmaDocIDA3 = Convert.ToInt32(doc1[0].RmaDocID);
                    var doc4 = (from item in rmaDocList2 where item.DocType.Equals("A4") select item).ToList();
                    if (doc4 != null && doc4.Count > 0)
                        rmaDocIDA4 = Convert.ToInt32(doc4[0].RmaDocID);

                }

            }

            targetFolder = targetFolder + "\\Documents\\RMA\\";
            if (!Directory.Exists(Path.GetFullPath(targetFolder)))
            {
                Directory.CreateDirectory(Path.GetFullPath(targetFolder));
            }
            if (fuDoc.HasFile)
            {
                //custFileName = System.IO.Path.GetFileName(fupRmaDocc.PostedFile.FileName);
                RmaDocument rmaDocObjc = new RmaDocument();
                extension = Path.GetExtension(fuDoc.PostedFile.FileName);
                //custFileName = txtRmaNum.Text + randomNo.ToString() + "C1" + extension;
                custFileName = "RMA" + randomNo.ToString() + "C1" + extension;
                rmaDocObjc.RmaDocID = rmaDocID;

                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    custImagePath = targetFolder + custFileName;
                    if (!File.Exists(Path.GetFullPath(custImagePath)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fuDoc.PostedFile.SaveAs(custImagePath);
                        rmaDocObjc.RmaDocName = custFileName;
                        rmaDocObjc.DocType = "C";
                        files = custFileName;
                    }
                    else
                    {
                        n = n + 1;
                        randomNo = GetRandomNumber(1, 99999);
                        custFileName = "RMA" + randomNo.ToString() + "C1" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjc);
            }
            if (fupRmaDocA1.HasFile)
            {
                RmaDocument rmaDocObjA1 = new RmaDocument();
                extension = Path.GetExtension(fupRmaDocA1.PostedFile.FileName);
                fileName1 = "RMA" + randomNo.ToString() + "A1" + extension;
                rmaDocObjA1.RmaDocID = rmaDocIDA1;
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xslx" || extension == "jpeg")
                {

                }
                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    imagePath1 = targetFolder + fileName1;
                    if (!File.Exists(Path.GetFullPath(imagePath1)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocA1.PostedFile.SaveAs(imagePath1);
                        rmaDocObjA1.RmaDocName = fileName1;
                        rmaDocObjA1.DocType = "A1";
                        if (files == string.Empty)
                            files = fileName1;
                        else
                            files = files + ", " + fileName1;
                    }
                    else
                    {
                        n = n + 1;
                        randomNo = GetRandomNumber(1, 99999);
                        fileName1 = "RMA" + randomNo.ToString() + "A1" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA1);

            }
            if (fupRmaDocA2.HasFile)
            {
                RmaDocument rmaDocObjA2 = new RmaDocument();

                extension = Path.GetExtension(fupRmaDocA2.PostedFile.FileName);
                fileName2 = "RMA" + randomNo.ToString() + "A2" + extension;
                rmaDocObjA2.RmaDocID = rmaDocIDA2;

                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    imagePath2 = targetFolder + fileName2;
                    if (!File.Exists(Path.GetFullPath(imagePath2)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocA2.PostedFile.SaveAs(imagePath2);
                        rmaDocObjA2.RmaDocName = fileName2;
                        rmaDocObjA2.DocType = "A2";
                        if (files == string.Empty)
                            files = fileName2;
                        else
                            files = files + ", " + fileName2;
                    }
                    else
                    {
                        n = n + 1;
                        randomNo = GetRandomNumber(1, 99999);
                        fileName2 = "RMA" + randomNo.ToString() + "A2" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA2);

            }
            if (fupRmaDocA3.HasFile)
            {
                RmaDocument rmaDocObjA3 = new RmaDocument();

                extension = Path.GetExtension(fupRmaDocA3.PostedFile.FileName);
                fileName3 = "RMA" + randomNo.ToString() + "A3" + extension;
                rmaDocObjA3.RmaDocID = rmaDocIDA3;

                int n = 1;
                for (int i = 0; i < n; i++)
                {

                    imagePath3 = targetFolder + fileName3;
                    if (!File.Exists(Path.GetFullPath(imagePath3)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocA3.PostedFile.SaveAs(imagePath3);
                        rmaDocObjA3.RmaDocName = fileName3;
                        rmaDocObjA3.DocType = "A3";
                        if (files == string.Empty)
                            files = fileName3;
                        else
                            files = files + ", " + fileName3;
                    }
                    else
                    {
                        n = n + 1;
                        randomNo = GetRandomNumber(1, 99999);
                        fileName3 = "RMA" + randomNo.ToString() + "A3" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA3);
            }
            if (fupRmaDocA4.HasFile)
            {
                RmaDocument rmaDocObjA4 = new RmaDocument();

                extension = Path.GetExtension(fupRmaDocA4.PostedFile.FileName);
                fileName4 = "RMA" + randomNo.ToString() + "A4" + extension;
                rmaDocObjA4.RmaDocID = rmaDocIDA4;

                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    imagePath4 = targetFolder + fileName4;
                    if (!File.Exists(Path.GetFullPath(imagePath4)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocA4.PostedFile.SaveAs(imagePath4);
                        rmaDocObjA4.RmaDocName = fileName4;
                        rmaDocObjA4.DocType = "A4";

                        if (files == string.Empty)
                            files = fileName4;
                        else
                            files = files + ", " + fileName4;
                    }
                    else
                    {
                        n = n + 1;
                        randomNo = GetRandomNumber(1, 99999);
                        fileName4 = "RMA" + randomNo.ToString() + "A4" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA4);


            }
            Session["rmaDocList"] = rmaDocList;
            lblMsg.Text = files;

            return rmaDocList;

        }
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        private static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        protected void btntriage_Click(object sender, EventArgs e)
        {
            Button btnAddtriage = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnAddtriage.NamingContainer;
            int itemindex = item.ItemIndex;
            int rmadelGUID = 0;

            if (Session["adm"] == null)
            {
                //trStatus.Visible = false;
                txtLanComments.Enabled = false;
            }
            else { txtComments.Enabled = false; }

            ViewState["rmadelitemindex"] = itemindex;
            List<RmaDetailModel> rmaEsnLookup = Session["rmadetaillist"] as List<RmaDetailModel>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemindex)
                {
                    txtTriageNotes.Text = rmaEsnLookup[itemindex].TriageNotes;
                    if (rmaEsnLookup[itemindex].TriageStatusID > 0)
                        ddlTriage.SelectedValue = rmaEsnLookup[itemindex].TriageStatusID.ToString();
                    else
                        ddlTriage.SelectedIndex = 0;

                    lblSKU.Text = rmaEsnLookup[itemindex].SKU;
                    lblESN.Text = rmaEsnLookup[itemindex].ESN;

                    rmadelGUID = Convert.ToInt32(rmaEsnLookup[itemindex].RmaDetGUID);
                    ViewState["rmadelGUID"] = rmadelGUID;
                    tblTriage.Visible = true;
                   // tblPopup.Visible = true;
                }
            }
            if (Session["adm"] == null)
            {
                btnTriage.Visible = false;
               // tblPopup.Visible = false;

            }

        }

        protected void btnTriage_Click(object sender, EventArgs e)
        {
            int rmadelitemindex, triageStatusID;
            if (ViewState["rmaGUID"] != null)
                RmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);


            rmadelitemindex = 0;
            string triageNotes = txtTriageNotes.Text.Trim();
            List<RmaDetailModel> rmaEsnLookup = Session["rmadetaillist"] as List<RmaDetailModel>;

            if (ViewState["rmadelitemindex"] != null)
                int.TryParse(ViewState["rmadelitemindex"].ToString(), out rmadelitemindex);

            int.TryParse(ddlTriage.SelectedValue, out triageStatusID);

            if (triageStatusID == 0)
                triageStatusID = 1;

            //ViewState["rmadelitemindex"]
            rmaEsnLookup[rmadelitemindex].TriageStatusID = triageStatusID;
            rmaEsnLookup[rmadelitemindex].TriageNotes = triageNotes;
            //lblSKU.Text = rmaEsnLookup[rmadelitemindex].SKU;
            //lblESN.Text = rmaEsnLookup[rmadelitemindex].ESN;

            Session["rmadetaillist"] = rmaEsnLookup;
            rptRma.DataSource = rmaEsnLookup;
            rptRma.DataBind();
            tblTriage.Visible = false;
            if (Session["adm"] == null)
            {
                //trStatus.Visible = false;
                txtLanComments.Enabled = false;
            }
            else { txtComments.Enabled = false; }

            // tblPopup.Visible = false;

        }

        protected void btnTriageCancel_Click(object sender, EventArgs e)
        {
            tblTriage.Visible = false;
            if (Session["adm"] == null)
            {
                //trStatus.Visible = false;
                txtLanComments.Enabled = false;
            }
            else { txtComments.Enabled = false; }

            //tblPopup.Visible = false;
        }
    }
}