using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Text.RegularExpressions;
using System.IO;

namespace avii.RMA
{
    public partial class NewRmaForm : System.Web.UI.Page
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }

           
            if (!IsPostBack)
            {
                int companyID = 0;
                int userID = 0;

                int rmaGUID = 0;
                ViewState["rmadetailslist"] = null;
                BindStates();
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                    userID = userInfo.UserGUID;
                }

                string company = ConfigurationManager.AppSettings["company"].ToString();

                int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
                ViewState["company"] = company;
                ViewState["maxEsn"] = maxEsn;
                if (Session["adm"] == null)
                {
                    userID = Convert.ToInt32(Session["UserID"]);
                    divAvc.Disabled = true;
                    txtAVComments.Enabled = false;
                    imgUpload.OnClientClick = "openRmaDocDialogAndBlock('RMA Document Upload - Customer', 'imgUpload')";
                    pnlDoc.Style.Add("display", "none");
                    //txtNewSKU.Enabled = false;
                    ddlSKU.Enabled = false;
                }
                else
                {
                    imgUpload.OnClientClick = "openRmaDocDialogAndBlock('RMA Document Upload - Admin', 'imgUpload')";

                    //BindCompany();
                    ViewState["createdby"] = userID;
                    userID = 0;
                    divAvc.Disabled = false;
                    txtAVComments.Enabled = true;
                    ddlSKU.Enabled = true;
                }
                //BindCompany();
                ViewState["userID"] = userID;
                txtRMADate.Text = DateTime.Now.ToShortDateString();
                txtRecievedOn.Text = DateTime.Now.ToShortDateString();
                if (Request["rmaGUID"] != "" && Request["rmaGUID"] != null)
                {
                    rmaGUID = Convert.ToInt32(Request["rmaGUID"]);

                }
                else
                {
                    btnHistory.Visible = false;
                    //rmaGUID = 0;
                }

                ViewState["rmaGUID"] = rmaGUID;

                if (userID > 0)
                {
                    if (companyID == 0)
                    {
                        RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(-1, "", -1, userID);
                        companyID = objRMAcompany.CompanyID;
                    }
                    if (rmaGUID > 0)
                    {
                        btn_Cancel.Visible = false;
                        btnBack.Visible = true;
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
                    chkApplyAll.Visible = false;
                    lblStatus.Text = "Pending";
                    lblStatus.Visible = true;

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
                    
                }

                else if (userID == 0)
                {
                    if (ddlCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
                    hdnUserID.Value = "0";
                    if (rmaGUID > 0)
                    {
                        btn_Cancel.Visible = false;
                        btnBack.Visible = true;
                        //hdnUserID.Value = "1";
                        
                        btnpanel.Visible = true;
                        lblCompany.Visible = false;
                        ddlCompany.Visible = false;
                        lblrmanumber.Visible = true;
                        txtRmaNum.Visible = true;
                        txtRMADate.Visible = true;
                        lblRMADate.Visible = true;
                        rmadtpanel.Visible = true;
                        // lblrmadate_md.Visible = true;
                       
                    }
                    else
                    {
                        // Donot show RMA Number to the user if they are adding new RMA
                        lblrmanumber.Visible = false;
                        txtRmaNum.Visible = false;
                        btn_Cancel.Visible = true;
                        btnBack.Visible = false;
                        if (Request["mode"] == "import")
                        {
                            //btnpanel.Visible = false;
                            lblrmanumber.Visible = false;
                            txtRmaNum.Visible = false;
                            txtRMADate.Visible = false;
                            lblRMADate.Visible = false;
                            rmadtpanel.Visible = false;

                            // lblrmadate_md.Visible = false;

                        }
                        else
                        {
                            lblrmanumber.Visible = true;
                            txtRmaNum.Visible = true;
                            txtRMADate.Visible = true;
                            lblRMADate.Visible = true;
                            rmadtpanel.Visible = true;
                            // lblrmadate_md.Visible = true;
                        }
                        
                        //btnpanel.Visible = false;
                        lblCompany.Visible = true;
                        ddlCompany.Visible = true;

                        lblStatus.Visible = false;

                       
                        
                        if (Request["mode"] == "esn")
                        {
                            ViewState["esn"] = "esn";
                            
                            btnpanel.Visible = true;
                        }


                    }

                }

                if (Request["companyID"] != null && Request["companyID"] != "")
                    companyID = Convert.ToInt32(Request["companyID"]);
                ViewState["companyID"] = companyID;
                if (userID == 0)
                {
                    BindCompany();
                    ddlStatus.SelectedIndex = 14;

                }
                if (rmaGUID > 0)
                {

                    GetRMAnRMADetailInfo(rmaGUID, userID);
                    if (userID == 0)
                    {
                        BindCompany();

                        lblStatus.Visible = false;
                        ddlStatus.Visible = true;
                        chkApplyAll.Visible = true;
                    }
                    else
                    {
                        ddlStatus.Visible = false;
                        lblStatus.Visible = true;
                        chkApplyAll.Visible = false;
                    }
                }
                else
                {
                    GenerateRMA();
                    chkApplyAll.Visible = false;
                    if (ViewState["esn"] != null)
                    { 
                        BindRMADetail();
                    }
                }
                if(Request["success"] != null)
                {
                    if (Session["msg"] != null)
                        lblConfirm.Text = Session["msg"] as string;
                    else
                        lblConfirm.Text = string.Empty;
                }
                
            }

        }
        //To autogenerate new RMA Number
        private void GenerateRMA()
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
        private void BindRMADetail()
        {
            List<avii.Classes.RMADetail> esnList = new List<RMADetail>();
            RMADetail objRMAesn = new RMADetail();
            objRMAesn.CallTime = 0;
            objRMAesn.Notes = string.Empty;
            objRMAesn.Reason = string.Empty;
            objRMAesn.StatusID = 1;
            objRMAesn.rmaDetGUID = 0;
            objRMAesn.RecievedOn = null;
            objRMAesn.DeviceCondition = 0;
            objRMAesn.DeviceDefect = string.Empty;
            objRMAesn.DeviceDesignation = 0;
            objRMAesn.DeviceState = 0;
            objRMAesn.Disposition = 0;
            objRMAesn.LocationCode = string.Empty;
            objRMAesn.NewSKU = string.Empty;
            objRMAesn.ReStockingDate = null;
            objRMAesn.ReStockingFee = 0;
            objRMAesn.Warranty = 0;
            objRMAesn.WarrantyExpieryDate = null;
            esnList.Add(objRMAesn);
            rptRmaItem.DataSource = esnList;
            rptRmaItem.DataBind();
            hdnRmaItemcount.Value = "0";
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




        }

        //Bind Company DropdownList
        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();

        }

        private void BindCompanyOld()
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                ddlCompany.DataSource = RMAUtility.getRMAUserCompany(-1, "", -1, -1);
                ddlCompany.DataValueField = "companyID";
                ddlCompany.DataTextField = "companyName";
                ddlCompany.DataBind();
                ListItem item = new ListItem("", "0");
                ddlCompany.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                lblConfirm.Text = ex.Message.ToString();
            }
        }

        private void ValidateESN(ref RMADetail avEsn, int companyID, int rmaGUID)
        {
            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {

                List<avii.Classes.RMADetail> esnlist = RMAUtility.GetRMAESN(companyID, avEsn.ESN,  0);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        avEsn.UPC = esnlist[0].UPC.ToString();
                        avEsn.ItemCode = esnlist[0].ItemCode;
                        avEsn.rmaDetGUID = esnlist[0].rmaDetGUID;
                        avEsn.AVSalesOrderNumber = esnlist[0].AVSalesOrderNumber;
                        avEsn.PurchaseOrderNumber = esnlist[0].PurchaseOrderNumber;
                        avEsn.AllowRMA = esnlist[0].AllowRMA;
                        avEsn.AllowDuplicate = esnlist[0].AllowDuplicate;
                    }
                }
            }
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }


        protected void btnCustomerComments_Click(object sender, EventArgs e)
        {
            try
            {
                int rmaGUID = 0;
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                lblRNum.Text = string.Empty;
                Control tmp2 = LoadControl("~/controls/ForecastComment.ascx");
                avii.Controls.ForecastComment ctlRMAComments = tmp2 as avii.Controls.ForecastComment;
                pnlComments.Controls.Clear();
                //string info = e.CommandArgument.ToString();
                //string[] arr = info.Split(',');
                //if (arr.Length > 1)
                {
                    //int poid = Convert.ToInt32(arr[0]);
                    //string poNumber = arr[1];
                    lblRNum.Text = txtRmaNum.Text;
                    if (tmp2 != null)
                    {

                        ctlRMAComments.BindRMAComments(rmaGUID, "R");
                    }
                    pnlComments.Controls.Add(ctlRMAComments);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lbl_msg.Text = ex.Message;
            }

        }
        protected void btnAVComments_Click(object sender, EventArgs e)
        {
            try
            {
                int rmaGUID = 0;
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                lblRNum.Text = string.Empty;
                Control tmp2 = LoadControl("~/controls/ForecastComment.ascx");
                avii.Controls.ForecastComment ctlRMAComments = tmp2 as avii.Controls.ForecastComment;
                pnlComments.Controls.Clear();
                //string info = e.CommandArgument.ToString();
                //string[] arr = info.Split(',');
                //if (arr.Length > 1)
                {
                    //int poid = Convert.ToInt32(arr[0]);
                    //string poNumber = arr[1];
                    lblRNum.Text = txtRmaNum.Text;
                    if (tmp2 != null)
                    {

                        ctlRMAComments.BindRMAComments(rmaGUID, "A");
                    }
                    pnlComments.Controls.Add(ctlRMAComments);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lbl_msg.Text = ex.Message;
            }

        }

        //private void Fill_EsnList(ref List<RMADetail> rmaEsnLookup, bool isValidate, int rmaGUID)
        //{
        //    int statusID = 1;
        //    int companyID = 0;
        //    string esn = string.Empty;
        //    string externalESN = "External Esn";
        //    ViewState["duplicateESN"] = null;
        //    ViewState["ESNalreadyExists"] = null;
        //    ViewState["allowRMA"] = null;
        //    ViewState["ExternalEsn"] = null;
        //    if (ViewState["companyID"] != null)
        //        companyID = Convert.ToInt32(ViewState["companyID"]);

        //    Hashtable hshEsnDuplicateCheck = new Hashtable();

        //    {
        //        if (rmaEsnLookup == null)
        //            rmaEsnLookup = new List<RMADetail>();

        //        avii.Classes.RMADetail objRMAesn = null;
        //        foreach (DataListItem i in Dl_rma_detail.Items)
        //        {
        //            TextBox txtEsn = (TextBox)i.FindControl("txt_ESN");
        //            if (txtEsn.Text.Trim().Length > 0)
        //            {
        //                CheckBox chkESN1 = (CheckBox)i.FindControl("chkESN");
        //                if (chkESN1.Checked)
        //                {
        //                    esn = string.Empty;
        //                    if (chkApplyAll.Checked)
        //                    {
        //                        statusID = Convert.ToInt32(ddlStatus.SelectedValue);
        //                    }
        //                    else
        //                    {
        //                        DropDownList ddl_Status = (DropDownList)i.FindControl("ddl_Status");
        //                        if (ddl_Status.SelectedIndex > 0)
        //                            statusID = Convert.ToInt32(ddl_Status.SelectedValue);
        //                    }

        //                    HiddenField hdnrmaDetGuid = (HiddenField)i.FindControl("hdnRMADetGUID");


        //                    esn = txtEsn.Text.Trim();
        //                    if (string.IsNullOrEmpty(esn))
        //                    {
        //                        lblConfirm.Text = "ESN can not be empty";
        //                        break;
        //                    }

        //                    if (hshEsnDuplicateCheck.ContainsKey(esn))
        //                    {
        //                        lblConfirm.Text = string.Format("Duplicate ESN ({0}) is found", esn);
        //                        ViewState["duplicateESN"] = "1";
        //                    }
        //                    else
        //                    {
        //                        hshEsnDuplicateCheck.Add(esn, esn);


        //                        TextBox txtCallTime = (TextBox)i.FindControl("txtCallTime");
        //                        Label txtUPC = (Label)i.FindControl("lblItemCode");
        //                        //Label lblAvso = (Label)i.FindControl("lblAvso");
        //                        TextBox txtNotes = (TextBox)i.FindControl("txtNotes");
        //                        DropDownList ddReason = (DropDownList)i.FindControl("ddReason");
        //                        //HiddenField hdnAllowRMA = i.FindControl("hdnAllowRMA") as HiddenField;
        //                        //HiddenField hdnAllowDuplicate = i.FindControl("hdnDuplicate") as HiddenField;
        //                        //if (!Convert.ToBoolean(hdnAllowRMA.Value))
        //                        //{

        //                        //}
        //                        //if(txtUPC.Text.Trim() == "RMA Exists")
        //                        //{
        //                        //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
        //                        //    ViewState["ESNalreadyExists"] = "1";
        //                        //}

        //                        if (esn.Trim() != string.Empty)
        //                        {
        //                            if (txtCallTime.Text == "")
        //                                txtCallTime.Text = "0";

        //                            objRMAesn = new avii.Classes.RMADetail();
        //                            objRMAesn.ESN = esn;
        //                            //if (ViewState["validate"] == null)
        //                            //{
        //                            ValidateESN(ref objRMAesn, companyID, rmaGUID);
        //                            //if (objRMAesn.UPC == "RMA Exists")
        //                            //{
        //                            //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
        //                            //    ViewState["ESNalreadyExists"] = "1";
        //                            //}
        //                            if (!objRMAesn.AllowDuplicate)
        //                            {
        //                                lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
        //                                ViewState["ESNalreadyExists"] = "1";
        //                            }
        //                            if (externalESN == objRMAesn.UPC && Session["adm"] == null)
        //                            {
        //                                lblConfirm.Text = "RMA is not allowed for External Esn";
        //                                //ViewState["ExternalEsn"] = "1";
        //                            }
        //                            string esns = string.Empty;
        //                            if (!objRMAesn.AllowRMA && Session["adm"] == null)
        //                            {
        //                                if (ViewState["allowRMA"] == null)
        //                                    ViewState["allowRMA"] = esn;
        //                                else
        //                                {
        //                                    esns = ViewState["allowRMA"].ToString();

        //                                    ViewState["allowRMA"] = esns + ", " + esn;
        //                                }
        //                                if (esns == string.Empty)
        //                                    lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
        //                                else
        //                                    lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to these ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
        //                            }
        //                            txtUPC.Text = objRMAesn.UPC;
        //                            // lblAvso.Text = objRMAesn.AVSalesOrderNumber;
        //                            //}
        //                            objRMAesn.CallTime = Convert.ToInt32(txtCallTime.Text);
        //                            objRMAesn.Notes = txtNotes.Text;
        //                            objRMAesn.Reason = ddReason.SelectedValue;
        //                            objRMAesn.StatusID = statusID;
        //                            objRMAesn.rmaDetGUID = Convert.ToInt32(hdnrmaDetGuid.Value);
        //                            rmaEsnLookup.Add(objRMAesn);
        //                            // Disable controls

        //                        }
        //                    }
        //                }
        //            }

        //        }

        //    }

        //    if (isValidate)
        //    {
        //        avii.Classes.RMADetail objRMAesnblank = new avii.Classes.RMADetail();
        //        objRMAesnblank.ESN = "";
        //        objRMAesnblank.UPC = "";
        //        objRMAesnblank.StatusID = 1;
        //        objRMAesnblank.rmaDetGUID = 0;
        //        rmaEsnLookup.Add(objRMAesnblank);
        //        ViewState["rmadetailslist"] = rmaEsnLookup;
        //    }
        //    //avii.Classes.RMADetail objRMAesnblank = new avii.Classes.RMADetail();
        //    //objRMAesnblank.ESN = "";
        //    //objRMAesnblank.UPC = "";
        //    //objRMAesnblank.rmaDetGUID = 0;
        //    //rmaEsnLookup.Add(objRMAesnblank);

        //    //Dl_rma_detail.DataSource = rmaEsnLookup;
        //    //Dl_rma_detail.DataBind();
        //}
        private void GetRMAnRMADetailInfo(int rmaGUID, int userID)
        {

            List<RMADetail> rmaDetaillist = new List<RMADetail>();
            //List<RMAEsnLookUp> esnLookup = new List<RMAEsnLookUp>();
            RMADetail objRMADETAIL = new RMADetail();

            avii.Classes.RMA rmaInfo = RMAUtility.GetNewRMAInfo(rmaGUID, "", "", 0, -1, "");

            if (rmaInfo != null)
            {
                ViewState["companyID"] = rmaInfo.RMAUserCompany.CompanyID;
                txtAddress.Text = rmaInfo.Address;
                txtCity.Text = rmaInfo.City;
                txtCustName.Text = rmaInfo.RmaContactName;
                //txtState.Text = RmaInfo.State;
                dpState.SelectedValue = rmaInfo.State;
                txtZip.Text = rmaInfo.Zip;
                txtRmaNum.Text = rmaInfo.RmaNumber;
                txtRMADate.Text = Convert.ToDateTime(rmaInfo.RmaDate).ToShortDateString();
                //txtRemarks.Text = rmaInfo.Comment;
                ddlStatus.SelectedValue = rmaInfo.RmaStatusID.ToString();
                //txtAVComments.Text = rmaInfo.AVComments;
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                txtPhone.Text = rmaInfo.Phone;
                txtEmail.Text = rmaInfo.Email;
                //ViewState["rmadoc"] = RmaInfo.RmaDocument;
                //txtCustEmail.Text = RmaInfo.CustomerEmail;
                hdncompanyname.Value = rmaInfo.RMAUserCompany.CompanyName;
                if (userID > 0)
                {
                    if (rmaInfo.RmaStatusID > 1)
                    {
                        btnSubmitRMA.Enabled = false;
                    }
                }
            }

            //objRMADETAIL.rmaGUID = rmaGUID;
            //objRMADETAIL.rmaDetGUID = 0;
            //objRMADETAIL.ESN = string.Empty;
            //objRMADETAIL.Reason = string.Empty;
            //objRMADETAIL.StatusID = 1;
            //objRMADETAIL.CallTime = 0;
            //objRMADETAIL.Notes = string.Empty;
            //objRMADETAIL.ItemCode = string.Empty;
            //objRMADETAIL.UPC = string.Empty;
            //objRMADETAIL.Status = string.Empty;
            //RmaInfo.RmaDetails.Add(objRMADETAIL);

            //for (int i = 0; i < RmaInfo.RmaDetails.Count; i++)
            //{
            //    RMAEsnLookUp rmaobj = new RMAEsnLookUp();
            //    if (RmaInfo.RmaDetails[i].ESN != string.Empty && RmaInfo.RmaDetails[i].ESN != null)
            //    {

            //        rmaobj.ESN = RmaInfo.RmaDetails[i].ESN;
            //        rmaobj.UPC = RmaInfo.RmaDetails[i].UPC;
            //        rmaobj.CallTime = (int)RmaInfo.RmaDetails[i].CallTime;
            //        rmaobj.Notes = RmaInfo.RmaDetails[i].Notes;
            //        rmaobj.Reason = RmaInfo.RmaDetails[i].Reason;
            //        rmaobj.Po_Id = (int)RmaInfo.RmaDetails[i].Po_id;
            //        rmaobj.Pod_Id = (int)RmaInfo.RmaDetails[i].Pod_id;
            //        rmaobj.StatusID = (int)RmaInfo.RmaDetails[i].StatusID;
            //        rmaobj.rmaDetGUID = (int)RmaInfo.RmaDetails[i].rmaDetGUID;

            //        esnLookup.Add(rmaobj);
            //    }
            //}

            ViewState["rmadetailslist"] = rmaInfo.RmaDetails;// esnLookup;
            Session["rmaDocList"] = rmaInfo.RmaDocumentList;// esnLookup;
            if (rmaInfo.RmaDocumentList.Count > 0)
            {
                for (int i = 0; i < rmaInfo.RmaDocumentList.Count; i++)
                {
                    if (lblRmaDocs.Text == string.Empty)
                        lblRmaDocs.Text = rmaInfo.RmaDocumentList[i].RmaDocument;
                    else
                        lblRmaDocs.Text = lblRmaDocs.Text + ", " + rmaInfo.RmaDocumentList[i].RmaDocument;
                }
            }
            rptRmaItem.DataSource = rmaInfo.RmaDetails;
            rptRmaItem.DataBind();
            hdnRmaItemcount.Value = rptRmaItem.Items.Count.ToString();

            if (userID > 0)
            {
                //DropDownList ddl_Status1 = (DropDownList)Dl_rma_detail.Items[Dl_rma_detail.Items.Count - 1].FindControl("ddl_Status");
                //ddl_Status1.Visible = false;
                //hdnStatus.Value = "0";
            }
        }

        private void BindProductSKU(int companyID, string SKU)
        {
            //lblMsg.Text = string.Empty;
            //List<CompanySKUno> skuList = RMAUtility.GetProductSKUList(companyID, SKU);
            //if (skuList != null)
            //{
            //    ddlSKU.DataSource = skuList;
            //    ddlSKU.DataValueField = "SKU";
            //    ddlSKU.DataTextField = "SKU";

            //    ddlSKU.DataBind();

            //}
            //else
            //{
            //    ddlSKU.DataSource = null;
            //    ddlSKU.DataBind();
            //    //lblMsg.Text = "No SKU are assigned to select Customer";

            //}
            List<CompanySKUno> skuList = MslOperation.GetCompanySKUList(companyID, 2);
            if (skuList != null)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "SKU";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                //lblMsg.Text = "No SKU are assigned to select Customer";

            }
            ListItem newList = new ListItem("", "");
            ddlSKU.Items.Insert(0, newList);



        }
        protected void imgEditRMA_Commnad(object sender, CommandEventArgs e)
        {
            //int rmaGUID =  Convert.ToInt32(e.CommandArgument);
            lblAddMsg.Text = string.Empty;
            lblEditMsg.Text = string.Empty;
            ImageButton editImg = (ImageButton)sender;
            RepeaterItem item = (RepeaterItem)editImg.NamingContainer;
            int itemindex = item.ItemIndex;
            ViewState["itemindex"] = itemindex;
            List<RMADetail> esnList = (List<RMADetail>)ViewState["rmadetailslist"];
            if (esnList.Count >= itemindex && esnList[itemindex].rmaDetGUID > 0)
            {
                lblEsn.Text = esnList[itemindex].ESN;
                if (ViewState["companyID"] != null)
                {
                    int companyID = Convert.ToInt32(ViewState["companyID"]);

                    BindProductSKU(companyID, esnList[itemindex].ItemCode);

                }
                txtEditCallTime.Text = esnList[itemindex].CallTime.ToString();
                ddlEditReason.SelectedValue = esnList[itemindex].Reason.ToString();
                ddlDeviceCond.SelectedValue = esnList[itemindex].DeviceCondition.ToString();
                ddlDeviceDesig.SelectedValue = esnList[itemindex].DeviceDesignation.ToString();
                ddlDeviceState.SelectedValue = esnList[itemindex].DeviceState.ToString();
                txtDefect.Text = esnList[itemindex].DeviceDefect;
                ddlEditWarranty.SelectedValue = esnList[itemindex].Warranty.ToString();
                dpDisposition.SelectedValue = esnList[itemindex].Disposition.ToString();
                txtEditWarrantyDate.Text = (Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString() == "1/1/1901" ? string.Empty : Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString());//Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString();
                //esnList[itemindex].WarrantyExpieryDate.ToString();
                //txtRecievedOn.Text = esnList[itemindex].RecievedOn.ToString();

                txtReStockingDate.Text = (Convert.ToDateTime(esnList[itemindex].ReStockingDate).ToShortDateString() == "1/1/1901" ? string.Empty : Convert.ToDateTime(esnList[itemindex].ReStockingDate).ToShortDateString()); ;// Convert.ToDateTime(esnList[itemindex].ReStockingDate).ToShortDateString();
                txtLocationCode.Text = esnList[itemindex].LocationCode;
                
                //txtNewSKU.Text = esnList[itemindex].NewSKU;
                if (!string.IsNullOrEmpty(esnList[itemindex].NewSKU))
                    ddlSKU.SelectedValue = esnList[itemindex].NewSKU;

                txtEditNotes.Text = esnList[itemindex].Notes;
                txtReStockingFee.Text = esnList[itemindex].ReStockingFee.ToString();
                ddlEditStatus.SelectedValue = esnList[itemindex].StatusID.ToString();

                

                //ModalPopupExtender2.Show(); 
                RegisterStartupScript("jsUnblockDialog", "unblockEditDialog();");


            }
            else
                if (esnList.Count >= itemindex && esnList[itemindex].rmaDetGUID == 0)
                {
                    txtCallTime.Text = esnList[itemindex].CallTime.ToString();
                    ddlReason.SelectedValue = esnList[itemindex].Reason.ToString();
                    dpWarranty.SelectedValue = esnList[itemindex].Warranty.ToString();

                    txtWarranty.Text = (Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString() == "1/1/1901" ? string.Empty : Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString());
                        //Convert.ToDateTime(esnList[itemindex].WarrantyExpieryDate).ToShortDateString();//esnList[itemindex].WarrantyExpieryDate.ToString();
                    txtRecievedOn.Text = ( Convert.ToDateTime(esnList[itemindex].RecievedOn).ToShortDateString() =="1/1/1901"? string.Empty: Convert.ToDateTime(esnList[itemindex].RecievedOn).ToShortDateString()); 
                    //esnList[itemindex].RecievedOn.ToString();
                    //txtReStockingDate.Text = esnList[itemindex].ReStockingDate.ToString();
                    txtESN.Text = esnList[itemindex].ESN;
                    //txtNewSKU.Text = esnList[itemindex].NewSKU;
                    txtNotes.Text = esnList[itemindex].Notes;
                    ///txtReStockingFee.Text = esnList[itemindex].ReStockingFee.ToString();
                    ddlAddStatus.SelectedValue = esnList[itemindex].StatusID.ToString();
                    //ModalPopupExtender1.Show(); 
                    RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                }

            
        }
        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            GridDataBind();
        }
        private void TriggerClientGridRefresh()
        {
            string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
            RegisterStartupScript("jsGridRefresh", script);
        }
        protected void btnNewRma_Click(object sender, EventArgs e)
        {
            lblAddMsg.Text = string.Empty;
            txtESN.Text = string.Empty;
            dpWarranty.SelectedIndex = 0;
            txtCallTime.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtWarranty.Text = string.Empty;
            ddlReason.SelectedIndex = 0;
            ddlAddStatus.SelectedIndex = 0;
            txtRecievedOn.Text = DateTime.Now.ToShortDateString();
                
            ViewState["itemindex"] = null;
            RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
            //ModalPopupExtender1.Show();
        }
        
        protected void btnAddLineItem_Click(object sender, EventArgs e)
        {
            //string unassignedESN = "Unassigned ESN";
            lblAddMsg.Text = string.Empty;
            int companyID = 0;
            int rmaGUID = 0;
            int statusID = 1;
            double reStockingFee = 0;
            string esn = string.Empty;
            string maxesnmessage = "Esn Should be between 8 to 30 digits!";
            int itemindex = 0;
            string newSKU, locationCode, deviceDefect;
            newSKU = locationCode = deviceDefect = string.Empty;
            int deviceState, deviceDesignation, deviceCondition, warranty, disposition;
            deviceState = deviceDesignation = deviceCondition = warranty = disposition = 0;
            string externalESN = "External Esn";
            ViewState["duplicateESN"] = null;
            ViewState["ESNalreadyExists"] = null;
            ViewState["allowRMA"] = null;
            //ViewState["ExternalEsn"] = null;
            List<RMADetail> rmaEsnLookup = null;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            rmaEsnLookup = new List<RMADetail>();
            if (ViewState["rmadetailslist"] != null)
                rmaEsnLookup = (List<RMADetail>)ViewState["rmadetailslist"];

            
            esn = txtESN.Text.Trim();
            if (!string.IsNullOrEmpty(esn))
            {
                string esns = string.Empty;
                
                RMADetail objRMAesn = new avii.Classes.RMADetail();
                objRMAesn.ESN = esn;
                if (esn.Length < 8 || esn.Length > 30)
                {
                    lblAddMsg.Text = maxesnmessage;
                    return;
                }
                var EsnList = (from item in rmaEsnLookup where item.ESN.Equals(esn) select item).ToList();
                if (EsnList.Count > 0 && ViewState["itemindex"] == null)
                {
                    lblAddMsg.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                    //ModalPopupExtender1.Show();
                    //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                    return;
                }
                else
                    if (ViewState["itemindex"] != null)
                    {
                        
                        itemindex = Convert.ToInt32(ViewState["itemindex"]);
                        if (rmaEsnLookup[itemindex].ESN == esn && EsnList.Count > 1)
                        {
                            lblAddMsg.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                            //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                            return;
                        }
                        else
                            if (rmaEsnLookup[itemindex].ESN != esn && EsnList.Count > 0)
                            {
                                lblAddMsg.Text = string.Format("Duplicate ESN ({0}) is found", esn);
                                //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                                return;
                            }

                        //ModalPopupExtender1.Show();
                        
                    }
                ValidateESN(ref objRMAesn, companyID, rmaGUID);
                if (!objRMAesn.AllowDuplicate)
                {
                    lblAddMsg.Text = string.Format("RMA ({0}) already exists", esn);
                    ViewState["ESNalreadyExists"] = "1";
                    //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                    //ModalPopupExtender1.Show();
                    return;
                }

                //if (externalESN == objRMAesn.UPC && Session["adm"] == null)
                //{
                //    lblAddMsg.Text = "RMA is not allowed for External Esn";
                //    //ViewState["ExternalEsn"] = "1";
                //    //ModalPopupExtender1.Show();
                //    //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                //    return;
                //}
                if (externalESN == objRMAesn.ItemCode)
                {
                    lblAddMsg.Text = "RMA is not allowed for External Esn";


                    //ViewState["ExternalEsn"] = "1";
                    statusID = 31;
                    return;

                    //ModalPopupExtender1.Show();
                    //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                    //return;
                }
                
                
                if (!objRMAesn.AllowRMA && Session["adm"] == null)
                {
                    //if (ViewState["allowRMA"] == null)
                    //    ViewState["allowRMA"] = esn;
                    //else
                    //{
                    //    esns = ViewState["allowRMA"].ToString();

                    //    ViewState["allowRMA"] = esns + ", " + esn;
                    //}
                    //if (esns == string.Empty)
                    lblAddMsg.Text = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", esn, objRMAesn.UPC);
                    //else
                    //    lblAddMsg.Text = string.Format("RMA is not allowed for this item({1}) related to these ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
                    //ModalPopupExtender1.Show();
                    //RegisterStartupScript("jsUnblockDialog", "unblockAddDialog();");
                    return;
                }

                //List<avii.Classes.RMADetail> rmaDetailList = (List<avii.Classes.RMADetail>)Session["rmadetaillist"];

                
                //Hashtable hshEsnDuplicateCheck = new Hashtable();

                //txtUPC.Text = objRMAesn.UPC;
                // lblAvso.Text = objRMAesn.AVSalesOrderNumber;
                int callTime = 0;
                if (txtCallTime.Text != string.Empty)
                    callTime = Convert.ToInt32(txtCallTime.Text);
                if (dpWarranty.SelectedIndex > 0)
                    warranty = Convert.ToInt32(dpWarranty.SelectedValue);
                //deviceDefact = txtDefect.Text;
                if(!string.IsNullOrEmpty(txtReStockingFee.Text.Trim()))
                    reStockingFee = Convert.ToDouble(txtReStockingFee.Text);
                objRMAesn.CallTime = callTime;
                objRMAesn.Notes = txtNotes.Text;
                objRMAesn.Reason = ddlReason.SelectedValue;
                objRMAesn.StatusID = statusID;
                objRMAesn.rmaDetGUID = 0;
                objRMAesn.RecievedOn = (txtRecievedOn.Text.Trim().Length > 0 ? Convert.ToDateTime(txtRecievedOn.Text.Trim()) : DateTime.Now);
                objRMAesn.DeviceCondition = deviceCondition;
                objRMAesn.DeviceDefect = deviceDefect;
                objRMAesn.DeviceDesignation = deviceDesignation;
                objRMAesn.DeviceState = deviceState;
                objRMAesn.Disposition = disposition;
                objRMAesn.LocationCode = locationCode;
                objRMAesn.NewSKU = newSKU;
                objRMAesn.ReStockingDate = (txtReStockingDate.Text.Trim().Length > 0 ? Convert.ToDateTime(txtReStockingDate.Text.Trim()) : Convert.ToDateTime("01/01/1901")); //DateTime.Now;
                objRMAesn.ReStockingFee = reStockingFee;
                objRMAesn.Warranty = warranty;
                objRMAesn.WarrantyExpieryDate = (txtWarranty.Text.Trim().Length > 0 ? Convert.ToDateTime(txtWarranty.Text.Trim()) : Convert.ToDateTime("01/01/1901"));
                if (ViewState["itemindex"] == null)
                    rmaEsnLookup.Add(objRMAesn);
                else
                {
                    int itemIndex = Convert.ToInt32(ViewState["itemindex"]);
                    rmaEsnLookup[itemIndex] = objRMAesn;
                    
                }

                //if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
                //{

                //    foreach (avii.Classes.RMADetail redt in rmaEsnLookup)
                //    {
                //        if (hshEsnDuplicateCheck.ContainsKey(redt.ESN))
                //        {

                //            lblAddMsg.Text = string.Format("Duplicate ESN ({0}) is found", redt.ESN);
                //            ViewState["duplicateESN"] = "1";

                //           // break;
                //        }
                //        else
                //        {
                //            hshEsnDuplicateCheck.Add(redt.ESN, redt.ESN);
                //        }

                //    }
                //}

                //if (Convert.ToString(ViewState["duplicateESN"]) == "1")
                //{
                //    rmaEsnLookup = null;
                //    if (ViewState["rmadetailslist"] != null)
                //        rmaEsnLookup = (List<RMADetail>)ViewState["rmadetailslist"];
                //    ViewState["rmadetailslist"] = rmaEsnLookup;
            
                //    ModalPopupExtender1.Show();
                //}
                //else
                if (rmaEsnLookup.Count > 0)
                    btnSubmitRMA.Enabled = true;
                {
                    ViewState["rmadetailslist"] = rmaEsnLookup;
                    TriggerClientGridRefresh();
                    RegisterStartupScript("jsUnblockDialog", "closeAddDialog();");
                }
            }

        }
        private void GridDataBind()
        {
            if (ViewState["rmadetailslist"] != null)
            {
                List<RMADetail> rmaEsnLookup = (List<RMADetail>)ViewState["rmadetailslist"];

                rptRmaItem.DataSource = rmaEsnLookup;
                rptRmaItem.DataBind();
                hdnRmaItemcount.Value = rmaEsnLookup.Count.ToString();
            }
        }
        protected void rptRmaItem_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton imgEdit = (ImageButton)e.Item.FindControl("imgEdit");
                if (0 == Convert.ToInt32(imgEdit.CommandArgument))
                    imgEdit.OnClientClick = "openAddDialogAndBlock('Edit RMA Detail', '" + imgEdit.ClientID + "')";
                else
                    imgEdit.OnClientClick = "openEditDialogAndBlock('Edit RMA Detail', '" + imgEdit.ClientID + "')";

            }
        }
        protected void btnEditLineItem_Click(object sender, EventArgs e)
        {

            try
            {
                //ViewState["companyID"]
                //ViewState["rmaGUID"]
                lblEditMsg.Text = string.Empty;
                string sku = string.Empty;
                int companyID = 0;
                int recordCount = 0;
                if (ViewState["companyID"] != null)
                    companyID = Convert.ToInt32(ViewState["companyID"]);
                
                if (ViewState["itemindex"] != null)
                {
                    int itemindex = Convert.ToInt32(ViewState["itemindex"]);
                    int callTime = 0;
                    if (txtEditCallTime.Text != string.Empty)
                        callTime = Convert.ToInt32(txtEditCallTime.Text);
                    List<RMADetail> esnList = (List<RMADetail>)ViewState["rmadetailslist"];
                    if (esnList.Count >= itemindex && esnList[itemindex].rmaDetGUID > 0)
                    {
                        
                        esnList[itemindex].CallTime = callTime;

                        esnList[itemindex].Reason = ddlEditReason.SelectedValue;
                        esnList[itemindex].DeviceCondition = Convert.ToInt32(ddlDeviceCond.SelectedValue);
                        esnList[itemindex].DeviceDesignation = Convert.ToInt32(ddlDeviceDesig.SelectedValue);
                        esnList[itemindex].DeviceState = Convert.ToInt32(ddlDeviceState.SelectedValue);
                        esnList[itemindex].DeviceDefect = txtDefect.Text;
                        esnList[itemindex].Warranty = Convert.ToInt32(ddlEditWarranty.SelectedValue);
                        esnList[itemindex].Disposition = Convert.ToInt32(dpDisposition.SelectedValue);
                        esnList[itemindex].WarrantyExpieryDate = (txtEditWarrantyDate.Text.Trim().Length > 0 ? Convert.ToDateTime(txtEditWarrantyDate.Text.Trim()) : Convert.ToDateTime("01/01/1901"));
                        //txtRecievedOn.Text = esnList[itemindex].RecievedOn.ToString();
                        esnList[itemindex].ReStockingDate = (txtReStockingDate.Text.Trim().Length > 0 ? Convert.ToDateTime(txtReStockingDate.Text.Trim()) : Convert.ToDateTime("01/01/1901"));
                        esnList[itemindex].LocationCode = txtLocationCode.Text;
                        sku = ddlSKU.SelectedValue;// txtNewSKU.Text.Trim();
                        esnList[itemindex].NewSKU = sku;
                        esnList[itemindex].Notes = txtEditNotes.Text;
                        esnList[itemindex].ReStockingFee = Convert.ToDouble(txtReStockingFee.Text);
                        esnList[itemindex].StatusID = Convert.ToInt32(ddlEditStatus.SelectedValue);

                        if (!string.IsNullOrEmpty(sku))
                            RmaOperations.ValidateNewSkuAssignment(companyID, sku, out recordCount);

                        ///ModalPopupExtender2.Show();


                    }
                    ViewState["rmadetailslist"] = esnList;// esnLookup;

                    //rptRmaItem.DataSource = esnList;
                    //rptRmaItem.DataBind();
                    if (!string.IsNullOrEmpty(sku) && recordCount == 0)
                    {
                        lblEditMsg.Text = sku + " invalid SKU";
                        
                    }
                    else
                    {
                        TriggerClientGridRefresh();
                        RegisterStartupScript("jsUnblockDialog", "closeDialog()");
                    }
                }

            }
            catch (Exception ex)
            {
                lblEditMsg.Text = ex.Message;
            }
        }
        private static bool isEmail(string inputEmail)
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

        private bool ValidateRequiredColumns()
        {
            bool validated = false;

            if (txtCity.Text.Trim().Length > 0 && txtCustName.Text.Trim().Length > 0 && txtAddress.Text.Trim().Length > 0
                    && dpState.SelectedIndex > 0 && txtZip.Text.Trim().Length > 0 && txtRMADate.Text.Trim().Length > 0 && txtEmail.Text.Trim().Length > 0 && txtPhone.Text.Trim().Length > 0)
            {
                if (isEmail(txtEmail.Text.Trim()))
                {
                    if (txtEmail.Text.Trim().ToLower().IndexOf("langlobal.com") <= 0)
                        validated = true;
                }

            }

            return validated;
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
            RegisterStartupScript("jsUnblockDialog", "closeRmaDocDialog();");
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
                    if (doc != null && doc.Count>0)
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
                custFileName = txtRmaNum.Text + randomNo.ToString() + "C1" + extension;
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
                        custFileName = txtRmaNum.Text + randomNo.ToString() + "C1" + extension;
                        
                    }
                }
                rmaDocList.Add(rmaDocObjc);
            }
            if (fupRmaDocA1.HasFile)
            {
                RmaDocuments rmaDocObjA1 = new RmaDocuments();
                extension = Path.GetExtension(fupRmaDocA1.PostedFile.FileName);
                fileName1 = txtRmaNum.Text + randomNo.ToString() + "A1" + extension;
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
                        fupRmaDocc.PostedFile.SaveAs(imagePath1);
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
                        fileName1 = txtRmaNum.Text + randomNo.ToString() + "A1" + extension;
                        
                    }
                }
                rmaDocList.Add(rmaDocObjA1);

            }
            if (fupRmaDocA2.HasFile)
            {
                RmaDocuments rmaDocObjA2 = new RmaDocuments();
                
                extension = Path.GetExtension(fupRmaDocA2.PostedFile.FileName);
                fileName2 = txtRmaNum.Text + randomNo.ToString() + "A2" + extension;
                rmaDocObjA2.RmaDocID = rmaDocIDA2;
                
                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    imagePath2 = targetFolder + fileName2;
                    if (!File.Exists(Path.GetFullPath(imagePath2)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocc.PostedFile.SaveAs(imagePath2);
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
                        fileName2 = txtRmaNum.Text + randomNo.ToString() + "A2" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA2);

            }
            if (fupRmaDocA3.HasFile)
            {
                RmaDocuments rmaDocObjA3 = new RmaDocuments();
                
                extension = Path.GetExtension(fupRmaDocA3.PostedFile.FileName);
                fileName3 = txtRmaNum.Text + randomNo.ToString() + "A3" + extension;
                rmaDocObjA3.RmaDocID = rmaDocIDA3;
                
                int n = 1;
                for (int i = 0; i < n; i++)
                {

                    imagePath3 = targetFolder + fileName3;
                    if (!File.Exists(Path.GetFullPath(imagePath3)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocc.PostedFile.SaveAs(imagePath3);
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
                        fileName3 = txtRmaNum.Text + randomNo.ToString() + "A3" + extension;

                    }
                }
                rmaDocList.Add(rmaDocObjA3);
            }
            if (fupRmaDocA4.HasFile)
            {
                RmaDocuments rmaDocObjA4 = new RmaDocuments();
                
                extension = Path.GetExtension(fupRmaDocA4.PostedFile.FileName);
                fileName4 = txtRmaNum.Text + randomNo.ToString() + "A4" + extension;
                rmaDocObjA4.RmaDocID = rmaDocIDA4;
                
                int n = 1;
                for (int i = 0; i < n; i++)
                {
                    imagePath4 = targetFolder + fileName4;
                    if (!File.Exists(Path.GetFullPath(imagePath4)))
                    {
                        //Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        fupRmaDocc.PostedFile.SaveAs(imagePath4);
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
                        fileName4 = txtRmaNum.Text + randomNo.ToString() + "A4" + extension;

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
            string successMessageAdmin = "RMA is successfully updated.";
            string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>Aerovoice Returns Department</b>.";
            //string fileName = string.Empty;
            //string filePath = string.Empty;

            bool isEmail = false;
            List<RMADetail> rmaEsnLookup = new List<RMADetail>();
            rmaEsnLookup = (List<RMADetail>)ViewState["rmadetailslist"];

            string rmaNumber = string.Empty;
            lblConfirm.Text = string.Empty;
            bool isAdmin = false;
            int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
            if (!ValidateRequiredColumns())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('Please enter all required columns with correct data')</script>", false);
                return;
            }
            if (rmaEsnLookup!= null && rmaEsnLookup.Count > 0)
            {
                
                int rmaGUID = 0;
                int companyID = 0;
                string customerEmail = string.Empty;
                string overrideEmail = string.Empty;
                avii.Classes.RMA rmaInfoObj = new avii.Classes.RMA();
                List<CustomerEmail> custEmail = new List<CustomerEmail>();
                int rmastatus = 1;
                int userid = 0;
                bool adminStatusChange = false;
                string rma_status = "Pending";
                string dateMsg = "Invalid RMA Date! Can not create RMA before 120 days back.";
                string custName, address, city, state, zip, phone, email;
                DateTime currentDate = DateTime.Now;
                DateTime rmaDate = new DateTime();
                if (ViewState["ExternalEsn"] != null)
                {
                    //rmastatus = 31;
                    //ddlStatus.SelectedValue = "31";

                }
                //if (ViewState["rmaGUID"] != null)
                //    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                if (ViewState["companyID"] != null)
                    companyID = Convert.ToInt32(ViewState["companyID"]);
                if (ViewState["userID"] != null)
                    int.TryParse(ViewState["userID"].ToString(), out userid);
                int createdby = userid;
                int modifiedby = userid;
                if (ViewState["rmaGUID"] != null)
                {
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                    if (userid == 0)
                    {
                        rmastatus = Convert.ToInt32(ddlStatus.SelectedValue);
                        if (rmastatus == 0)
                            rmastatus = 1;
                        rma_status = ddlStatus.SelectedItem.Text;
                        isAdmin = true;
                        if (rma_status != lblStatus.Text    && !String.IsNullOrEmpty(rma_status) 
                                                            && !String.IsNullOrEmpty(lblStatus.Text))
                        {
                            adminStatusChange = true;
                        }
                    }
                }
                if (userid == 0)
                {
                    custEmail = CustomerEmailOperations.GetCustomerEmailList(companyID);
                    var userEmail = (from item in custEmail where item.ModuleGUID.Equals (75) select item).ToList();
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
                    if (Session["adm"] == null)
                    {
                        TimeSpan diffResult = currentDate - rmaDate;
                        if (diffResult.Days > 120)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + dateMsg + "')</script>", false);
                            return;
                        }
                    }
                    try
                    {
                        custName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
                        address = (txtAddress.Text.Trim().Length > 0 ? txtAddress.Text.Trim() : null);
                        city = (txtCity.Text.Trim().Length > 0 ? txtCity.Text.Trim() : null);
                        state = (dpState.SelectedIndex > 0 ? dpState.SelectedValue.Trim() : null);
                        zip = (txtZip.Text.Trim().Length > 0 ? txtZip.Text.Trim() : null);
                        phone = (txtPhone.Text.Trim().Length > 0 ? txtPhone.Text.Trim() : null);
                        email = (txtEmail.Text.Trim().Length > 0 ? txtEmail.Text.Trim() : null);
                        lblStatus.Text = (ddlStatus.SelectedIndex == 0 ? "Pending" : ddlStatus.SelectedItem.Text);
                        rmaInfoObj = RmaInfo(rmaGUID, rmastatus, modifiedby, createdby, userid, custName, address, city, state, zip, email, phone);
                        if (rmaInfoObj.RmaDetails.Count > maxEsn && Session["adm"] == null)
                        {
                            lbl_msg.Text = string.Format("Maximum of {0} ESNs are allowed per RMA request", maxEsn);
                        }
                        else if (rmaInfoObj != null && rmaInfoObj.RmaDetails != null && (rmaInfoObj.RmaDetails.Count > 0 && (rmaInfoObj.RmaDetails.Count <= maxEsn || Session["adm"] != null)))
                        {
                            RMAResponse rmaResponse = new RMAResponse();
                            rmaResponse = RMAUtility.Update_RMA_New(rmaInfoObj, companyID);
                            ViewState["rmadetailslist"] = null;
                            if (rmaResponse != null && string.IsNullOrEmpty(rmaResponse.ErrorCode))
                            {
                                rmaNumber = rmaResponse.RMANumber;

                                if (isEmail)
                                {
                                    if (rmaGUID == 0 || adminStatusChange)
                                    {
                                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                                        if (userInfo != null)
                                        {
                                            try
                                            {
                                                string emailAddress = txtEmail.Text.Trim();

                                                string emailAddr = clsGeneral.CustomerEmailAddress(userInfo, emailAddress, customerEmail, overrideEmail);

                                                string smsg = string.Empty;
                                                if (adminStatusChange)
                                                    smsg = string.Format("Thank you for submitting <b>RMA# {0}</b>  Dated: {1}, Your RMA has been processed and <b>Current Status is {2}</b>", rmaNumber, txtRMADate.Text, lblStatus.Text);
                                                else
                                                    smsg = string.Format("Thank you for submitting <b>RMA# {0}</b>  Dated: {1}, we shall be contacting you shortly. Please do not send us RMA Items until APPROVED <BR><BR><b>Current Status = {2}</b>", rmaNumber, txtRMADate.Text, lblStatus.Text);

                                                SendClientMail(smsg, emailAddr, userid);
                                            }
                                            catch (Exception ex)
                                            {
                                                lbl_msg.Text = ex.Message;
                                            }
                                        }

                                    }
                                }
                                
                            }

                            //Reset();
                            
                            //btnSubmitRMA.Enabled = false;
                            //btnValidate.Enabled = false;
                            hdnValidateESNs.Value = string.Empty;


                            if (rmaGUID > 0)
                            {
                                Response.Redirect("NewRMAQuery.aspx?search=rma", false);
                                
                            }
                            else
                            {
                                string msg = string.Empty;
                                if (Session["adm"] != null)
                                    msg = string.Format(successMessageAdmin, rmaNumber);
                                else
                                    msg = string.Format(successMessage, rmaNumber);
                                lblConfirm.Text = msg;
                                Session["msg"] = msg;
                                //if (Session["adm"] != null)
                                //    lblConfirm.Text = string.Format(successMessageAdmin, rmaNumber);
                                //else
                                //    lblConfirm.Text = string.Format(successMessage, rmaNumber);

                                btnSubmitRMA.Enabled = false;
                                rmaGUID = 0;
                                ViewState["rmaGUID"] = rmaGUID;
                                ViewState["validate"] = null;
                                GenerateRMA();
                                string url = Request.Url.AbsoluteUri + "&success=1";
                                url = "NewRMAForm.aspx?success=1";
                                Response.Redirect(url);
        
                            }
                        }
                        else
                        {
                            lblConfirm.Text = "RMA does not have ESN assigned.";
                        }
                    }
                    catch (Exception objExp)
                    {
                        //string innerMsg = objExp.Message + " " + objExp.InnerException.ToString();
                        //string url = Convert.ToString(HttpContext.Current.Request.UrlReferrer);
                        //string source = objExp.Source;

                        //if (HttpContext.Current.Session["userInfo"] != null)
                        //{
                        //    avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)HttpContext.Current.Session["userInfo"];
                        //    avii.Classes.CustomErrorOperation.InesrtIntoErrorLog(0, source, url, innerMsg, objUserInfo.UserGUID);
                        //}

                        lblConfirm.Text = objExp.Message.ToString();
                    }

                    

                }

                else
                    lblConfirm.Text = "Date Can't be empty";

                lblConfirm.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('RMA does not have ESN assigned.')</script>", false);
                    
            }

            
        }
        private avii.Classes.RMA RmaInfo(int rmaGUID, int rmastatus, int modifiedby, int createdby, int userid, string custName, string address, string city, string state, string zip, string email, string phone)
        {

            List<avii.Classes.RMADetail> rmaEsnLookup = new List<RMADetail>();
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>(); //RmaDocumentsList();
            //List<RmaDocuments> rmaDocList = RmaDocumentsList();
            rmaDocList = (List<RmaDocuments>)Session["rmaDocList"];

            //fill_esnList(ref rmaEsnLookup, false, rmaGUID);
            rmaEsnLookup = (List<avii.Classes.RMADetail>)ViewState["rmadetailslist"];
            if (chkApplyAll.Checked)
            {
                for (int i = 0; i < rmaEsnLookup.Count; i++)
                {
                    rmaEsnLookup[i].StatusID = rmastatus;
                }
            }
            else
            {
                if (rmaEsnLookup.Count == 1)
                {
                    rmaEsnLookup[0].StatusID = rmastatus;
                }

            }

            avii.Classes.RMA rmaInfoObj = new avii.Classes.RMA();
            rmaInfoObj.RmaGUID = rmaGUID;
            rmaInfoObj.RmaNumber = txtRmaNum.Text;
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
            rmaInfoObj.RmaDocument = string.Empty; ;
            rmaInfoObj.RmaDocumentList = rmaDocList;
            //rmaInfoObj.CustomerEmail = txtCustEmail.Text;
            rmaInfoObj.UserID = userid;
            //rmaInfoObj.PoNum = txtPo_num.Text;
            rmaInfoObj.Comment = txtRemarks.Text;
            //rmaInfoObj.Xml = RmaDetailXml;
            rmaInfoObj.RmaDetails = rmaEsnLookup;

            return rmaInfoObj;
        }
        private void SendClientMail(string smsg, string custEmail, int userID)
        {
            // string smsg = string.Empty, filename = string.Empty, subject = string.Empty;
            try
            {

                //smsg = string.Format("Thank you for submitting <b>RMA# {0}</b>  Dated: {1}, we shall be contacting you shortly. Please do not send us RMA Items until APPROVED \n\n<b>Current Status = {2}</b>", rmaNumber, rmaDate, status);
                if (custEmail.Length > 0)
                {
                    string rmaemail = string.Empty;
                    try
                    {
                        rmaemail = System.Configuration.ConfigurationSettings.AppSettings["rmaemail"];
                    }
                    catch { }
                    string subject = "Aerovoice RMA Department";
                    clsGeneral.SendEmail(custEmail, rmaemail, subject, smsg, userID, 1);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custEmail);




                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
            }
        }
        private void Reset()
        {
            lblRmaDocs.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            lbl_msg.Text = string.Empty;
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
            //txtState.Text = string.Empty;
            dpState.SelectedIndex = 0;
            txtZip.Text = string.Empty;

            if (userID == 0)
            {
                ddlCompany.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 14;

                if (rmaGUID == 0)
                {
                    companyID = 0;
                    ViewState["companyID"] = companyID;
                    hdncompanyname.Value = string.Empty;
                }
            }
            rptRmaItem.DataSource = null;
            rptRmaItem.DataBind();
            hdnRmaItemcount.Value = "0";
        }
        
        protected void btnCancelRMA_click(object sender, EventArgs e)
        { }

        protected void btnHistory_Click(object sender, EventArgs e)
        {

            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            //ReloadRmahistory(rmaGUID);
            RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
        }

        protected void btnRmaDoc_Click(object sender, EventArgs e)
        {

            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            //ReloadRmahistory(rmaGUID);
            RegisterStartupScript("jsUnblockDialog", "unblockRmaDocDialog();");
        }
        private void ReloadRmahistory(int rmaGUID)
        {
            Control tmp1 = LoadControl("../controls/RmaHistory.ascx");
            avii.Controls.RmaHistory ctlRmaHistory = tmp1 as avii.Controls.RmaHistory;
            phrHistory.Controls.Clear();
            if (tmp1 != null)
            {
                ctlRmaHistory.RmaGUID = rmaGUID;

                ctlRmaHistory.BindRmaHistory();
            }
            phrHistory.Controls.Add(ctlRmaHistory);

        }
        
        protected void btnBackRMAQuery_click(object sender, EventArgs e)
        {
            ViewState["rmadetailslist"] = null;
            Response.Redirect("NewRMAQuery.aspx?search=rma");
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            string company = string.Empty;
            ViewState["rmadetailslist"] = null;
            if (ViewState["company"] != null)
                company = ViewState["company"].ToString();
            
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
            ViewState["companyID"] = companyID;
            hdncompanyname.Value = ddlCompany.SelectedItem.Text;
            if (ViewState["esn"] != null)
            {
                BindRMADetail();
            }

            
        }

    }
}