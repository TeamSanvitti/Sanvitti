using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
//using avii.Classes;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web.Services;
using System.IO;
using System.Text;
using SV.Framework.Models.Common;
using SV.Framework.Models.RMA;

namespace avii
{
    public partial class RMA_Form : System.Web.UI.Page
    {
        private string successMessageAdmin = "RMA is successfully updated.";
        private string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>Lan Global Returns Department</b>.";

        private List<SV.Framework.Models.Old.RMA.RMADetail> pvtEsnList = null;
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
                if (Session["userInfo"] == null)
                {
                    Response.Redirect(url);
                }
            }
      
            if (!IsPostBack)
            {

                int companyID = 0;
                int userID = 0;
                int rmaGUID = 0;
                btnNewRMA.Visible = false;
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

                // lblPrint.Visible = true;
                // chkPrint.Visible = true;
                BindStates();

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                    userID = userInfo.UserGUID;
                    //txtLocationCode.Enabled = false;
                }

                //string company = ConfigurationManager.AppSettings["company"].ToString();

                int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
                //ViewState["company"] = company;
                ViewState["maxEsn"] = maxEsn;
                //if (Session["adm"] == null)
                //{
                    
                //    divAvc.Disabled = true;
                //    txtAVComments.Enabled = false;
                //}
                //else
                //{
                //    userID = 0;
                //    divAvc.Disabled = false;
                //    txtAVComments.Enabled = true;
                //}
                if (Session["adm"] == null)
                {
                    userID = Convert.ToInt32(Session["UserID"]);
                    divAvc.Disabled = true;
                    txtAVComments.Enabled = false;
                }
                else
                {
                    ViewState["createdby"] = userID;
                    userID = 0;
                    divAvc.Disabled = false;
                    txtAVComments.Enabled = true;
                }
                ViewState["userID"] = userID;

                if (Request["rmaGUID"] != "" && Request["rmaGUID"] != null)
                {
                    rmaGUID = Convert.ToInt32(Request["rmaGUID"]);
                    btnComments.Visible = false;
                }
                else
                {
                    rmaGUID = 0;
                    btnComments.Visible = false;
                }
                ViewState["rmaGUID"] = rmaGUID;

                if (userID > 0)
                {
                    if (companyID == 0)
                    {

                        RMAUserCompany objRMAcompany = rmaUtility.getRMAUserCompanyInfo(-1, "", -1, userID);
                        companyID = objRMAcompany.CompanyID;
                    }
                    if (rmaGUID > 0)
                    {
                        btn_Cancel.Visible = false;
                        btnBack.Visible = true;
                        btnSubmitRMA.Text = "Save Changes";
                    }
                    else
                    {
                        btn_Cancel.Visible = true;
                        btnBack.Visible = false;
                    }
                    lblrmanumber.Visible = false;
                    txtRmaNum.Visible = false;
                    hdnUserID.Value = "1";
                    ddlStatus.Visible = false;
                    chkAll.Visible = false;
                    lblStatus.Text = "Pending";
                    lblStatus.Visible = true;

                    esnPanel.Visible = true;
                    btnpanel.Visible = true;
                    lblCompany.Visible = false;
                    ddlCompany.Visible = false;
                    txtRMADate.Visible = true;
                    lblRMADate.Visible = true;
                    rmadtpanel.Visible = true;
                    // lblrmadate_md.Visible = true;
                    if (Request["mode"] == "esn")
                    {
                        ViewState["esn"] = "esn";
                        //btnSubmitRMA.Enabled = false;
                        // btnValidate.Enabled = false;
                    }
                    //txtLocationCode.Enabled = false;
                    
                }

                else if (userID == 0)
                {
                    if (ddlCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
                    hdnUserID.Value = "0";
                    if (rmaGUID > 0)
                    {
                        //txtLocationCode.Enabled = true;
                        btn_Cancel.Visible = false;
                        btnBack.Visible = true;
                        hdnUserID.Value = "1";
                        esnPanel.Visible = true;
                        btnpanel.Visible = true;
                        lblCompany.Visible = false;
                        ddlCompany.Visible = false;
                        lblrmanumber.Visible = true;
                        txtRmaNum.Visible = true;
                        txtRMADate.Visible = true;
                        lblRMADate.Visible = true;
                        rmadtpanel.Visible = true;
                        btnSubmitRMA.Text = "Save Changes";
                        // lblrmadate_md.Visible = true;
                        if (companyID > 0)
                            BindUserStores(0, companyID);
                        
                    }
                    else
                    {
                        // Donot show RMA Number to the user if they are adding new RMA
                        lblrmanumber.Visible = false;
                        txtRmaNum.Visible = false;
                        btn_Cancel.Visible = true;
                        btnBack.Visible = false;
                        
                        lblrmanumber.Visible = true;
                        txtRmaNum.Visible = true;
                        txtRMADate.Visible = true;
                        lblRMADate.Visible = true;
                        rmadtpanel.Visible = true;
                            // lblrmadate_md.Visible = true;
                        
                        esnPanel.Visible = false;
                        btnpanel.Visible = false;
                        lblCompany.Visible = true;
                        ddlCompany.Visible = true;

                        lblStatus.Visible = false;

                        
                        if (Request["mode"] == "esn")
                        {
                            ViewState["esn"] = "esn";
                            esnPanel.Visible = true;
                            btnpanel.Visible = true;
                        }


                    }

                }

                if (Request["companyID"] != null && Request["companyID"] != "")
                    companyID = Convert.ToInt32(Request["companyID"]);
                ViewState["companyID"] = companyID;

                txtRMADate.Text = DateTime.Now.ToShortDateString();
                if (rmaGUID == 0)
                {
                    txtRMADate.Enabled = false;
                    ddlStatus.Enabled = false;
                }
                if (userID == 0)
                {
                    bindCompany();
                    ddlStatus.SelectedIndex = 1;

                }
                else
                {
                    if (userID > 0)
                    {

                        try
                        {

                            RMAUserCompany objRMAcompany = new RMAUserCompany();
                            objRMAcompany = rmaUtility.getRMAUserCompanyInfo(companyID, "", -1, -1);
                            hdncompanyname.Value = objRMAcompany.CompanyName;

                        }
                        catch (Exception ex)
                        {
                            lbl_msg.Text = ex.Message.ToString();
                        }
                    }
                }


                
                if (rmaGUID > 0)
                {
                    lblrmanumber.Visible = true;
                    txtRmaNum.Visible = true;
                    
                    if (companyID > 0)
                        BindUserStores(0, companyID);
                    else
                        if (userID > 0)
                        {

                            BindUserStores(userID, 0);
                        }

                    BindRMAStatuses(companyID);  
                    GetRMAnRMADetailInfo(rmaGUID, userID);
                    if (userID == 0)
                    {
                        //lblPrint.Visible = true;
                        //chkPrint.Visible = true;

                        lblStatus.Visible = false;
                        ddlStatus.Visible = true;
                        chkAll.Visible = true;
                    }
                    else
                    {
                        ddlStatus.Visible = false;
                        lblStatus.Visible = true;
                        chkAll.Visible = false;
                    }
                }
                else
                {
                    txtRmaNum.Text = "To be assigned";
                    //generateRMA();
                    chkAll.Visible = false;
                    if (userID > 0)
                    {
                        BindRMAStatuses(userInfo.CompanyGUID);
                        BindUserStores(userID, 0);
                    }
                    else
                        BindRMAStatuses(0);

                    if (ViewState["esn"] != null)
                        Dl_esn_bind("");

                    
                }
                if (rmaGUID == 0)
                {
                    ddlStatus.SelectedIndex = 0;


                    lblStatus.Visible = false;
                    ddlStatus.Visible = false;
                    chkAll.Visible = false;
                    status.Visible = false;
                }
                ReadOlnyAccess();
                //btnSubmitRMA.Enabled = false;
            }
        }
        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
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
                        btnValidate.Visible = false;
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
                lbl_msg.Text = ex.Message;
            }
        }
        
        private void BindUserStores(int userID, int companyID)
        {
            //List<avii.Classes.StoreLocation> storeList = avii.Classes.StoreOperations.GetCompanyStoreList(companyID, userID);
            List<SV.Framework.Admin.StoreLocation> storeList = SV.Framework.Admin.UserStoreOperation.GetUserStoreLocationList(companyID, userID);

            if (storeList != null && storeList.Count > 0)
            {
                lbl_msg.Text = string.Empty;

                ddlStoreID.Visible = true;
                Session["userstore"] = storeList;
                ddlStoreID.DataSource = storeList;
                ddlStoreID.DataValueField = "StoreID";
                ddlStoreID.DataTextField = "StoreName";
                ddlStoreID.DataBind();
                ddlStoreID.Items.Insert(0, new ListItem("", ""));
                //ddlStoreID.SelectedIndex = 1;
                lblStore.Visible = true;
            }
            else
            {
                Session["userstore"] = null;
                ddlStoreID.Items.Clear();
                ddlStoreID.DataSource = null;
                ddlStoreID.DataBind();
                lblStore.Visible = false;
                ddlStoreID.Visible = false;
                lbl_msg.Text = "No store assigned to this user, please contact administrator to get more information.";

            }
        }
        [WebMethod]
        public static string UpdateEsnWarranty(string warrantyInfo)
        {
            string returnMessage = string.Empty;
            string[] arr = warrantyInfo.Split(',');
            int itemIndex = 0;
            int warranty = 1;
            itemIndex = Convert.ToInt32(arr[1]);
            warranty = Convert.ToInt32(arr[0]);
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = System.Web.HttpContext.Current.Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                    if (rmaEsnLookup.Count >= itemIndex)
                    {
                        rmaEsnLookup[itemIndex].Warranty = warranty;
                        System.Web.HttpContext.Current.Session["rmadetailslist"] = rmaEsnLookup;
                    }
            }

            return warrantyInfo;
        }
        [WebMethod]
        public static string UpdateEsnDisposition(string dispositionInfo)
        {
            string returnMessage = string.Empty;
            string[] arr = dispositionInfo.Split(',');
            int itemIndex = 0;
            int disposition = 0;
            itemIndex = Convert.ToInt32(arr[1]);
            disposition = Convert.ToInt32(arr[0]);
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = System.Web.HttpContext.Current.Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemIndex)
                {
                    rmaEsnLookup[itemIndex].Disposition = disposition;
                    System.Web.HttpContext.Current.Session["rmadetailslist"] = rmaEsnLookup;
                }
            }

            return dispositionInfo;
        }
        [WebMethod]
        public static string UpdateEsnReason(string reasonInfo)
        {
            string returnMessage = string.Empty;
            string[] arr = reasonInfo.Split(',');
            int itemIndex = 0;
            int reason = 0;
            itemIndex = Convert.ToInt32(arr[1]);
            reason = Convert.ToInt32(arr[0]);
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = System.Web.HttpContext.Current.Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemIndex)
                {
                    rmaEsnLookup[itemIndex].Reason = reason;
                    System.Web.HttpContext.Current.Session["rmadetailslist"] = rmaEsnLookup;
                }
            }

            return reasonInfo;
        }
        [WebMethod]
        public static string UpdateEsnStatus(string statusInfo)
        {
            string returnMessage = string.Empty;
            string[] arr = statusInfo.Split(',');
            int itemIndex = 0;
            int statusID = 0;
            itemIndex = Convert.ToInt32(arr[1]);
            statusID = Convert.ToInt32(arr[0]);
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = System.Web.HttpContext.Current.Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemIndex)
                {
                    rmaEsnLookup[itemIndex].StatusID = statusID;
                    System.Web.HttpContext.Current.Session["rmadetailslist"] = rmaEsnLookup;
                }
            }

            return statusInfo;
        }
       
        //To obtain RMA and RMA Detail records while editing
        private void GetRMAnRMADetailInfo(int rmaGUID, int userID)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            List<SV.Framework.Models.Old.RMA.RMADetail> rmaDetaillist = new List<SV.Framework.Models.Old.RMA.RMADetail>();
            //List<RMAEsnLookUp> esnLookup = new List<RMAEsnLookUp>();
            SV.Framework.Models.Old.RMA.RMADetail objRMADETAIL = new SV.Framework.Models.Old.RMA.RMADetail();

            SV.Framework.Models.RMA.RMA RmaInfo = rmaUtility.getRMAInfo(rmaGUID, "", "", 0, -1, "");

            if (RmaInfo != null)
            {
                hdnShipDate.Value = RmaInfo.MaxShipmentDate.ToShortDateString();
                txtAddress.Text = RmaInfo.Address;
                txtCity.Text = RmaInfo.City;
                txtCustName.Text = RmaInfo.RmaContactName;
                //txtState.Text = RmaInfo.State;
                dpState.SelectedValue = RmaInfo.State;
                txtZip.Text = RmaInfo.Zip;
                txtRmaNum.Text = RmaInfo.RmaNumber;
                txtRMACustomerNumber.Text = RmaInfo.CustomerRMANumber;
                txtRMACustomerNumber.Visible = false;
                lblRMACustNo.Visible = false;
                txtRMADate.Text = RmaInfo.RmaDate.ToShortDateString();
                //txtRemarks.Text = RmaInfo.Comment;
                ddlStatus.SelectedValue = RmaInfo.RmaStatusID.ToString();
                //txtAVComments.Text = RmaInfo.AVComments;
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                txtPhone.Text = RmaInfo.Phone;
                txtEmail.Text = RmaInfo.Email;
                //txtLocationCode.Text = RmaInfo.LocationCode;

                //chkShipLabel.Checked = RmaInfo.AllowShippingLabel;

                //if (!string.IsNullOrWhiteSpace(RmaInfo.DocComment))
                //    lblDocError.Text = "Error: " + RmaInfo.DocComment;

                if (!string.IsNullOrEmpty(RmaInfo.StoreID))
                   ddlStoreID.SelectedValue = RmaInfo.StoreID;
                //txtCustEmail.Text = RmaInfo.CustomerEmail;
                hdncompanyname.Value = RmaInfo.RMAUserCompany.CompanyName;
                //if (RmaInfo.RmaStatusID > 1 && userID == 0)
                //    txtLocationCode.Enabled = true;
                //else
                //    txtLocationCode.Enabled = false;
    // Section commented out by LLEUNG
    //            if (userID > 0)
    //            {
    //                if (RmaInfo.RmaStatusID > 1)
    //                {
    //                    btnSubmitRMA.Enabled = false;
    //                }
    //            }
            }

            objRMADETAIL.rmaGUID = rmaGUID;
            objRMADETAIL.rmaDetGUID = 0;
            objRMADETAIL.ESN = string.Empty;
            objRMADETAIL.Reason = 0;
            objRMADETAIL.StatusID = 1;
            objRMADETAIL.CallTime = 0;
            objRMADETAIL.Notes = string.Empty;
            objRMADETAIL.ItemCode = string.Empty;
            objRMADETAIL.UPC = string.Empty;
            objRMADETAIL.Status = string.Empty;
            RmaInfo.RmaDetails.Add(objRMADETAIL);

            Session["rmadetailslist"] = RmaInfo.RmaDetails;// esnLookup;

            DlRma.DataSource = RmaInfo.RmaDetails;
            DlRma.DataBind();
            Session["rmaDocList"] = RmaInfo.RmaDocumentList;// esnLookup;
            if (RmaInfo.RmaDocumentList!= null && RmaInfo.RmaDocumentList.Count > 0)
            {
                for (int i = 0; i < RmaInfo.RmaDocumentList.Count; i++)
                {
                    //if (userID == 0 && RmaInfo.RmaDocumentList[i].DocType.ToUpper() == "RL")
                    //{
                    //    lblPrint.Visible = true;
                    //    chkPrint.Visible = true;

                    //}
                    if (lblRmaDocs.Text == string.Empty)
                        lblRmaDocs.Text = RmaInfo.RmaDocumentList[i].RmaDocument;
                    else
                        lblRmaDocs.Text = lblRmaDocs.Text + ", " + RmaInfo.RmaDocumentList[i].RmaDocument;
                }
            }
            

            hdnRmaItemcount.Value = DlRma.Items.Count.ToString();

            if (userID > 0)
            {
                DropDownList ddl_Status1 = (DropDownList)DlRma.Items[DlRma.Items.Count - 1].FindControl("ddl_Status");
                ddl_Status1.Visible = false;
                //hdnStatus.Value = "0";
            }
        }


        //To autogenerate new RMA Number
        private void generateRMA()
        {
            //string rmaGUID = string.Empty;
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


                txtRmaNum.Text = st + dt.Year.ToString() + month + day; //+ "-" + rmaGUID;
            }
            catch (Exception ex)
            {
                lbl_msg.Text = ex.Message.ToString();
            }

        }
        //Bind Company DropdownList
        private void bindCompany()
        {
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            try
            {
                ddlCompany.DataSource = avii.Classes.clsCompany.GetCompany(0,0);
                ddlCompany.DataValueField = "companyID";
                ddlCompany.DataTextField = "companyName";
                ddlCompany.DataBind();
                //ListItem item = new ListItem("", "0");
                //ddlCompany.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                lblConfirm.Text = ex.Message.ToString();
            }
        }
        private void Dl_esn_bind(string po_num)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int userID = 0;
            int companyID = 0;
            int dlindex = 0;
            int rmaGUID = 0;

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            //
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            //RMAEsnLookUp objRMAesnblank = new RMAEsnLookUp();
            SV.Framework.Models.Old.RMA.RMADetail objRMAesnblank = new SV.Framework.Models.Old.RMA.RMADetail();
            lblConfirm.Text = string.Empty;
            try
            {
                List<SV.Framework.Models.Old.RMA.RMADetail> esnList = rmaUtility.getRMAesn(companyID, "", po_num, 0, rmaGUID, userID);
                //List<SV.Framework.Models.Old.RMA.RMADetail> esnList = new List<RMADetail>();
                if (esnList != null && esnList.Count > 0)
                {
                    if (esnList[0].PoStatusId != 3 && esnList[0].PoStatusId != 0)
                    {
                        lblConfirm.Text = "PO not shipped yet!";
                    }
                    if (po_num != "" && esnList.Count > 0)
                    {
                        if (esnList.Count == 1 && (esnList[0].UPC == "Invalid Esn" || esnList[0].UPC == "Not Found"))
                        {
                            lblConfirm.Text = "No Records Found";
                        }
                        else
                        {
                            objRMAesnblank.ESN = "";
                            objRMAesnblank.UPC = "";
                            // objRMAesnblank.maker = "";
                            objRMAesnblank.rmaDetGUID = 0;
                            esnList.Add(objRMAesnblank);
                        }

                    }
                    if (lblConfirm.Text != "PO not shipped yet!")
                    {
                        Session["rmadetailslist"] = esnList;
                        DlRma.DataSource = esnList;
                        DlRma.DataBind();
                        if (esnList.Count > 1)
                            esnPanel.Visible = true;

                    }
                    else
                    {

                    }

                    hdnRmaItemcount.Value = DlRma.Items.Count.ToString();
                    dlindex = DlRma.Items.Count;
                    if (rmaGUID == 1)
                    {
                        if (dlindex > 0)
                        {
                            DropDownList ddl_Status1 = (DropDownList)DlRma.Items[dlindex - 1].FindControl("ddl_Status");
                            ddl_Status1.Visible = false;
                        }
                    }
                    if (po_num != "")
                    {
                        for (int i = 0; i < dlindex - 1; i++)
                        {
                            CheckBox chkESN2 = (CheckBox)DlRma.Items[i].FindControl("chkESN");
                            DropDownList ddl_Status = (DropDownList)DlRma.Items[i].FindControl("ddl_Status");

                            if (userID > 0)
                            {
                                if (Convert.ToInt32(ddl_Status.SelectedValue) > 1)
                                {
                                    chkESN2.Checked = true;
                                    chkESN2.Enabled = false;
                                }
                                else
                                {
                                    chkESN2.Enabled = true;
                                    chkESN2.Checked = true;
                                }
                                ddl_Status.Visible = false;
                            }
                            else
                            {
                                chkESN2.Enabled = true;
                                chkESN2.Checked = true;
                            }

                        }
                        //if (userID > 0)
                        {

                            if (dlindex > 0)
                            {
                                DropDownList ddl_Status1 = (DropDownList)DlRma.Items[dlindex - 1].FindControl("ddl_Status");
                                ddl_Status1.Visible = false;
                            }
                        }
                        
                            

                    }
                }
                else
                {
                    lblConfirm.Text = "Purchase Order# does not exists";
                }
            }
            catch (Exception ex)
            {
                lblConfirm.Text = ex.Message.ToString();
            }

            if (lblConfirm.Text != string.Empty)
            {
                btnValidate.Enabled = false;
                //btnSubmitRMA.Enabled = false;
            }
            else
            {
                if (dlindex > 1)
                {
                    //btnValidate.Enabled = true;
                    btnSubmitRMA.Enabled = true;
                }
            }

        }

        protected void btnCancelRMA_click(object sender, EventArgs e)
        {
            reset();
            generateRMA();
            //if (Request["mode"] == "po")
            //btnSubmitRMA.Enabled = false;
            //else
            //    btnSubmitRMA.Enabled = true;
            lblConfirm.Text = string.Empty;

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

        private bool validate_Company_Esn(string esn, int poFlag)
        {
            bool errorReturn = false;
            int companyID = 0;
            int userID = 0;
            int rmaGUID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            string esnval;
            string maxesnmessage = "Esn Should be between 8 to 30 digits!";
            //company validation
            if (companyID == 0)
            {
                if (userID == 0)
                {
                    if (Request["mode"] == "esn" || Request["mode"] == "po")
                    {
                        if (ddlCompany.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Company not selected!')</script>", false);
                            lbl_msg.Text = "Company not Selected!";
                            errorReturn = true;
                            return errorReturn;
                        }

                        companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
                        ViewState["companyID"] = companyID;
                    }
                }

            }


            esnval = esn.Trim();

            //esn length check
            if (esnval.Length < 8 || esnval.Length > 30)
            {
                hdnmsg.Value = maxesnmessage;
                esn = "";
                errorReturn = true;
                lbl_msg.Text = maxesnmessage;
                //if (Request["mode"] == "esn" || rmaGUID > 0 || Request["mode"] == "po" || hdncompanyname.Value == company)
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(" + poFlag + ")</script>", false);

            }

            return errorReturn;
        }

        private List<SV.Framework.Models.Old.RMA.RMADetail> fill_esnList(SV.Framework.Models.Old.RMA.RMADetail rmaEsnLookup, int selectedItemIndex, SV.Framework.Models.Old.RMA.RMADetail rmaEsnPreviousRow)
        {
            //SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            // List<RMAEsnLookUp> rmaEsnLookups = Session["rmadetailslist"] as List<RMAEsnLookUp>;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookups = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookups == null)
            {
                rmaEsnLookups = new List<SV.Framework.Models.Old.RMA.RMADetail>();
            }

            if (rmaEsnLookups.Count == selectedItemIndex)
            {
                rmaEsnLookups.Add(rmaEsnLookup);
                
            }
            else
            {
                rmaEsnLookups[selectedItemIndex] = rmaEsnLookup;
                if (selectedItemIndex > 0 && (selectedItemIndex+1) == rmaEsnLookups.Count)
                    rmaEsnLookups[selectedItemIndex - 1] = rmaEsnPreviousRow;
            }

            return rmaEsnLookups;
        }

        private void fill_esnList(ref List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup, bool isValidate, int rmaGUID)
        {
            int triagStatusID;
            
            int statusID = 1;
            string status = "Pending";
            int companyID = 0;
            string esn = string.Empty;
            ViewState["duplicateESN"] = null;
            ViewState["ESNalreadyExists"] = null;
            ViewState["ExternalESN"] = null; 
            ViewState["ESNexpiredwarranty"] = null;
            ViewState["warranty"] = null;
            ViewState["disposition"] = null;
            ViewState["allowRMA"] = null;

            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            //if (ViewState["rmaGUID"] != null)
            //    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

            

            Hashtable hshEsnDuplicateCheck = new Hashtable();
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {

                foreach (SV.Framework.Models.Old.RMA.RMADetail redt in rmaEsnLookup)
                {
                    if (ViewState["validate"] == null)
                    {
                        if (hshEsnDuplicateCheck.ContainsKey(redt.ESN))
                        {

                            lblConfirm.Text = string.Format("Duplicate ESN ({0}) is found", redt.ESN);
                            ViewState["duplicateESN"] = "1";

                            break;
                        }
                        else
                        {
                            hshEsnDuplicateCheck.Add(redt.ESN, redt.ESN);
                        }

                        string upc, avso, Po_Num;
                        ValidateESN(redt.ESN, companyID, out upc, out avso, out Po_Num, rmaGUID);
                        redt.UPC = upc;
                        redt.AVSalesOrderNumber = avso;
                        redt.PurchaseOrderNumber = Po_Num;
                        if (chkAll.Checked)
                        {
                            redt.StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                        }

                    }
                }
            }
            else
            {
                if (rmaEsnLookup == null)
                    rmaEsnLookup = new List<SV.Framework.Models.Old.RMA.RMADetail>();

                SV.Framework.Models.Old.RMA.RMADetail objRMAesn = null;
                foreach (DataListItem i in DlRma.Items)
                {
                    TextBox txtEsn = (TextBox)i.FindControl("txt_ESN");
                    if (txtEsn.Text.Trim().Length > 0)
                    {
                        CheckBox chkESN1 = (CheckBox)i.FindControl("chkESN");
                        if (chkESN1.Checked)
                        {
                            esn = string.Empty;
                            if (chkAll.Checked)
                            {
                                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
                                status = ddlStatus.SelectedItem.Text;
                            }
                            else
                            {
                                DropDownList ddl_Status = (DropDownList)i.FindControl("ddl_Status");
                                if (ddl_Status.SelectedIndex > 0)
                                {
                                    statusID = Convert.ToInt32(ddl_Status.SelectedValue);
                                    status = ddl_Status.SelectedItem.Text;
                                }
                            }

                            //HiddenField hdnrmaDetGuid = (HiddenField)i.FindControl("hdnRDOID");


                            esn = txtEsn.Text.Trim();
                            if (string.IsNullOrEmpty(esn))
                            {
                                lblConfirm.Text = "ESN can not be empty";
                                break;
                            }

                            if (hshEsnDuplicateCheck.ContainsKey(esn))
                            {
                                lblConfirm.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                                ViewState["duplicateESN"] = "1";
                            }
                            else
                            {
                                hshEsnDuplicateCheck.Add(esn, esn);


                                //TextBox txtCallTime = (TextBox)i.FindControl("txtCTime");
                                Label txtUPC = (Label)i.FindControl("lblUPC");
                                //Label lblAvso = (Label)i.FindControl("lblAvso");
                                TextBox txtNotes = (TextBox)i.FindControl("txtNotes");
                                DropDownList ddReason = (DropDownList)i.FindControl("ddReason");
                                DropDownList dpWarranty = (DropDownList)i.FindControl("dpWarranty");
                                DropDownList dpDisposition = (DropDownList)i.FindControl("dpDisposition");
                                HiddenField hdnRDOID = (HiddenField)i.FindControl("hdnRDOID");

                                HiddenField hdTriagNotes = (HiddenField)i.FindControl("hdTriagNotes");
                                HiddenField hdTriagStatusID = (HiddenField)i.FindControl("hdTriagID");
                                HiddenField hdnNewSKU = (HiddenField)i.FindControl("hdnNewSKU");
                                HiddenField hdnTracking = (HiddenField)i.FindControl("hdnTracking");



                                triagStatusID = 0;
                                if (dpWarranty.SelectedIndex == 0)
                                {
                                    ViewState["warranty"] = "1";
                                    lblConfirm.Text = "Type is required";

                                }
                                //if (rmaGUID > 0)
                                //{
                                //    if (dpDisposition.SelectedIndex == 0)
                                //    {
                                //        ViewState["disposition"] = "1";
                                //        if (lblConfirm.Text != string.Empty)
                                //        {
                                //            lblConfirm.Text = " Type is required <br /> Disposition is required";
                                //        }
                                //        else
                                //            lblConfirm.Text = "Disposition is required";

                                //    }
                                //}
                                //if (txtUPC.Text.Trim() == "RMA Exists")
                                //{
                                //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                //    ViewState["ESNalreadyExists"] = "1";
                                //}
                                if (esn.Trim() != string.Empty)
                                {
                                    //if (txtCallTime.Text == "")
                                    //    txtCallTime.Text = "0";

                                    objRMAesn = new SV.Framework.Models.Old.RMA.RMADetail();
                                    objRMAesn.ESN = esn;
                                    //if (ViewState["validate"] == null)
                                    //{
                                    ValidateESN(ref objRMAesn, companyID, rmaGUID);

                                    if (!objRMAesn.AllowDuplicate)
                                    {
                                        lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                        ViewState["ESNalreadyExists"] = "1";
                                    }
                                    string rmaEsns = string.Empty;
                                    if (!objRMAesn.AllowRMA)
                                    {
                                    //    if (ViewState["allowRMA"] == null)
                                    //        ViewState["allowRMA"] = esn;
                                    //    else
                                    //    {
                                    //        rmaEsns = ViewState["allowRMA"].ToString();

                                    //        ViewState["allowRMA"] = rmaEsns + ", " + esn;
                                    //    }
                                    //    if (rmaEsns == string.Empty)
                                    //        lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
                                    //    else
                                    //        lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to these ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
                                    }
                                    

                                    if ((objRMAesn.WarrantyExpirationDate - DateTime.Now).Days < 0 && dpWarranty.SelectedIndex == 1 && txtNotes.Text.Trim() == string.Empty && dpDisposition.SelectedIndex > 0)
                                    {
                                        string esns = string.Empty;
                                        if (ViewState["ESNexpiredwarranty"] == null)
                                            ViewState["ESNexpiredwarranty"] = esn;
                                        else
                                        {
                                            esns = ViewState["ESNexpiredwarranty"].ToString();

                                            ViewState["ESNexpiredwarranty"] = esns + ", " + esn;
                                        }
                                        if (isValidate)
                                        {
                                            if (esns == string.Empty)
                                                lblConfirm.Text = string.Format("This ESN/MEID({0}) has an expired warranty.  Please provide additional information on the NOTES field", esn);
                                            else
                                                lblConfirm.Text = string.Format("These ESN/MEID({0}) has an expired warranty.  Please provide additional information on the NOTES field", ViewState["ESNexpiredwarranty"].ToString());
                                            
                                        }
                                        else
                                        {
                                            if (txtNotes.Text.Trim() == string.Empty)
                                            {
                                                
                                                if (esns == string.Empty)
                                                    lblConfirm.Text = string.Format("This ESN/MEID({0}) has an expired warranty.  Please provide additional information on the NOTES field", esn);
                                                else
                                                    lblConfirm.Text = string.Format("These ESN/MEID({0}) has an expired warranty.  Please provide additional information on the NOTES field", ViewState["ESNexpiredwarranty"].ToString());

                                                ViewState["ESNexpiredwarranty1"] = "1";
                                            }
                                        }
                                    }

                                    if (objRMAesn.ItemCode == "Not Found" && dpWarranty.SelectedIndex > 0 && dpDisposition.SelectedIndex > 0)
                                    {
                                        string esns = string.Empty;
                                        if (ViewState["ExternalESN"] == null)
                                            ViewState["ExternalESN"] = esn;
                                        else
                                        {
                                            esns = ViewState["ExternalESN"].ToString();

                                            ViewState["ExternalESN"] = esns + ", " + esn;
                                        }
                                        if (isValidate)
                                        {
                                            if (esns == string.Empty)
                                                lblConfirm.Text = string.Format("This ESN/MEID({0}) cannot be found in our system.  You can still request an RMA for this item, but you will have to provide the Invoice Number and Date on the NOTES field", esn);
                                            else
                                                lblConfirm.Text = string.Format("These ESN/MEID({0}) cannot be found in our system.  You can still request an RMA for this item, but you will have to provide the Invoice Number and Date on the NOTES field", esn);
                                            
                                        }
                                        else
                                        {
                                            if (txtNotes.Text.Trim() == string.Empty)
                                            {

                                                ViewState["ExternalESN1"] = "1";
                                                if (esns == string.Empty)
                                                    lblConfirm.Text = string.Format("This ESN/MEID({0}) cannot be found in our system.  You can still request an RMA for this item, but you will have to provide the Invoice Number and Date on the NOTES field", esn);
                                                else
                                                    lblConfirm.Text = string.Format("These ESN/MEID({0}) cannot be found in our system.  You can still request an RMA for this item, but you will have to provide the Invoice Number and Date on the NOTES field", ViewState["ExternalESN"].ToString());
                                            
                                                
                                            }
                                        }
                                    }

                                    txtUPC.Text = objRMAesn.UPC;
                                    // lblAvso.Text = objRMAesn.AVSalesOrderNumber;
                                    //}
                                    objRMAesn.rmaDetGUID = Convert.ToInt32(hdnRDOID.Value);
                                    objRMAesn.CallTime = 0; // Convert.ToInt32(txtCallTime.Text);
                                    objRMAesn.Notes = txtNotes.Text;
                                    objRMAesn.Reason = Convert.ToInt32(ddReason.SelectedValue);
                                    //objRMAesn.Reason = ddReason.SelectedValue;
                                    objRMAesn.StatusID = statusID;
                                    objRMAesn.Status = status;
                                    //objRMAesn.rmaDetGUID = 0;
                                    objRMAesn.Warranty = Convert.ToInt32(dpWarranty.SelectedValue);
                                    objRMAesn.Disposition = Convert.ToInt32(dpDisposition.SelectedValue);

                                    int.TryParse(hdTriagStatusID.Value, out triagStatusID);

                                    objRMAesn.TriageStatusID = triagStatusID;
                                    objRMAesn.TriageNotes = hdTriagNotes.Value;
                                    objRMAesn.NewSKU = hdnNewSKU.Value;
                                    objRMAesn.ShippingTrackingNumber = hdnTracking.Value;


                                    rmaEsnLookup.Add(objRMAesn);
                                    // Disable controls

                                }
                            }
                        }
                    }

                }
            }

            if (isValidate)
            {
                SV.Framework.Models.Old.RMA.RMADetail objRMAesnblank = new SV.Framework.Models.Old.RMA.RMADetail();
                objRMAesnblank.ESN = "";
                objRMAesnblank.UPC = "";
                objRMAesnblank.StatusID = 1;
                objRMAesnblank.rmaDetGUID = 0;
                rmaEsnLookup.Add(objRMAesnblank);
                ViewState["rmadetailslist"] = rmaEsnLookup;
            }
            //SV.Framework.Models.Old.RMA.RMADetail objRMAesnblank = new SV.Framework.Models.Old.RMA.RMADetail();
            //objRMAesnblank.ESN = "";
            //objRMAesnblank.UPC = "";
            //objRMAesnblank.rmaDetGUID = 0;
            //rmaEsnLookup.Add(objRMAesnblank);

            DlRma.DataSource = rmaEsnLookup;
            DlRma.DataBind();
        }

        private bool check_DuplicateEsn(string txtEsn, int itemindex, int poFlag)
        {
            bool returnError = false;
            string esnmessage = "ESN already added to the RMA!";
            List<SV.Framework.Models.Old.RMA.RMADetail> esnList = (List<SV.Framework.Models.Old.RMA.RMADetail>)Session["rmadetailslist"];
            if (esnList != null)
            {
                foreach (SV.Framework.Models.Old.RMA.RMADetail rmaesn in esnList)
                {
                    if (!string.IsNullOrEmpty(rmaesn.ESN) && rmaesn.ESN == txtEsn)
                    {
                        hdnmsg.Value = esnmessage;
                        txtEsn = "";
                        lbl_msg.Text = esnmessage;
                        returnError = true;
                        break;

                    }
                }
            }

            return returnError;
        }

        protected int add_BlankESN(string lblItemCode, int itemindex, DataListItem item, List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup)
        {
            if (lblItemCode == "Invalid Esn" || hdnmsg.Value == "ESN already added to the RMA")
            {
                itemindex = item.ItemIndex;
            }
            else if (string.IsNullOrEmpty(rmaEsnLookup[rmaEsnLookup.Count - 1].ESN))
                itemindex = item.ItemIndex;
            else
            {
                itemindex = item.ItemIndex + 1;

                SV.Framework.Models.Old.RMA.RMADetail objRMAesnblank = new SV.Framework.Models.Old.RMA.RMADetail();
                objRMAesnblank.ESN = "";
                objRMAesnblank.UPC = "";
                // objRMAesnblank.maker = "";
                objRMAesnblank.rmaDetGUID = 0;
                rmaEsnLookup.Add(objRMAesnblank);
            }

            return itemindex;
        }
        protected void check_Maxesn(int itemindex, List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup, string lblItemCode)
        {
            int userID = 0;
            int maxEsn = 10;
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);
            if (ViewState["maxEsn"] != null)
                maxEsn = Convert.ToInt32(ViewState["maxEsn"]);
            if (itemindex <= maxEsn)
            {
                DlRma.DataSource = rmaEsnLookup;
                DlRma.DataBind();
                hdnRmaItemcount.Value = DlRma.Items.Count.ToString();

                if (DlRma.Items.Count == itemindex)
                {

                    TextBox txtEsn13 = (TextBox)DlRma.Items[itemindex - 1].FindControl("txt_ESN");

                    txtEsn13.Focus();
                }
                if (userID > 0)
                {
                    DropDownList ddl_Status1 = (DropDownList)DlRma.Items[DlRma.Items.Count - 1].FindControl("ddl_Status");
                    ddl_Status1.Visible = false;
                    //hdnStatus.Value = "0";
                }

            }

            else
            {
                lblConfirm.Text = "Can't add more ESN";
                lblConfirm.Visible = true;
            }


        }

        protected void CallTime_TextChanged(object sender, EventArgs e)
        {
            int calltime = 0;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                TextBox txtCallTime = (TextBox)sender;
                if (txtCallTime.Text.Trim().Length > 0 && int.TryParse(txtCallTime.Text.Trim(), out calltime))
                {
                    DataListItem item = (DataListItem)txtCallTime.NamingContainer;
                    int itemindex = item.ItemIndex;
                    if (rmaEsnLookup.Count >= itemindex)
                    {
                        rmaEsnLookup[itemindex].CallTime = calltime;
                    }
                }

            }
        }

        protected void Notes_TextChanged(object sender, EventArgs e)
        {
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                TextBox txtNotes = (TextBox)sender;
                if (txtNotes.Text.Trim().Length > 0)
                {
                    DataListItem item = (DataListItem)txtNotes.NamingContainer;
                    int itemindex = item.ItemIndex;
                    if (rmaEsnLookup.Count >= itemindex)
                    {
                        rmaEsnLookup[itemindex].Notes = txtNotes.Text.Trim();
                    }
                }
            }
        }

        protected void Status_OnChanged(object sender, EventArgs e)
        {
            //List<SV.Framework.Models.RMA.RMAEsnLookUp> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.RMA.RMAEsnLookUp>;
            //if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            //{
            //    DropDownList dpStatus = (DropDownList)sender;
            //    DataListItem item = (DataListItem)dpStatus.NamingContainer;
            //    int itemindex = item.ItemIndex;
            //    if (rmaEsnLookup.Count > itemindex)
            //    {
            //        rmaEsnLookup[itemindex].StatusID = Convert.ToInt32(dpStatus.SelectedValue);
            //    }
            //}
        }
        //

        protected void Reason_OnChanged(object sender, EventArgs e)
        {
            //List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            //if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            //{
            //    DropDownList dpReason = (DropDownList)sender;
            //    DataListItem item = (DataListItem)dpReason.NamingContainer;
            //    int itemindex = item.ItemIndex;
            //    if (rmaEsnLookup.Count >= itemindex)
            //    {
            //        rmaEsnLookup[itemindex].Reason = dpReason.SelectedValue;
            //    }
            //}
        }

        protected void ESN_TextChanged(object sender, EventArgs e)
        {
            int poFlag = 0;
            int companyID = 0;
            
            TextBox textEsn = (TextBox)sender;
            int rmaguid = 0;
            SV.Framework.Models.Old.RMA.RMADetail rmaEsn = new SV.Framework.Models.Old.RMA.RMADetail();
            SV.Framework.Models.Old.RMA.RMADetail rmaEsnPreviousRow = new SV.Framework.Models.Old.RMA.RMADetail();
            DataListItem item = (DataListItem)textEsn.NamingContainer;
            int itemindex = item.ItemIndex;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = new List<SV.Framework.Models.Old.RMA.RMADetail>();
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            if (Request["mode"] == "po")
            {
                poFlag = 1;
            }
            int currentIndex = itemindex - 1;
            //if (int.TryParse(((HiddenField)item.FindControl("hdnRDOID")).Value, out rmaguid))
            rmaEsn.rmaDetGUID = rmaguid;
            rmaEsn.ESN = ((TextBox)item.FindControl("txt_ESN")).Text;
            if (itemindex > 0 || (itemindex + 1) < DlRma.Items.Count)
            {

                if ((itemindex + 1) < DlRma.Items.Count)
                    currentIndex = itemindex;
                DropDownList dpStatus = (DropDownList)DlRma.Items[currentIndex].FindControl("ddl_Status");
                //TextBox txtCTime = (TextBox)DlRma.Items[currentIndex].FindControl("txtCTime");
                //if (txtCTime.Text.Trim() == string.Empty)
                //    txtCTime.Text = "0";
                TextBox txtNotes1 = (TextBox)DlRma.Items[currentIndex].FindControl("txtNotes");
                DropDownList ddReason1 = (DropDownList)DlRma.Items[currentIndex].FindControl("ddReason");
                DropDownList dpWarranty1 = (DropDownList)DlRma.Items[currentIndex].FindControl("dpWarranty");
                DropDownList dpDisposition1 = (DropDownList)DlRma.Items[currentIndex].FindControl("dpDisposition");
                Label lblCode = (Label)DlRma.Items[currentIndex].FindControl("lblCode");
                Label lblPONum = (Label)DlRma.Items[currentIndex].FindControl("lblPONum");

                if ((itemindex + 1) < DlRma.Items.Count)
                {
                    rmaEsn.ItemCode = lblCode.Text;
                    rmaEsn.PurchaseOrderNumber = lblPONum.Text;
                    rmaEsn.CallTime = 0; // Convert.ToInt32(txtCTime.Text);
                    rmaEsn.Disposition = Convert.ToInt32(dpDisposition1.SelectedValue);
                    rmaEsn.Notes = txtNotes1.Text;
                    rmaEsn.Reason = Convert.ToInt32(ddReason1.SelectedValue);
                    //rmaEsn.Reason = ddReason1.SelectedValue;
                    rmaEsn.Warranty = Convert.ToInt32(dpWarranty1.SelectedValue);
                    rmaEsn.StatusID = Convert.ToInt32(dpStatus.SelectedValue);
                }
                else
                {
                    rmaEsnPreviousRow.ESN = ((TextBox)DlRma.Items[itemindex - 1].FindControl("txt_ESN")).Text;
                    rmaEsnPreviousRow.ItemCode = lblCode.Text;
                    rmaEsn.PurchaseOrderNumber = lblPONum.Text;
                    rmaEsnPreviousRow.CallTime = 0; // Convert.ToInt32(txtCTime.Text);
                    rmaEsnPreviousRow.Disposition = Convert.ToInt32(dpDisposition1.SelectedValue);
                    rmaEsnPreviousRow.Notes = txtNotes1.Text;
                    rmaEsnPreviousRow.Reason = Convert.ToInt32(ddReason1.SelectedValue);
                    rmaEsnPreviousRow.Warranty = Convert.ToInt32(dpWarranty1.SelectedValue);
                    rmaEsnPreviousRow.StatusID = Convert.ToInt32(dpStatus.SelectedValue);
                }



            }
            //btnSubmitRMA.Enabled = false;
            hdnmsg.Value = "";
            lblConfirm.Text = string.Empty;
            lbl_msg.Text = string.Empty;
            //company check and esn length check
            bool errorReturn = validate_Company_Esn(rmaEsn.ESN, poFlag);

            if (ViewState["rmaGUID"] != null)
                int.TryParse(ViewState["rmaGUID"].ToString(), out rmaguid);

            // check duplicate esn
            bool checkDuplicate = check_DuplicateEsn(rmaEsn.ESN, itemindex, poFlag);

            if (checkDuplicate)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('" + hdnmsg.Value + "');</script>", false);

            else if (errorReturn == false && checkDuplicate == false)
            {
                ValidateESN(ref rmaEsn, companyID, rmaguid);

                rmaEsnLookup = fill_esnList(rmaEsn, itemindex, rmaEsnPreviousRow);
                // add blank row
                itemindex = add_BlankESN(rmaEsn.UPC, itemindex, item, rmaEsnLookup);

                if (itemindex < rmaEsnLookup.Count && itemindex > 0)
                {
                    //max esn check before bind
                    check_Maxesn(rmaEsnLookup.Count, rmaEsnLookup, rmaEsn.UPC);
                }
            }

            if (Request["mode"] == "esn" || Request["mode"] == "po")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(" + poFlag + ")</script>", false);
            //else
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>disableSubmit('2')</script>", false);

        }

        protected void btnValidate_click(object sender, EventArgs e)
        {
            lblConfirm.Text = string.Empty;
            lbl_msg.Text = string.Empty;
            ViewState["validate"] = null;
            int rmaGUID = 0;

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            int companyID = 0;
            if (ViewState["companyID"] != null)
            {
                companyID = Convert.ToInt32(ViewState["companyID"]);
                //List<RMADetail> rmaEsnList = ViewState["rmadetailslist"] as List<RMADetail>;
                List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnList = new List<SV.Framework.Models.Old.RMA.RMADetail>();
                if (rmaEsnList != null)
                {
                    fill_esnList(ref rmaEsnList, true, rmaGUID);
                    //btnSubmitRMA.Visible = true;
                    btnSubmitRMA.Enabled = true;
                    ViewState["validate"] = "1";
                    hdnValidateESNs.Value = "1";
                }
            }

            if (ViewState["companyID"] == null || (ViewState["companyID"] != null && string.IsNullOrEmpty(Convert.ToInt32(ViewState["companyID"]).ToString()))
                    || (ddlCompany.Visible == true && ddlCompany.SelectedIndex == 0))
            {
                ViewState["validate"] = null;
                lbl_msg.Text = "Company is not selected";
                //btnSubmitRMA.Enabled = false;
            }

        }

        private void ValidateESN(ref SV.Framework.Models.Old.RMA.RMADetail avEsn, int companyID, int rmaGUID)
        {
            int userId = 0;
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            if (ViewState["userID"] != null)
            {
                userId = Convert.ToInt32(ViewState["userID"]);
            }

            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {
                List<SV.Framework.Models.Old.RMA.RMADetail> esnlist = rmaUtility.getRMAesn(companyID, avEsn.ESN, "", 0, rmaGUID, userId);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        avEsn.UPC = esnlist[0].UPC;
                        avEsn.ItemCode = esnlist[0].ItemCode;
                        avEsn.rmaDetGUID = esnlist[0].rmaDetGUID;
                        avEsn.AVSalesOrderNumber = esnlist[0].AVSalesOrderNumber;
                        avEsn.PurchaseOrderNumber = esnlist[0].PurchaseOrderNumber;
                        avEsn.AllowDuplicate = esnlist[0].AllowDuplicate;
                        avEsn.WarrantyExpirationDate = esnlist[0].WarrantyExpirationDate;
                    }
                }
            }
        }


        private void ValidateESN(string esn, int companyID, out string UPC, out string AVSO, out string Po_Num, int rmaGUID)
        {
            int userId = 0;
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            if (ViewState["userID"] != null)
            {
                userId = Convert.ToInt32(ViewState["userID"]);
            }

            //esn lookup
            UPC = string.Empty;
            AVSO = string.Empty;
            Po_Num = string.Empty;
            if (!string.IsNullOrEmpty(esn) && companyID > 0)
            {
                List<SV.Framework.Models.Old.RMA.RMADetail> esnlist = rmaUtility.getRMAesn(companyID, esn, "", 0, rmaGUID, userId);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        UPC = esnlist[0].UPC.ToString();
                        AVSO = esnlist[0].AVSalesOrderNumber;
                        Po_Num = esnlist[0].PurchaseOrderNumber;
                    }
                }
            }
        }


        private bool ValidateRequiredColumns(out string returnMessage)
        {
            bool validated = false;
            returnMessage = string.Empty;
            if (txtCity.Text.Trim().Length > 0 && txtCustName.Text.Trim().Length > 0 && txtAddress.Text.Trim().Length > 0
                    && dpState.SelectedIndex > 0 && txtZip.Text.Trim().Length > 0 && txtRMADate.Text.Trim().Length > 0 && txtEmail.Text.Trim().Length > 0 && txtPhone.Text.Trim().Length > 0)
            {
                if (isEmail(txtEmail.Text.Trim()))
                {
                    if (txtEmail.Text.Trim().ToLower().IndexOf("langlobal.com") <= 0)
                    {
                        validated = true;
                    }
                    else
                        returnMessage = "Email should not have langlobal.com email address.";
                }

            }
            else
                returnMessage = "Please enter all required columns with correct data";

            return validated;
        }

        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
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

        protected void btnRmaDocUpload_Clicck(object sender, EventArgs e)
        {
            RmaDocumentsList();
            //RegisterStartupScript("jsUnblockDialog", "closeRmaDocDialog();");
            //closeRmaDocDialog()
        }
        private List<RmaDocuments> RmaDocumentsList()
        {
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>();

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
                List<RmaDocuments> rmaDocList2 = new List<RmaDocuments>();
                rmaDocList2 = Session["rmaDocList"] as List<RmaDocuments>;
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
            if (fupRmaDocc.HasFile)
            {
                //custFileName = System.IO.Path.GetFileName(fupRmaDocc.PostedFile.FileName);
                RmaDocuments rmaDocObjc = new RmaDocuments();
                extension = Path.GetExtension(fupRmaDocc.PostedFile.FileName);
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
                        fupRmaDocc.PostedFile.SaveAs(custImagePath);
                        rmaDocObjc.RmaDocument = custFileName;
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
                RmaDocuments rmaDocObjA1 = new RmaDocuments();
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
                        rmaDocObjA1.RmaDocument = fileName1;
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
                RmaDocuments rmaDocObjA2 = new RmaDocuments();

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
                        rmaDocObjA2.RmaDocument = fileName2;
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
                RmaDocuments rmaDocObjA3 = new RmaDocuments();

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
                        rmaDocObjA3.RmaDocument = fileName3;
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
                RmaDocuments rmaDocObjA4 = new RmaDocuments();

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
                        rmaDocObjA4.RmaDocument = fileName4;
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
            lblRmaDocs.Text = files;

            return rmaDocList;

        }
        
        protected void btnSubmitRMA_click(object sender, EventArgs e)
        {
            
            string rmaNumber = string.Empty;
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            string locationCode = string.Empty;
            lblConfirm.Text = string.Empty;
            bool isAdmin = false;
            bool isEmail = false;
            
            int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
            bool isMail = Convert.ToBoolean(ConfigurationManager.AppSettings["RMA_AUTO_EMAIL"]);
            string returnMessage1 = "Please enter all required columns with correct data";
            if (!ValidateRequiredColumns(out returnMessage1))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('"+ returnMessage1 + "')</script>", false);
                return;

            }
           //if (hdnValidateESNs.Value != string.Empty)
            ViewState["validate"] = "1";

            if (ViewState["validate"] == null)
            {
                lblConfirm.Text = "Please validate the ESN records before submit";
                //btnSubmitRMA.Visible = false;
                //btnSubmitRMA.Enabled = false;
                return;
            }
            else
            {
                if (hdnValidateESNs.Value == "2")
                    ViewState["validate"] = null;
                int rmaGUID = 0;
                int companyID = 0;

                string customerEmail = string.Empty;
                string overrideEmail = string.Empty;
                List<avii.Classes. CustomerEmail> custEmail = new List<avii.Classes.CustomerEmail>();
                
                SV.Framework.Models.RMA.RMA rmaInfoObj = new SV.Framework.Models.RMA.RMA();
                SV.Framework.Models.RMA.RMAUserCompany objRMAcompany = new SV.Framework.Models.RMA.RMAUserCompany();
                int rmastatus = 1;
                int userid = 0;
                bool adminStatusChange = false;
                string rma_status = "Pending";
                string dateMsg = "Invalid RMA Date! Can not create RMA before 365 days back.";
                string custName, address, city, state, zip, phone, email;
                DateTime currentDate = DateTime.Now;
                DateTime rmaDate = new DateTime();
                DataTable rmaDT = new DataTable();
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                if (ViewState["companyID"] != null)
                    companyID = Convert.ToInt32(ViewState["companyID"]);
                if (ViewState["userID"] != null)
                    int.TryParse(ViewState["userID"].ToString(), out userid);
                int createdby = userid;
                int modifiedby = userid;
                int maxshipmentdays = 10;
                int.TryParse(ConfigurationSettings.AppSettings["maxshipmentdays"].ToString(), out maxshipmentdays);

                if (ViewState["rmaGUID"] != null)
                {

                    rmastatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (rmastatus == 0)
                        rmastatus = 1;
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                    if (userid == 0)
                    {


//                        if (rmastatus == 2 && userid == 0)
//                        {
//                            if (txtLocationCode.Text.Trim() == string.Empty)
//                            {
//                                lbl_msg.Text = "Location code is required";
//                                return;
//                            }
//                        }


                        rma_status = ddlStatus.SelectedItem.Text;
                        isAdmin = true;

                        if (rma_status != lblStatus.Text && !String.IsNullOrEmpty(rma_status)
                                                            && !String.IsNullOrEmpty(lblStatus.Text))
                        {
                            adminStatusChange = true;
                        }
                    }

                }

                if (userid == 0)
                {
                    //objRMAcompany = RMAUtility.getUserEmail(companyID);
                    //userid = objRMAcompany.UserID;
                    //customerEmail = objRMAcompany.Email;
                    custEmail = avii.Classes. CustomerEmailOperations.GetCustomerEmailList(companyID);
                    var userEmail = (from item in custEmail where item.ModuleGUID.Equals(75) select item).ToList(); //75 MODULEGUID IS FOR RMA
                    if (userEmail != null && userEmail.Count > 0)
                    {
                        isEmail = userEmail[0].IsEmail;
                        userid = userEmail[0].UserID;
                        customerEmail = userEmail[0].Email;
                        overrideEmail = userEmail[0].OverrideEmail;
                        if (string.IsNullOrEmpty(overrideEmail) && string.IsNullOrEmpty(customerEmail))
                            customerEmail = userEmail[0].CompanyEmail;
                        if (ViewState["createdby"] != null)
                        {
                            createdby = Convert.ToInt32(ViewState["createdby"]);
                            modifiedby = Convert.ToInt32(ViewState["createdby"]);
                        }
                        //else
                        //{
                        //    createdby = 0;
                        //    modifiedby = 0;
                        //}
                    }
                }
                else
                {
                    if (Session["adm"] == null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            isEmail = userInfo.IsEmail;

                        }
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
                    try
                    {
                        custName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
                        address = (txtAddress.Text.Trim().Length > 0 ? txtAddress.Text.Trim() : null);
                        city = (txtCity.Text.Trim().Length > 0 ? txtCity.Text.Trim() : null);
                        //state = (txtState.Text.Trim().Length > 0 ? txtState.Text.Trim() : null);
                        state = (dpState.SelectedIndex > 0 ? dpState.SelectedValue : null);
                        zip = (txtZip.Text.Trim().Length > 0 ? txtZip.Text.Trim() : null);
                        phone = (txtPhone.Text.Trim().Length > 0 ? txtPhone.Text.Trim() : null);
                        email = (txtEmail.Text.Trim().Length > 0 ? txtEmail.Text.Trim() : null);
                        //locationCode = (txtLocationCode.Text.Trim().Length > 0 ? txtLocationCode.Text.Trim() : null);
                        lblStatus.Text = (ddlStatus.SelectedIndex == 0 ? "Pending" : ddlStatus.SelectedItem.Text);
                        rmaInfoObj = RmaInfo(rmaGUID, rmastatus, modifiedby, createdby, userid, custName, address, city, state, zip, email, phone);

                        if (ViewState["duplicateESN"] != null && ViewState["duplicateESN"] == "1")
                        {
                            lbl_msg.Text = "Duplicate ESN(s) are found in the RMA Form";
                        }
                        else if (ViewState["ESNalreadyExists"] != null && ViewState["ESNalreadyExists"] == "1")
                        {
                            //lbl_msg.Text = ", RMA already exists";
                        }
                        else if (ViewState["ESNexpiredwarranty1"] != null && ViewState["ESNexpiredwarranty1"] == "1")
                        { 
                        
                        }
                        else if (ViewState["ExternalESN1"] != null && ViewState["ExternalESN1"] == "1")
                        {

                        }
                        else if (ViewState["allowRMA"] != null)
                        {
                           
                        }
                        else if (ViewState["warranty"] != null && ViewState["warranty"] == "1")
                        {

                        }
                        else if (ViewState["disposition"] != null && ViewState["disposition"] == "1")
                        {

                        }

                            
                        else if (rmaInfoObj.RmaDetails.Count > maxEsn)
                        {
                            lbl_msg.Text = string.Format("Maximum of {0} ESNs are allowed per RMA request", maxEsn);
                        }

                        else if (rmaInfoObj != null && rmaInfoObj.RmaDetails != null && (rmaInfoObj.RmaDetails.Count > 0 && rmaInfoObj.RmaDetails.Count <= maxEsn))
                        {
                            bool docPrint = false;// chkPrint.Checked;
                            rmaInfoObj.AllowShippingLabel = false; // chkShipLabel.Checked;
                            rmaInfoObj.MaxShipmentDate = DateTime.Now.AddDays(maxshipmentdays);
                            if (rmaGUID > 0)
                                rmaInfoObj.MaxShipmentDate = Convert.ToDateTime(hdnShipDate.Value);

                            generateRMA();
                            rmaInfoObj.RmaNumber = txtRmaNum.Text;
                            rmaInfoObj.CustomerRMANumber = txtRMACustomerNumber.Text.Trim();
                            string RMASource = "RMAExternal";
                            rmaInfoObj.RMASource = RMASource;

                            rmaDT = rmaUtility.update_RMA(rmaInfoObj, docPrint);

                            
                            if (rmaDT.Rows.Count > 0)
                            {
                               string CustomerRMANumberExists =  rmaDT.Rows[0]["CustomerRMANumberExists"] as string;
                                if(!string.IsNullOrEmpty(CustomerRMANumberExists))
                                {
                                    lbl_msg.Text = rmaInfoObj.CustomerRMANumber + " Customer RMA Number already exists!";
                                    return;
                                }
                                Session["rmaDocList"] = null;
                                rmaNumber = rmaDT.Rows[0]["rmanumber"] as string;
                                txtRmaNum.Text = rmaNumber;
                                string totalESN = string.Empty;
                                //string esnInfo = string.Empty;

                                //string productCodeInfo = string.Empty;
                                string reasonInfo = string.Empty;
                                //string statusInfo = string.Empty;

                                StringBuilder sb = new StringBuilder();
                                //sb.Append("<br/> <table><tr>");
                                //sb.Append("<td >ESN</td>");

                                foreach (SV.Framework.Models.Old.RMA.RMADetail rmaESN in rmaInfoObj.RmaDetails)
                                {
                                    //esnInfo = string.Empty;
                                    //esnInfo = string.Format("ESN:<b> {0}</b>, Product Code: <b>{1}</b>, Status: <b>{2}</b>, Reason: <b>{3}</b>", rmaESN.ESN, rmaESN.ItemCode, rmaESN.Status, ((RMAReason)rmaESN.Reason)).ToString();
                                    ///
                                    reasonInfo=((RMAReason)Convert.ToInt32(rmaESN.Reason)).ToString();
                                    sb.Append("<tr>");
                                    sb.Append("<td>");
                                    sb.Append(rmaESN.ESN);
                                    sb.Append("</td>");
                                    sb.Append("<td>");
                                    sb.Append(rmaESN.ItemCode);
                                    sb.Append("</td>");
                                    sb.Append("<td>");
                                    sb.Append(reasonInfo);
                                    sb.Append("</td>");
                                    sb.Append("<td>");
                                    sb.Append(rmaESN.Status);
                                    sb.Append("</td>");

                                    sb.Append("</tr>");

                                    //if (string.IsNullOrEmpty(totalESN))
                                    //    totalESN = rmaESN.ESN;
                                    //else
                                    //    totalESN = totalESN + ", " + rmaESN.ESN;
                                }
                                //sb.Append("</tr></table>");

                                totalESN = sb.ToString();

                                if (isMail && isEmail)
                                {
                                    if (rmaGUID == 0 || adminStatusChange)
                                    {
                                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                                        if (userInfo != null)
                                        {
                                            try
                                            {
                                                //string emailAddr = "rma@wsaPortal.com";
                                                string emailTo = string.Empty;
                                                string emailCC = string.Empty;
                                                //if (!string.IsNullOrEmpty(userInfo.Email) && userInfo.UserType != "Internal User")
                                                //{
                                                //    emailCC = userInfo.Email; // +"," + txtEmail.Text.Trim();
                                                //}
                                                //else if (!string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Internal User")
                                                //{
                                                //    emailCC = customerEmail; // +"," + txtEmail.Text.Trim();
                                                //}
                                                string emailAddress = txtEmail.Text.Trim();

                                                emailTo = avii.Classes.clsGeneral.CustomerEmailAddress(userInfo, emailAddress, customerEmail, overrideEmail);

                                                //string[] array = emailAddr.Split(',');
                                                //if (array.Length > 0)
                                                //    emailTo = array[0];
                                                //if (array.Length > 1)
                                                //    emailCC = array[1];
                                                //string emailAddr = "rma@wsadistributing.com";
                                                string smsg = string.Empty;
                                                if (adminStatusChange)
                                                    smsg = string.Format("Thank you for submitting RMA# <b>{0}</b> Dated: <b>{1}</b>, Your RMA has been processed and Current Status is <b>{2}</b>.", rmaNumber, txtRMADate.Text, lblStatus.Text);// + totalESN;
                                                else
                                                    smsg = string.Format("Thank you for submitting RMA# <b>{0}</b> Dated: <b>{1}</b>, we shall be contacting you shortly. Please do not send us RMA Items until APPROVED \r\n  Current Status = <b>{2}.</b>", rmaNumber, txtRMADate.Text, lblStatus.Text);// +totalESN;
                                                //commented on 26/8/2019
                                                //SendClientMail(smsg, emailTo, emailAddr, txtCustName.Text, totalESN, rmaNumber, txtRMADate.Text, lblStatus.Text);
                                            }
                                            catch (Exception ex)
                                            {
                                                lbl_msg.Text = ex.Message;
                                            }
                                        }

                                    }
                                    //rmaGUID = Convert.ToInt32(rmaDT.Rows[0]["rma_guid"]);
                                }
                            }
                            
                            //if (ViewState["esn"] == null)
                            //{
                            //    DlRma.DataSource = null;
                            //    DlRma.DataBind();
                            //}
                            //btnSubmitRMA.Enabled = false;
                            //btnValidate.Enabled = false;
                            hdnValidateESNs.Value = string.Empty;
                            DisableAll();
                            lbl_msg.Text = string.Empty;
                            if (rmaGUID > 0)
                            {

                                
                                btn_Cancel.Visible = false;

                                //Response.Redirect("~/RMA/RMAQuery.aspx?search=rma", false);
                                if (Session["adm"] != null)
                                    lblConfirm.Text = string.Format(successMessageAdmin, rmaNumber);
                                else
                                    lblConfirm.Text = string.Format(successMessage, rmaNumber);

                                btnSubmitRMA.Visible = false;
                                btnValidate.Visible = false;

                                btnNewRMA.Visible = true;
                                btnBack.Visible = true;

                                //rmaGUID = 0;
                                //ViewState["rmaGUID"] = rmaGUID;
                                //ViewState["validate"] = null;
                                //generateRMA();
                            }
                            else
                            {
                                //reset();
                                if (Session["adm"] != null)
                                    lblConfirm.Text = string.Format(successMessageAdmin, rmaNumber);
                                else
                                    lblConfirm.Text = string.Format(successMessage, rmaNumber);
                                //btnSubmitRMA.Enabled = false;
                                rmaGUID = 0;
                                ViewState["rmaGUID"] = rmaGUID;
                                ViewState["validate"] = null;

                                btnSubmitRMA.Visible = false;
                                btnValidate.Visible = false;

                                btnNewRMA.Visible = true;
                                btnBack.Visible = false;
                                btn_Cancel.Visible = false;
                                //generateRMA();
                                
                                
                                //if (rmaInfoObj.RmaDetails.Count > 0)
                                //{
                                //    if (Session["adm"] != null)
                                //        lblConfirm.Text = string.Format(successMessage, rmaNumber);
                                //    else
                                //        lblConfirm.Text = successMessageAdmin;
                                //    generateRMA();
                                //}
                                //else
                                //    lblConfirm.Text = "There is no esn to insert!!";

                            }
                        }
                        else
                        {
                            lblConfirm.Text = "RMA does not have ESN assigned.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblConfirm.Text = ex.Message.ToString();
                    }



                }

                else
                    lblConfirm.Text = "Date Can't be empty";

                lblConfirm.Visible = true;
            }
        }
        protected void btnNewRMA_click(object sender, EventArgs e)
        {
            Response.Redirect("~/RMA-Form.aspx?mode=esn", false);
        }
        private SV.Framework.Models.RMA.RMA RmaInfo(int rmaGUID, int rmastatus, int modifiedby, int createdby, int userid, string custName, string address, string city, string state, string zip, string email, string phone)
        {
            string storeID = string.Empty;
            if (ddlStoreID.SelectedIndex > 0)
                storeID = ddlStoreID.SelectedValue;
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>(); //RmaDocumentsList();
            //List<RmaDocuments> rmaDocList = RmaDocumentsList();
            
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = new List<SV.Framework.Models.Old.RMA.RMADetail>();
            fill_esnList(ref rmaEsnLookup, false, rmaGUID);
            Session["rmadetailslist"] = rmaEsnLookup;
            if (Session["rmaDocList"] != null)
                rmaDocList = (List<RmaDocuments>)Session["rmaDocList"];

            SV.Framework.Models.RMA.RMA rmaInfoObj = new SV.Framework.Models.RMA.RMA();
            rmaInfoObj.RmaGUID = rmaGUID;
            rmaInfoObj.RmaNumber = txtRmaNum.Text;
            rmaInfoObj.CustomerRMANumber = txtRMACustomerNumber.Text;

            rmaInfoObj.RmaDate = (txtRMADate.Text.Trim().Length > 0 ? Convert.ToDateTime(txtRMADate.Text.Trim()) : DateTime.MinValue);
            rmaInfoObj.RmaStatusID = rmastatus;
            rmaInfoObj.ModifiedBy = modifiedby;
            rmaInfoObj.CreatedBy = createdby;
            rmaInfoObj.RmaContactName = custName;
            rmaInfoObj.Address = address;
            rmaInfoObj.City = city;
            rmaInfoObj.State = state;
            rmaInfoObj.Zip = zip;
            rmaInfoObj.Email = email;
            rmaInfoObj.Phone = phone;
            rmaInfoObj.AVComments = txtAVComments.Text.Trim();
            //rmaInfoObj.LocationCode = txtLocationCode.Text.Trim();
            rmaInfoObj.StoreID = storeID;

            //rmaInfoObj.CustomerEmail = txtCustEmail.Text;
            rmaInfoObj.UserID = userid;
            //rmaInfoObj.PoNum = txtPo_num.Text;
            rmaInfoObj.Comment = txtRemarks.Text;
            //rmaInfoObj.Xml = RmaDetailXml;
            rmaInfoObj.RmaDetails = rmaEsnLookup;
            rmaInfoObj.RmaDocumentList = rmaDocList;

            return rmaInfoObj;
        }

        public string ShowRMADetailStatus()
        {
            int userID = 0;
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);
            int rmaGUID = 0;
            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);


            if (userID == 0 && rmaGUID > 0)
            {
                return "tableInDatalist";
            }
            else
            {
                return "hideTable";
            }
        }

        public string HideShowDisposition()
        {
            int rmaGUID = 0;
            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

            if (rmaGUID > 0)
            {
                return "tableInDatalist";
            }
            else
            {
                return "hideTable";
            }
        }


        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            //int userID = 0;
            //int companyID = 0;
            //int rmaGUID = 0;
            //int rmadrtGUID;
            //int hdnReasonID = 0;
            //string reasonExp = string.Empty;
            //string reasonvalue = string.Empty;
            
            //if (ViewState["userID"] != null)
            //    userID = Convert.ToInt32(ViewState["userID"]);
            
            ////SV.Framework.Models.RMA.RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(companyID, "", 1, -1);
            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    Label lblStatusheader = (Label)e.Item.FindControl("lblHeader");
            //    if (userID > 0)
            //    {
            //        lblStatusheader.Visible = false;
            //    }
            //    else
            //        lblStatusheader.Visible = true;
            //}

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //if (ViewState["company"] != null)
                //    company = ViewState["company"].ToString();

                //if (ViewState["companyID"] != null)
                //    companyID = Convert.ToInt32(ViewState["companyID"]);
                //if (ViewState["rmaGUID"] != null)
                //    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

                DropDownList ddReason = (DropDownList)e.Item.FindControl("ddReason");
                DropDownList dpWarranty = (DropDownList)e.Item.FindControl("dpWarranty");
                DropDownList dpDisposition = (DropDownList)e.Item.FindControl("dpDisposition");
                //Hashtable htReason = RMAUtility.getReasonHashtable();
                //GetEnumForBind(typeof(avii.Classes.ReasonType));

                //ddReason.DataSource = htReason;
                //ddReason.DataTextField = "value";
                //ddReason.DataValueField = "key";
                //ddReason.DataBind();

                //ListItem li = new ListItem("None", "0");
                //ddReason.Items.Insert(0, li);

                HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
                HiddenField hdnReason = (HiddenField)e.Item.FindControl("hdnReason");
                HiddenField hdnWarranty = (HiddenField)e.Item.FindControl("hdnWarranty");
                HiddenField hdnDisposition = (HiddenField)e.Item.FindControl("hdnDisposition");
                DropDownList ddl_RmaItemStatus = (DropDownList)e.Item.FindControl("ddl_Status");

                Button btntriage = (Button)e.Item.FindControl("btntriage");
                //if (Session["adm"] != null)
                btntriage.OnClientClick = "openTriageDialogAndBlock('" + btntriage.Text + "', '" + btntriage.ClientID + "')";

                Button btnRMAReceive = (Button)e.Item.FindControl("btnRMAReceive");
                if (Session["adm"] != null)
                    btnRMAReceive.OnClientClick = "openRMAReceiveDialogAndBlock('" + btnRMAReceive.Text + "', '" + btnRMAReceive.ClientID + "')";
                else
                    btnRMAReceive.Visible = false;


                if (Session["customerRMAStatusList"] != null)
                {
                    List<CustomerRMAStatus> customerRMAStatusList = Session["customerRMAStatusList"] as List<CustomerRMAStatus>;

                    ddl_RmaItemStatus.DataSource = customerRMAStatusList;
                    ddl_RmaItemStatus.DataValueField = "StatusID";
                    ddl_RmaItemStatus.DataTextField = "StatusDescription";

                    ddl_RmaItemStatus.DataBind();
                    ddl_RmaItemStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));


                }

                //CheckBox chkESN2 = (CheckBox)e.Item.FindControl("chkESN");
                //TextBox txtEsn12 = (TextBox)e.Item.FindControl("txt_ESN");
                //TextBox txtCallTime2 = (TextBox)e.Item.FindControl("txtCTime");
                //TextBox txtNotes2 = (TextBox)e.Item.FindControl("txtNotes");


                ddl_RmaItemStatus.SelectedValue = hdnStatus.Value;
                ddReason.SelectedValue = hdnReason.Value;
                dpWarranty.SelectedValue = hdnWarranty.Value;
                dpDisposition.SelectedValue = hdnDisposition.Value;

                //hdnStatus.Value = "0";
                //if (Convert.ToInt32(ddlStatus.SelectedValue) > 1)
                //{
                //    chkESN2.Checked = true;
                //    chkESN2.Enabled = false;
                //    txtEsn12.ReadOnly = true;
                //    txtCallTime2.ReadOnly = true;
                //}
                //else
                //{
                //    chkESN2.Enabled = true;
                //    chkESN2.Checked = true;
                //}
                //ddl_RmaItemStatus.Visible = false;

                //if (rmaGUID > 0)
                //{
                //    //CheckBox chkESN = (CheckBox)e.Item.FindControl("chkESN");
                //    HiddenField hdnRMADetGUID = (HiddenField)e.Item.FindControl("hdnRDOID");

                //    rmadrtGUID = Convert.ToInt32(hdnRMADetGUID.Value);

                //    //List<SV.Framework.Models.Old.RMA.RMADetail> rmaDetaillist = RMAUtility.getRMADetail(rmaGUID, rmadrtGUID, "");
                //    if (rmadrtGUID > 0)
                //    {
                //        if (pvtEsnList == null)
                //        {
                //            pvtEsnList = (List<SV.Framework.Models.Old.RMA.RMADetail>)Session["rmadetailslist"];
                //        }
                //        var rmaDetaillist = (from item in pvtEsnList where item.rmaDetGUID.Equals(rmadrtGUID) select item).ToList();

                //        if (rmaDetaillist != null && rmaDetaillist.Count > 0)
                //        {
                //            ddReason.SelectedValue = rmaDetaillist[0].Reason.ToString();
                //            ddl_RmaItemStatus.SelectedValue = rmaDetaillist[0].StatusID.ToString();
                //            //if ("Pending" != rmaDetaillist[0].Status.ToString())
                //            //{
                //            //    chkESN.Checked = true;
                //            //    chkESN.Enabled = false;
                //            //}
                //            if (userID == 0)
                //            {
                //                if (Convert.ToInt32(ddlStatus.SelectedValue) > 1)
                //                {
                //                    chkESN2.Checked = true;
                //                    chkESN2.Enabled = false;
                //                    //txtEsn12.ReadOnly = true;
                //                    //txtCallTime2.ReadOnly = true;
                //                    //txtNotes2.ReadOnly = true;
                //                    //ddReason.Enabled = false;
                //                }
                //                else
                //                {
                //                    chkESN2.Enabled = true;
                //                    chkESN2.Checked = true;
                //                }
                //            }
                //        }
                //    }
                //}

                //if (userID == 0)
                //{
                //    ddl_RmaItemStatus.Visible = true;
                //}
                //else
                //{
                //    ddl_RmaItemStatus.Visible = false;
                //    //hdnStatus.Value = "0";
                //}
                //TextBox txtCallTime = (TextBox)e.Item.FindControl("txtCTime");
                //TextBox txtNotes = (TextBox)e.Item.FindControl("txtNotes");
            }

        }
        private void SendClientMail(string smsg, string custEmail, string emailCC, string toName, string esnDetail, string rmaNumber, string rmaDate, string rmaStatus)
        {
            // string smsg = string.Empty, filename = string.Empty, subject = string.Empty;
            try
            {

                //smsg = string.Format("Thank you for submitting <b>RMA# {0}</b>  Dated: {1}, we shall be contacting you shortly. Please do not send us RMA Items until APPROVED \n\n<b>Current Status = {2}</b>", rmaNumber, rmaDate, status);
                if (custEmail.Length > 0)
                {
                    //string rmaemail = string.Empty;
                    //try
                    //{
                    //    rmaemail = System.Configuration.ConfigurationSettings.AppSettings["rmaemail"];
                    //}
                    //catch { }
                    //string emailAddr = "rma@wsaPortal.com";

                    string url = System.Configuration.ConfigurationManager.AppSettings["RMAEmailURL"].ToString();
                    //old
                    //clsGeneral.SendEmailNew(custEmail, emailCC, "WSA RMA Department", smsg, toName);
                    //Added email tempalte URL 2/23/2015
                    //commented on 268/2019
                    //clsGeneral.SendEmailWithTemplate(custEmail, emailCC, "WSA RMA Department", smsg, toName, url, esnDetail, rmaNumber, rmaDate, rmaStatus);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custEmail);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
            }
        }
        protected void btnBackRMAQuery_click(object sender, EventArgs e)
        {
            Response.Redirect("~/RMA/RMAQueryNew.aspx?search=rma");
        }
        private void reset()
        {
            Session["rmaDocList"] = null;
            lblConfirm.Text = string.Empty;
            ViewState["validate"] = null;
            ViewState["allowRMA"] = null;
            int companyID = 0;
            int userID = 0;
            int rmaGUID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);

            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            lblConfirm.Text = string.Empty;
            txtRMADate.Text = DateTime.Now.ToShortDateString();
            txtRemarks.Text = "";
            //txtPo_num.Text = "";
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCustName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            // txtState.Text = string.Empty;
            dpState.SelectedIndex = 0;
            txtZip.Text = string.Empty;
            chkAll.Checked = false;
            
            if (userID == 0)
            {
                ddlCompany.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 1;
                txtAVComments.Text = string.Empty;

                if (rmaGUID == 0)
                {
                    companyID = 0;
                    ViewState["companyID"] = companyID;
                    hdncompanyname.Value = string.Empty;
                }
            }
            Dl_esn_bind("");
        }
        private void DisableAll()
        {
            //lblConfirm.Text = string.Empty;
            //ViewState["validate"] = null;
            //int companyID = 0;
            int userID = 0;
            //int rmaGUID = 0;
            //if (ViewState["companyID"] != null)
            //    companyID = Convert.ToInt32(ViewState["companyID"]);

            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);

            //if (ViewState["rmaGUID"] != null)
            //    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            //lblConfirm.Text = string.Empty;
            txtRMADate.Enabled = false;
            txtRemarks.Enabled = false;
            //txtPo_num.Text = "";
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtCustName.Enabled = false;
            txtAddress.Enabled = false;
            txtCity.Enabled = false;
            //txtState.Enabled = false;
            dpState.Enabled = false;
            txtZip.Enabled = false;
            txtAVComments.Enabled = false;
            chkAll.Enabled = false;
            //txtLocationCode.Enabled = false;
            

            if (userID == 0)
            {
                ddlCompany.Enabled = false;
                ddlStatus.Enabled = false;

                
            }
            DlRma.Enabled = false;
            
        }
       
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            //string company = ConfigurationManager.AppSettings["company"].ToString();
            //string company = string.Empty;
            //if (ViewState["company"] != null)
            //    company = ViewState["company"].ToString();
            //SV.Framework.Models.RMA.RMAUtility rma_obj = new SV.Framework.Models.RMA.RMAUtility();
            //SV.Framework.Models.RMA.RMAUserCompany objrmaUserCompany = null;

            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());

            BindUserStores(0, companyID);
            BindRMAStatuses(companyID); 
            ViewState["companyID"] = companyID;
            hdncompanyname.Value = ddlCompany.SelectedItem.Text;
            //if (Request["mode"] == "esn")
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(0)</script>", false);

            if (Request["mode"] == "esn")
            {
                Dl_esn_bind("");
            }

            
        }
        protected void btnComments_Click(object sender, EventArgs e)
        {
            string custmEmail, emailCC, toName, rmaNumber;
            custmEmail = emailCC = toName = rmaNumber = string.Empty;
            try
            {
                int companyID, userid;
                companyID = userid = 0;
                string customerEmail = string.Empty;
                string overrideEmail = string.Empty;
                if (ViewState["companyID"] != null)
                    int.TryParse(ViewState["companyID"].ToString(), out companyID);

                if (Session["adm"] != null)
                {
                    List<avii.Classes.CustomerEmail> custEmail = new List<avii.Classes.CustomerEmail>();

                    //objRMAcompany = RMAUtility.getUserEmail(companyID);
                    //userid = objRMAcompany.UserID;
                    //customerEmail = objRMAcompany.Email;
                    custEmail = avii.Classes.CustomerEmailOperations.GetCustomerEmailList(companyID);
                    var userEmail = (from item in custEmail where item.ModuleGUID.Equals(75) select item).ToList(); //75 MODULEGUID IS FOR RMA
                    if (userEmail != null && userEmail.Count > 0)
                    {
                        //isEmail = userEmail[0].IsEmail;
                        userid = userEmail[0].UserID;
                        customerEmail = userEmail[0].Email;
                        overrideEmail = userEmail[0].OverrideEmail;
                        if (string.IsNullOrEmpty(overrideEmail) && string.IsNullOrEmpty(customerEmail))
                            customerEmail = userEmail[0].CompanyEmail;

                    }
                }
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    int rmaGUID = 0;
                    if (Request["rmaGUID"] != "" && Request["rmaGUID"] != null)
                        rmaGUID = Convert.ToInt32(Request["rmaGUID"]);

                    //string emailAddr = "rma@wsaPortal.com";
                    string emailTo = string.Empty;
                    // string emailCC = string.Empty;
                    //if (!string.IsNullOrEmpty(userInfo.Email) && userInfo.UserType != "Internal User")
                    //{
                    //    emailCC = userInfo.Email; // +"," + txtEmail.Text.Trim();
                    //}
                    //else if (!string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Internal User")
                    //{
                    //    emailCC = customerEmail; // +"," + txtEmail.Text.Trim();
                    //}
                    string emailAddress = txtEmail.Text.Trim();

                    custmEmail = avii.Classes.clsGeneral.CustomerEmailAddress(userInfo, emailAddress, customerEmail, overrideEmail);

                    //emailCC = txtEmails.Text.Trim();
                    //custmEmail = custmEmail + "," + txtEmail.Text.Trim();

                    lblEmails.Text = custmEmail;

                    //string[] array = emailAddr.Split(',');
                    //if (array.Length > 0)
                    //    emailTo = array[0];
                    //if (array.Length > 1)
                    //    emailCC = array[1];
                    //string emailAddr = "rma@wsadistributing.com";

                    //string url = System.Configuration.ConfigurationManager.AppSettings["RMACommentEmailURL"].ToString();
                    //url = url + "?rmaGUID=" + rmaGUID;
                    //SendCommentEMail(custmEmail, emailCC, toName, rmaNumber, url);
                    //System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custmEmail);
                }

            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
                lbl_msg.Text = ex.Message;
            }


            RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        private void SendCommentEMail(string custEmail, string emailCC, string toName, string rmaNumber, string url)
        {
            try
            {

                if (custEmail.Length > 0)
                {
                    //Add email tempalte URL 
                    avii.Classes.clsGeneral.SendEmailWithTemplate(custEmail, emailCC, "WSA RMA Department", string.Empty, toName, url, string.Empty, rmaNumber, string.Empty, string.Empty);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custEmail);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            string custmEmail, emailCC, toName, rmaNumber;
            custmEmail = emailCC = toName = rmaNumber = string.Empty;
            int rmaGUID = 0;
            try
            {

                if (Request["rmaGUID"] != "" && Request["rmaGUID"] != null)
                {
                    rmaGUID = Convert.ToInt32(Request["rmaGUID"]);
                }
                toName = txtCustName.Text;
                rmaNumber = txtRmaNum.Text;
                emailCC = txtEmails.Text.Trim();

                custmEmail = lblEmails.Text;

                //string[] array = emailAddr.Split(',');
                //if (array.Length > 0)
                //    emailTo = array[0];
                //if (array.Length > 1)
                //    emailCC = array[1];
                string emailAddr = "rma@wsadistributing.com";

                string url = System.Configuration.ConfigurationManager.AppSettings["RMACommentEmailURL"].ToString();
                url = url + "?rmaGUID=" + rmaGUID;
               // SendCommentEMail(custmEmail, emailCC, toName, rmaNumber, url);

                lblCommMsg.Text = "Sent successfully";
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custmEmail);


            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
            }

        }
        protected void btnAddtriage_click(object sender, EventArgs e)
        {
            Button btnAddtriage = (Button)sender;
            DataListItem item = (DataListItem)btnAddtriage.NamingContainer;
            int itemindex = item.ItemIndex;
            int rmadelGUID = 0;
            lblRMA.Text = txtRmaNum.Text;
                 
            ViewState["rmadelitemindex"] = itemindex;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemindex)
                {
                    txtTriageNotes.Text = rmaEsnLookup[itemindex].TriageNotes;
                    if (rmaEsnLookup[itemindex].TriageStatusID > 0)
                        ddlTriage.SelectedValue = rmaEsnLookup[itemindex].TriageStatusID.ToString();
                    else
                        ddlTriage.SelectedIndex = 0;

                    rmadelGUID = Convert.ToInt32(rmaEsnLookup[itemindex].rmaDetGUID);
                    ViewState["rmadelGUID"] = rmadelGUID;
                }
            }
            if (Session["adm"] == null)
            {
                btnTriage.Visible = false;

            }

            RegisterStartupScript("jsUnblockDialog", "unblockTriageDialog();");

        }

        protected void btnTriage_Click(object sender, EventArgs e)
        {
            int rmadelitemindex, triageStatusID;
            rmadelitemindex = 0;
            string triageNotes = txtTriageNotes.Text.Trim();
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;

            if (ViewState["rmadelitemindex"] != null)
                int.TryParse(ViewState["rmadelitemindex"].ToString(), out rmadelitemindex);

            int.TryParse(ddlTriage.SelectedValue, out triageStatusID);

            if (triageStatusID == 0)
                triageStatusID = 1;

            //ViewState["rmadelitemindex"]
            rmaEsnLookup[rmadelitemindex].TriageStatusID = triageStatusID;
            rmaEsnLookup[rmadelitemindex].TriageNotes = triageNotes;
            Session["rmadetailslist"] = rmaEsnLookup;


            TriggerClientGridRefresh();
            RegisterStartupScript("jsUnblockDialog", "closedTriageDialog();");
        }
        private void ReBindLineItems()
        {
            if (Session["rmadetailslist"] != null)
            {
                List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = (List<SV.Framework.Models.Old.RMA.RMADetail>)Session["rmadetailslist"];

                DlRma.DataSource = rmaEsnLookup;
                DlRma.DataBind();
                hdnRmaItemcount.Value = rmaEsnLookup.Count.ToString();
            }
        }
        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            ReBindLineItems();
        }
        private void TriggerClientGridRefresh()
        {
            string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
            RegisterStartupScript("jsGridRefresh", script);
        }

        protected void btnAddRMAReceive_click(object sender, EventArgs e)
        {
            lblReceiveRMA.Text = string.Empty;
            txtRSKU.Text = string.Empty;
            txtRESN.Text = string.Empty;
            txtShipTracking.Text = string.Empty;
            hdnRESN.Value = string.Empty;
            lblCreateDate.Text = string.Empty;
            btnDelete.Visible = false;
            btnRRMA.Visible = true;
            btnRRMACancel.Text = "Cancel";

            txtRESN.Enabled = true;
            txtRSKU.Enabled = true;
            txtShipTracking.Enabled = true;
            lblDate.Visible = false;
            Button btnRMAReceive = (Button)sender;
            DataListItem item = (DataListItem)btnRMAReceive.NamingContainer;
            int itemindex = item.ItemIndex;
            int rmadelGUID = 0;
            ViewState["rmadelitemindex"] = itemindex;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
                if (rmaEsnLookup.Count >= itemindex)
                {
                    hdnRESN.Value = rmaEsnLookup[itemindex].ESN;

                    if (!string.IsNullOrEmpty(rmaEsnLookup[itemindex].ShippingTrackingNumber) && btnRMAReceive.Text == "SHOW RECEIPT")
                    {
                        txtRESN.Text = rmaEsnLookup[itemindex].ESN;
                        txtRESN.Enabled = false;
                        txtRSKU.Enabled = false;
                        txtShipTracking.Enabled = false;

                        btnRRMA.Visible = false;
                        btnRRMACancel.Text = "Close";
                        lblDate.Visible = true;
                        btnDelete.Visible = true;
                        //if (!string.IsNullOrEmpty(rmaEsnLookup[itemindex].NewSKU))
                        txtRSKU.Text = rmaEsnLookup[itemindex].NewSKU;
                        //else
                        //    txtRSKU.Text = rmaEsnLookup[itemindex].ItemCode;

                        txtShipTracking.Text = rmaEsnLookup[itemindex].ShippingTrackingNumber;
                        lblCreateDate.Text = rmaEsnLookup[itemindex].CreateDate.ToString();
                    }

                    rmadelGUID = Convert.ToInt32(rmaEsnLookup[itemindex].rmaDetGUID);
                    ViewState["rmadelGUID"] = rmadelGUID;
                }
            }


            RegisterStartupScript("jsUnblockDialog", "unblockRMAReceiveDialog();");

        }

        protected void btnRMAReceive_click(object sender, EventArgs e)
        {
            int rmadelitemindex;
            rmadelitemindex = 0;
            string newESN = txtRESN.Text.Trim();
            string newSKU = txtRSKU.Text.Trim();
            string shippingTrackingNumber = txtRESN.Text.Trim();

            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;

            if (ViewState["rmadelitemindex"] != null)
                int.TryParse(ViewState["rmadelitemindex"].ToString(), out rmadelitemindex);

            //int.TryParse(ddlTriage.SelectedValue, out triageStatusID);

            //ViewState["rmadelitemindex"]
            if (!string.IsNullOrEmpty(newESN))
                rmaEsnLookup[rmadelitemindex].ESN = newESN;

            rmaEsnLookup[rmadelitemindex].NewSKU = newSKU;
            rmaEsnLookup[rmadelitemindex].UPC = newSKU;
            rmaEsnLookup[rmadelitemindex].ShippingTrackingNumber = shippingTrackingNumber;
            rmaEsnLookup[rmadelitemindex].StatusID = 2;
            Session["rmadetailslist"] = rmaEsnLookup;


            TriggerClientGridRefresh();
            RegisterStartupScript("jsUnblockDialog", "closedRMAReceiveDialog();");
        }
        protected void btnRMAReceiveDelete_click(object sender, EventArgs e)
        {
            lblReceiveRMA.Text = string.Empty;
            try
            {
                int rmadelitemindex, rmadetguid, userID;
                rmadelitemindex = rmadetguid = userID = 0;
                string newESN = string.Empty; // txtRESN.Text.Trim();
                string newSKU = string.Empty; //txtRSKU.Text.Trim();
                string shippingTrackingNumber = string.Empty; //txtShipTracking.Text.Trim();

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    
                    userID = userInfo.UserGUID;
                    //txtLocationCode.Enabled = false;
                }
                List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = Session["rmadetailslist"] as List<SV.Framework.Models.Old.RMA.RMADetail>;

                if (ViewState["rmadelitemindex"] != null)
                    int.TryParse(ViewState["rmadelitemindex"].ToString(), out rmadelitemindex);

                //rmadetguid= Convert.ToInt64(rmaEsnLookup[rmadelitemindex].rmaDetGUID);
                int.TryParse(rmaEsnLookup[rmadelitemindex].rmaDetGUID.ToString(), out rmadetguid);

                //ViewState["rmadelitemindex"]
                //if (!string.IsNullOrEmpty(newESN))
                //    rmaEsnLookup[rmadelitemindex].ESN = newESN;

                rmaEsnLookup[rmadelitemindex].NewSKU = newSKU;
                //rmaEsnLookup[rmadelitemindex].UPC = newSKU;
                rmaEsnLookup[rmadelitemindex].ShippingTrackingNumber = shippingTrackingNumber;
                rmaEsnLookup[rmadelitemindex].StatusID = 1;
                //Session["rmadetailslist"] = rmaEsnLookup;
                rmaEsnLookup[rmadelitemindex].CreateDate = DateTime.MinValue;
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();


               // rmaUtility.Delete_RMA_receive(rmadetguid, userID);

                Session["rmadetailslist"] = rmaEsnLookup;//

                TriggerClientGridRefresh();
                RegisterStartupScript("jsUnblockDialog", "closedRMAReceiveDialog();");
            }
            catch (Exception ex)
            {
                lblReceiveRMA.Text = ex.Message;

            }
        }

        protected void txtRESN_TextChanged(object sender, EventArgs e)
        {
            int companyID, rmaGUID;
            string newESN = txtRESN.Text.Trim();
            ///string oldESN = hdnRESN.Text.Trim();

            int.TryParse(ViewState["companyID"].ToString(), out companyID);

            int.TryParse(Request["rmaGUID"].ToString(), out rmaGUID);


            ValidateESN(newESN, companyID, rmaGUID);

        }
        private void ValidateESN(string esn, int companyID, int rmaGUID)
        {
            //esn lookup
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int userId = 0;
            if (ViewState["userID"] != null)
            {
                userId = Convert.ToInt32(ViewState["userID"]);
            }

            if (!string.IsNullOrEmpty(esn))
            {
                List<SV.Framework.Models.Old.RMA.RMADetail> esnlist = rmaUtility.getRMAesn(companyID, esn, "", 0, rmaGUID, userId);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        if (esnlist[0].ItemCode.ToLower() != "not found")
                        {
                            txtRSKU.Text = esnlist[0].ItemCode;
                            txtRSKU.Enabled = false;
                        }
                    }
                }
            }
        }


        
    }
}