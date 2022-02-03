using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Admin
{
    public partial class RMAForm : System.Web.UI.Page
    {
        private string successMessageAdmin = "RMA is successfully updated.";
        private string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>Aerovoice Returns Department</b>.";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //avii.Classes.RMAUtility rma_obj = new avii.Classes.RMAUtility();
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
                int companyID=0;
                int userID=0;
                int rmaGUID=0;
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
                    
                }
                else
                {
                    imgRma.Visible = false;
                    rmaGUID = 0;                   
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
                    if (Request["mode"] == "po")
                    {
                        POpanel.Visible = true;
                        esnPanel.Visible = false;
                        btnSubmitRMA.Enabled = false;
                        btnValidate.Enabled = false;

                    }
                    else if (Request["mode"] == "import")
                    {
                        btnpanel.Visible = false;
                        lblrmanumber.Visible = false;
                        txtRmaNum.Visible = false;
                        txtRMADate.Visible = false;
                        lblRMADate.Visible = false;
                        rmadtpanel.Visible = false;
                        // lblrmadate_md.Visible = false;
                        esnPanel.Visible = false;
                        POpanel.Visible = false;
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
                        esnPanel.Visible = true;
                        btnpanel.Visible = true;
                        lblCompany.Visible = false;
                        ddlCompany.Visible = false;
                        lblrmanumber.Visible = true;
                        txtRmaNum.Visible = true;
                        txtRMADate.Visible = true;
                        lblRMADate.Visible = true;
                        rmadtpanel.Visible = true;
                        // lblrmadate_md.Visible = true;
                        POpanel.Visible = false;
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
                            btnpanel.Visible = false;
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
                        esnPanel.Visible = false;
                        btnpanel.Visible = false;
                        lblCompany.Visible = true;
                        ddlCompany.Visible = true;

                        lblStatus.Visible = false;

                        POpanel.Visible = false;
                        if (Request["mode"] == "po")
                        {
                            POpanel.Visible = true;
                            esnPanel.Visible = false;
                            btnSubmitRMA.Enabled = false;
                            btnValidate.Enabled = false;
                            btnpanel.Visible = true;
                        }
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
                            objRMAcompany = RMAUtility.getRMAUserCompanyInfo(companyID, "", -1, -1);
                            hdncompanyname.Value = objRMAcompany.CompanyName;

                        }
                        catch (Exception ex)
                        {
                            lbl_msg.Text = ex.Message.ToString();
                        }
                    }
                }
                

                pnluploaddetail.Visible = false;
                if (rmaGUID > 0)
                {
                    
                    GetRMAnRMADetailInfo(rmaGUID, userID);
                    if (userID == 0)
                    {
                        bindCompany();

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
                    generateRMA();
                    chkApplyAll.Visible = false;
                    if (ViewState["esn"] != null)
                        Dl_esn_bind("");
                }

                //btnSubmitRMA.Enabled = false;
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

        //To obtain RMA and RMA Detail records while editing
        private void GetRMAnRMADetailInfo(int rmaGUID, int userID)
        {

            List<RMADetail> rmaDetaillist = new List<RMADetail>();
            //List<RMAEsnLookUp> esnLookup = new List<RMAEsnLookUp>();
            RMADetail objRMADETAIL = new RMADetail();

            avii.Classes.RMA RmaInfo = RMAUtility.getRMAInfo(rmaGUID, "", "",  0, -1, "");

            if (RmaInfo != null)
            {
                txtAddress.Text = RmaInfo.Address;
                txtCity.Text = RmaInfo.City;
                txtCustName.Text = RmaInfo.RmaContactName;
                //txtState.Text = RmaInfo.State;
                dpState.SelectedValue = RmaInfo.State;
                txtZip.Text = RmaInfo.Zip;
                txtRmaNum.Text = RmaInfo.RmaNumber;
                txtRMADate.Text = Convert.ToDateTime(RmaInfo.RmaDate).ToShortDateString();
                txtRemarks.Text = RmaInfo.Comment;
                ddlStatus.SelectedValue = RmaInfo.RmaStatusID.ToString();
                txtAVComments.Text = RmaInfo.AVComments;
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                txtPhone.Text = RmaInfo.Phone;
                txtEmail.Text = RmaInfo.Email;
                //txtCustEmail.Text = RmaInfo.CustomerEmail;
                hdncompanyname.Value = RmaInfo.RMAUserCompany.CompanyName;
                if (userID > 0)
                {
                    if (RmaInfo.RmaStatusID > 1)
                    {
                        btnSubmitRMA.Enabled = false;
                    }
                }
            }

            objRMADETAIL.rmaGUID = rmaGUID;
            objRMADETAIL.rmaDetGUID = 0;
            objRMADETAIL.ESN = string.Empty;
            objRMADETAIL.Reason = string.Empty;
            objRMADETAIL.StatusID = 1;
            objRMADETAIL.CallTime = 0;
            objRMADETAIL.Notes = string.Empty;
            objRMADETAIL.ItemCode = string.Empty;
            objRMADETAIL.UPC = string.Empty;
            objRMADETAIL.Status = string.Empty;
            RmaInfo.RmaDetails.Add(objRMADETAIL);

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

            ViewState["rmadetailslist"] = RmaInfo.RmaDetails;// esnLookup;

            Dl_rma_detail.DataSource = RmaInfo.RmaDetails;
            Dl_rma_detail.DataBind();
            hdnRmaItemcount.Value = Dl_rma_detail.Items.Count.ToString();
            
            if (userID > 0)
            {
                DropDownList ddl_Status1 = (DropDownList)Dl_rma_detail.Items[Dl_rma_detail.Items.Count - 1].FindControl("ddl_Status");
                ddl_Status1.Visible = false;
                //hdnStatus.Value = "0";
            }
        }

        protected void imgViewRMA_Click(object sender, EventArgs e)
        {
            lblHistory.Text = string.Empty;
            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            List<avii.Classes.RMA> rmaList = RMAUtility.GetRMAStatusReport(rmaGUID);
            Session["rmaList"] = rmaList;

            if (rmaList.Count > 0)
            {
                rptRma.DataSource = rmaList;
                rptRma.DataBind();
                mdlPopup2.Show();
            }
            else
            {
                lblHistory.Text = "No RMA history exists for this RMA";
                rptRma.DataSource = null;
                rptRma.DataBind();
                mdlPopup2.Show();

            }
        }
        //protected void imgViewRMAXML_Click(object sender, CommandEventArgs e)
        //{
            
        //    int rmaGUID = Convert.ToInt32(e.CommandArgument);
        //    List<avii.Classes.RMA> rmaList = null;
        //    if (Session["rmaList"] != null)
        //    {
        //        rmaList = (List<avii.Classes.RMA>)Session["rmaList"];
                
        //    }
        //    else
        //    {
        //        rmaList = RMAUtility.GetRMAStatusReport(rmaGUID);

        //    }
        //    var rmaXml = (from item in rmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
        //    if (rmaXml != null && rmaXml.Count > 0)
        //    {
        //        lblRmaXml.Text = rmaXml[0].AVComments;
        //    }
        //    mdlRmaXml.Show();

        //}
        //Bind State DropdownList
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

        //protected void btnUploadRMA_click(object sender, EventArgs e)
        //{
        //    int userID = 0;
        //    int esncount = 0;
        //    int rmaGUID = 0;
        //    string rmaNumber = string.Empty;
        //    int rmastatus = 1;
        //    int maxEsn=30;
        //    int companyID = 0;
        //    if (ViewState["companyID"] != null)
        //        companyID = Convert.ToInt32(ViewState["companyID"]);
        //    if (ViewState["userID"] != null)
        //        userID = Convert.ToInt32(ViewState["userID"]);
        //    if (ViewState["rmaGUID"] != null)
        //        rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
        //    string rmaGUIDs = string.Empty;
        //    string invalidesn = string.Empty;
        //    string externalesn = string.Empty;
        //    string Sessionid = Session.SessionID;
        //    string ponumber = string.Empty;
        //    string[] validFileExtensions = new string[] { "xls" };
        //    string fileDir;
        //    string filepath;
        //    string fileExtension;
        //    bool flag = false;
        //    DateTime dtime = DateTime.Now;
        //    DataTable rmaDT = new DataTable();
            
        //    //avii.Classes.RMA rmaInfoObj = new avii.Classes.RMA();
            
        //    lblExternalEsn.Text = string.Empty;
        //    lblInvalid_esn.Text = string.Empty;
        //    lbl_msg.Text = string.Empty;
        //    lblRMA.Text = string.Empty;
        //    try
        //    {

                
        //        if(ViewState["maxEsn"]!=null)
        //            maxEsn = Convert.ToInt32(ViewState["maxEsn"]);
        //        if (rmaupload.HasFile)
        //        {


        //            fileDir = Server.MapPath("~");
        //            filepath = fileDir + "uploads/" + rmaupload.FileName;
        //            fileExtension = System.IO.Path.GetExtension(rmaupload.FileName).Replace(".", string.Empty).ToLower();


        //            foreach (string extension in validFileExtensions)
        //            {
        //                if (fileExtension == extension)
        //                    flag = true;
        //            }
        //            if (flag == true)
        //            {

        //                try
        //                {
        //                    rmaupload.PostedFile.SaveAs(filepath);
        //                    DataTable dt = RMAUtility.getimportRMAList(filepath);
        //                    pnluploaddetail.Visible = true;
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        avii.Classes.RMA rmaInfoObj = null;
        //                        List<avii.Classes.RMADetail> rmaList = null;
        //                        for (int i = 0; i < dt.Rows.Count; i++)
        //                        {
        //                            if (dt.Rows[i]["esn"].ToString() != null && dt.Rows[i]["esn"].ToString() != string.Empty)
        //                            {
        //                                List<RMADetail> esnlist = RMAUtility.getRMAesn(companyID, dt.Rows[i]["esn"].ToString(), "", 0);

        //                                if (esnlist.Count > 0)
        //                                {
        //                                    if (esnlist[0].UPC != "Invalid Esn")
        //                                    {

        //                                        if (esnlist[0].UPC == "External Esn")
        //                                        {
        //                                            if (externalesn == string.Empty)
        //                                                externalesn = esnlist[0].ESN;
        //                                            else
        //                                                externalesn = externalesn + ", " + esnlist[0].ESN;

        //                                            lblExternalEsn.Text = externalesn;
        //                                        }

        //                                        try
        //                                        {
        //                                            avii.Classes.RMADetail rmaDetailObj = new avii.Classes.RMADetail();
        //                                            if (esnlist[0].ESN != string.Empty && esnlist[0].ESN != null)
        //                                            {
        //                                                if (maxEsn == esncount || i == 0)
        //                                                    rmaList = new List<avii.Classes.RMADetail>();

        //                                                rmaDetailObj.ESN = esnlist[0].ESN;

        //                                                rmaDetailObj.CallTime = 0;
        //                                                rmaDetailObj.Notes = string.Empty;
        //                                                rmaDetailObj.Reason = string.Empty;
        //                                                //rmaDetailObj.Po_id = esnlist[0].Po_Id;
        //                                                //rmaDetailObj.Pod_id = esnlist[0].Pod_Id;
        //                                                rmaDetailObj.StatusID = 1;
        //                                                rmaDetailObj.rmaDetGUID = 0;
        //                                                rmaDetailObj.rmaGUID = 0;
        //                                                rmaList.Add(rmaDetailObj);
        //                                                esncount++;
        //                                            }

        //                                            if (maxEsn == esncount)
        //                                            {
        //                                                esncount = 0;
        //                                                generateRMA();

        //                                                rmaInfoObj = new avii.Classes.RMA();
        //                                                rmaInfoObj.RmaGUID = 0;
        //                                                rmaInfoObj.RmaNumber = txtRmaNum.Text;
        //                                                rmaInfoObj.RmaDate = (txtRMADate.Text.Trim().Length > 0?Convert.ToDateTime(txtRMADate.Text.Trim()):DateTime.MinValue);
        //                                                rmaInfoObj.RmaStatusID = 1;
        //                                                rmaInfoObj.ModifiedBy = userID;
        //                                                rmaInfoObj.CreatedBy = userID;
        //                                                rmaInfoObj.RmaContactName = string.Empty;
        //                                                rmaInfoObj.Address = txtAddress.Text;
        //                                                rmaInfoObj.City = txtCity.Text;
        //                                                rmaInfoObj.State = txtState.Text;
        //                                                rmaInfoObj.Zip = txtZip.Text;

        //                                                //rmaInfoObj.CustomerEmail = txtCustEmail.Text;
        //                                                rmaInfoObj.UserID = userID;
        //                                                rmaInfoObj.PoNum = string.Empty; ;
        //                                                rmaInfoObj.Comment = string.Empty; 
        //                                                //rmaInfoObj.Xml = RmaDetailXml;
        //                                                rmaInfoObj.RmaDetails = rmaList;


        //                                                //rmaInfoObj = RmaInfo(0, rmastatus, userID, userID, userID, string.Empty,string.Empty,string.Empty,string.Empty, string.Empty, string.Empty);



        //                                                rmaDT = RMAUtility.update_RMA(rmaInfoObj);
        //                                                if (rmaDT.Rows.Count > 0)
        //                                                {
        //                                                    rmaGUID = Convert.ToInt32(rmaDT.Rows[0]["rma_guid"]);
        //                                                    rmaNumber = rmaDT.Rows[0]["rmanumber"] as string;
        //                                                }

        //                                                ViewState["rmaGUID"] = rmaGUID;
        //                                                if (lblRMA.Text == string.Empty)
        //                                                    lblRMA.Text = txtRmaNum.Text;
        //                                                else
        //                                                    lblRMA.Text = lblRMA.Text + ", " + txtRmaNum.Text;
        //                                            }


        //                                            //RMAUtility.update_RMA_DETAIL(0, rmaGUID, dt.Rows[i]["esn"].ToString(), "", 0, rmastatus, "", esnlist[0].Po_Id, esnlist[0].Pod_Id);
        //                                            lbl_msg.Text = "Uploaded Successfully";
                                                    

        //                                        }
        //                                        catch (Exception ex)
        //                                        {
        //                                            lblConfirm.Text = ex.Message.ToString();
        //                                        }

        //                                    }

        //                                    else
        //                                    {
        //                                        if (invalidesn == string.Empty)
        //                                            invalidesn = dt.Rows[i]["esn"].ToString();
        //                                        else
        //                                            invalidesn = invalidesn + ", " + dt.Rows[i]["esn"].ToString();
        //                                        lblInvalid_esn.Text = invalidesn;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        if (lblRMA.Text == string.Empty || lblRMA.Text == null)
        //                        {
        //                            lbl_msg.Text = "Can't upload! Please upload valid ESN";
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    lblConfirm.Text = ex.Message.ToString();
        //                }
        //            }
        //            else
        //            {
        //                lblConfirm.Text = "Invalid File Extension!";
        //                pnluploaddetail.Visible = false;
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        lblConfirm.Text = "File Data not in correct format!";
        //        pnluploaddetail.Visible = false;
        //    }
        //}
        //protected void btnCancelUpload_click(object sender, EventArgs e)
        //{
        //    int userID=0;
        //    if(ViewState["userID"]!=null) 
        //        userID = Convert.ToInt32(ViewState["userID"]);

        //    if (userID == 0)
        //    {
        //        ddlCompany.SelectedIndex = 0;
        //        ddlStatus.SelectedIndex = 1;
        //    }

        //    txtAVComments.Text = string.Empty;
        //    txtRemarks.Text = string.Empty;
        //    lblConfirm.Text = string.Empty;
        //    lblRMA.Text = string.Empty;
        //    lblExternalEsn.Text = string.Empty;
        //    lblInvalid_esn.Text = string.Empty;
        //    lbl_msg.Text = string.Empty;
        //    pnluploaddetail.Visible = false;
        //}
        //protected void RMA_po_button_click(object sender, EventArgs e)
        //{
        //    lblConfirm.Text = string.Empty;
        //    int companyID = 0;
        //    int userID = 0;
        //    int rmaGUID = 0;
        //    if (ViewState["companyID"] != null)
        //        companyID = Convert.ToInt32(ViewState["companyID"]);

        //    if (ViewState["userID"] != null)
        //        userID = Convert.ToInt32(ViewState["userID"]);
           
        //    if (txtPo_num.Text.Trim().Length > 0)
        //    {
        //        if (companyID == 0)
        //        {
        //            if (userID == 0)
        //            {

        //                if (ddlCompany.SelectedIndex == 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Company not selected!')</script>", false);
        //                    lbl_msg.Text = "Company not Selected!";
                            
        //                    return;
        //                }

        //                companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
        //                ViewState["companyID"] = companyID;

        //            }

        //        }

        //        Dl_esn_bind(txtPo_num.Text);
        //        if (Dl_rma_detail.Items.Count > 1)
        //        {
        //            btnSubmitRMA.Enabled = true;
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(0)</script>", false);
        //        }
        //        else
        //        {

        //            txtPo_num.Text = string.Empty;
        //        }

        //    }
        //    else
        //    {
        //        lblConfirm.Text = "Purchase Order# is missing";
        //    }

        //}

        //Bind RMA Detail Datalist
        private void Dl_esn_bind(string po_num)
        {
            
            int userID = 0;
            int companyID = 0;
            int dlindex=0;
            int rmaGUID = 0;
            
            if (ViewState["rmaGUID"] != null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
           
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            //RMAEsnLookUp objRMAesnblank = new RMAEsnLookUp();
            RMADetail objRMAesnblank = new RMADetail();
            lblConfirm.Text = string.Empty;
            try
            {
                List<avii.Classes.RMADetail> esnList = RMAUtility.getRMAesn(companyID, "", po_num, 0, rmaGUID, 0);
                //List<avii.Classes.RMADetail> esnList = new List<RMADetail>();
                if (esnList != null && esnList.Count > 0)
                {
                    if (esnList[0].PoStatusId != 3 && esnList[0].PoStatusId != 0)
                    {
                        lblConfirm.Text = "PO not shipped yet!";
                    }
                    if (po_num != "" && esnList.Count > 0)
                    {
                        if (esnList.Count == 1 && (esnList[0].UPC == "Invalid Esn" || esnList[0].UPC == "External Esn"))
                        {
                            lblConfirm.Text = "No Records Found";
                        }
                        else
                        {
                            objRMAesnblank.ESN = "";
                            objRMAesnblank.UPC = "";
                            objRMAesnblank.StatusID = 1;
                            objRMAesnblank.rmaDetGUID = 0;
                            esnList.Add(objRMAesnblank);
                        }

                    }
                    if (lblConfirm.Text != "PO not shipped yet!")
                    {
                        ViewState["rmadetailslist"] = esnList;
                        Dl_rma_detail.DataSource = esnList;
                        Dl_rma_detail.DataBind();
                        if (esnList.Count > 1)
                            esnPanel.Visible = true;

                    }
                    else
                    { 
                        
                    }

                    hdnRmaItemcount.Value = Dl_rma_detail.Items.Count.ToString();
                    dlindex = Dl_rma_detail.Items.Count;
                    if (po_num != "")
                    {
                        for (int i = 0; i < dlindex - 1; i++)
                        {
                            CheckBox chkESN2 = (CheckBox)Dl_rma_detail.Items[i].FindControl("chkESN");
                           DropDownList ddl_Status = (DropDownList)Dl_rma_detail.Items[i].FindControl("ddl_Status");

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
                        if (userID > 0)
                        {

                            if (dlindex > 0)
                            {
                                DropDownList ddl_Status1 = (DropDownList)Dl_rma_detail.Items[dlindex - 1].FindControl("ddl_Status");
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
                btnSubmitRMA.Enabled = false;
            }
            else
            {
                if (dlindex > 1)
                {
                    btnValidate.Enabled = true;
                    btnSubmitRMA.Enabled = true;
                }
            }

        }

        protected void btnCancelRMA_click(object sender, EventArgs e)
        {
            reset();
            generateRMA();
            //if (Request["mode"] == "po")
            btnSubmitRMA.Enabled = false;
            //else
            //    btnSubmitRMA.Enabled = true;
            lblConfirm.Text = string.Empty;
            

        }

        //Bind Company DropdownList
        private void bindCompany()
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
        private void reset()
        {
            lblConfirm.Text = string.Empty;
            lbl_msg.Text = string.Empty;
            ViewState["validate"] = null;
            ViewState["allowRMA"] = null;
            int companyID = 0;
            int userID = 0;
            int rmaGUID=0;
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
                ddlStatus.SelectedIndex = 1;

                if (rmaGUID == 0)
                {
                    companyID = 0;
                    ViewState["companyID"] = companyID;
                    hdncompanyname.Value = string.Empty;
                }
            }
            Dl_esn_bind("");
        }
       
        private bool validate_Company_Esn(string esn, string company, int poFlag)
        {
            bool errorReturn = false;
            int companyID = 0;
            int userID = 0;
            int rmaGUID=0;
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

        private List<RMADetail> fill_esnList(RMADetail rmaEsnLookup, int selectedItemIndex)
        {
           // List<RMAEsnLookUp> rmaEsnLookups = ViewState["rmadetailslist"] as List<RMAEsnLookUp>;
            List<RMADetail> rmaEsnLookups = ViewState["rmadetailslist"] as List<RMADetail>;
            if (rmaEsnLookups == null)
            {
                rmaEsnLookups = new List<RMADetail>();
            }

            if (rmaEsnLookups.Count == selectedItemIndex)
            {
                rmaEsnLookups.Add(rmaEsnLookup);
            }
            else
            {
                rmaEsnLookups[selectedItemIndex] = rmaEsnLookup;
            }

            return rmaEsnLookups;
        }

        private void fill_esnList(ref List<RMADetail> rmaEsnLookup, bool isValidate, int rmaGUID)
        {
            int statusID = 1;
            int companyID = 0;
            string esn = string.Empty;
            string externalESN = "External Esn";
            ViewState["duplicateESN"] = null;
            ViewState["ESNalreadyExists"] = null;
            ViewState["allowRMA"] = null;
            ViewState["ExternalEsn"] = null;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            Hashtable hshEsnDuplicateCheck = new Hashtable();
            if (rmaEsnLookup != null && rmaEsnLookup.Count > 0)
            {
              
                foreach (avii.Classes.RMADetail redt in rmaEsnLookup)
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
                            hshEsnDuplicateCheck.Add(redt.ESN,redt.ESN);
                        }

                        string upc, avso, Po_Num;
                        bool allowRMA, allowDuplicate;
                        allowRMA = redt.AllowRMA;
                        allowDuplicate = redt.AllowDuplicate;

                        ValidateESN(redt.ESN, companyID, rmaGUID, out upc, out avso, out Po_Num, out allowRMA, out allowDuplicate);
                        redt.UPC = upc;
                        redt.AVSalesOrderNumber = avso;
                        redt.PurchaseOrderNumber = Po_Num;
                        redt.AllowRMA = allowRMA;
                        redt.AllowDuplicate = allowDuplicate;
                        if (!allowRMA && Session["adm"] == null)
                        {
                            ViewState["allowRMA"] = redt.ESN;

                            lblConfirm.Text = string.Format("RMA is not allowed for this item related to ESN({0}).", redt.ESN);
                        }
                        if (!allowDuplicate)
                        {
                            lblConfirm.Text = string.Format("RMA ({0}) already exists", redt.ESN);
                            ViewState["ESNalreadyExists"] = "1";
                        }
                        if (chkApplyAll.Checked)
                        {
                            redt.StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                        }

                    }
                }

                //Dl_rma_detail.DataSource = rmaEsnLookup;
                //Dl_rma_detail.DataBind();
            }
            else
            {
                if (rmaEsnLookup == null)
                    rmaEsnLookup = new List<RMADetail>();

                avii.Classes.RMADetail objRMAesn = null;
                foreach (DataListItem i in Dl_rma_detail.Items)
                {
                    TextBox txtEsn = (TextBox)i.FindControl("txt_ESN");
                    if (txtEsn.Text.Trim().Length > 0)
                    {
                        CheckBox chkESN1 = (CheckBox)i.FindControl("chkESN");
                        if (chkESN1.Checked)
                        {
                            esn = string.Empty;
                            if (chkApplyAll.Checked)
                            {
                                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
                            }
                            else
                            {
                                DropDownList ddl_Status = (DropDownList)i.FindControl("ddl_Status");
                                if (ddl_Status.SelectedIndex > 0)
                                    statusID = Convert.ToInt32(ddl_Status.SelectedValue);
                            }

                            HiddenField hdnrmaDetGuid = (HiddenField)i.FindControl("hdnRMADetGUID");


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


                                TextBox txtCallTime = (TextBox)i.FindControl("txtCallTime");
                                Label txtUPC = (Label)i.FindControl("lblItemCode");
                                //Label lblAvso = (Label)i.FindControl("lblAvso");
                                TextBox txtNotes = (TextBox)i.FindControl("txtNotes");
                                DropDownList ddReason = (DropDownList)i.FindControl("ddReason");
                                //HiddenField hdnAllowRMA = i.FindControl("hdnAllowRMA") as HiddenField;
                                //HiddenField hdnAllowDuplicate = i.FindControl("hdnDuplicate") as HiddenField;
                                //if (!Convert.ToBoolean(hdnAllowRMA.Value))
                                //{
                                    
                                //}
                                //if(txtUPC.Text.Trim() == "RMA Exists")
                                //{
                                //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                //    ViewState["ESNalreadyExists"] = "1";
                                //}

                                if (esn.Trim() != string.Empty)
                                {
                                    if (txtCallTime.Text == "")
                                        txtCallTime.Text = "0";

                                    objRMAesn = new avii.Classes.RMADetail();
                                    objRMAesn.ESN = esn;
                                    //if (ViewState["validate"] == null)
                                    //{
                                    ValidateESN(ref objRMAesn, companyID, rmaGUID);
                                    //if (objRMAesn.UPC == "RMA Exists")
                                    //{
                                    //    lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                    //    ViewState["ESNalreadyExists"] = "1";
                                    //}
                                    if (!objRMAesn.AllowDuplicate)
                                    {
                                        lblConfirm.Text = string.Format("RMA ({0}) already exists", esn);
                                        ViewState["ESNalreadyExists"] = "1";
                                    }
                                    if (externalESN == objRMAesn.UPC && Session["adm"] == null)
                                    {
                                        lblConfirm.Text = "RMA is not allowed for External Esn";
                                        //ViewState["ExternalEsn"] = "1";
                                    }
                                    string esns = string.Empty;
                                    if (!objRMAesn.AllowRMA && Session["adm"] == null)
                                    {
                                        if (ViewState["allowRMA"] == null)
                                            ViewState["allowRMA"] = esn;
                                        else
                                        {
                                            esns = ViewState["allowRMA"].ToString();
                                            
                                            ViewState["allowRMA"] = esns + ", " + esn;
                                        }
                                        if (esns == string.Empty)
                                            lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
                                        else
                                            lblConfirm.Text = string.Format("RMA is not allowed for this item({1}) related to these ESN({0}).", ViewState["allowRMA"].ToString(), objRMAesn.UPC);
                                    }
                                    txtUPC.Text = objRMAesn.UPC;
                                    // lblAvso.Text = objRMAesn.AVSalesOrderNumber;
                                    //}
                                    objRMAesn.CallTime = Convert.ToInt32(txtCallTime.Text);
                                    objRMAesn.Notes = txtNotes.Text;
                                    objRMAesn.Reason = ddReason.SelectedValue;
                                    objRMAesn.StatusID = statusID;
                                    objRMAesn.rmaDetGUID = Convert.ToInt32(hdnrmaDetGuid.Value);
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
                avii.Classes.RMADetail objRMAesnblank = new avii.Classes.RMADetail();
                objRMAesnblank.ESN = "";
                objRMAesnblank.UPC = "";
                objRMAesnblank.StatusID = 1;
                objRMAesnblank.rmaDetGUID = 0;
                rmaEsnLookup.Add(objRMAesnblank);
                ViewState["rmadetailslist"] = rmaEsnLookup;
            }
            //avii.Classes.RMADetail objRMAesnblank = new avii.Classes.RMADetail();
            //objRMAesnblank.ESN = "";
            //objRMAesnblank.UPC = "";
            //objRMAesnblank.rmaDetGUID = 0;
            //rmaEsnLookup.Add(objRMAesnblank);

            Dl_rma_detail.DataSource = rmaEsnLookup;
            Dl_rma_detail.DataBind();
        }
        
        private bool check_DuplicateEsn(string txtEsn, int itemindex, int poFlag)
        {
            bool returnError = false;
            string esnmessage = "ESN already added to the RMA!";
            List<avii.Classes.RMADetail> esnList = (List<avii.Classes.RMADetail>)ViewState["rmadetailslist"];
            if (esnList != null)
            {
                foreach (RMADetail rmaesn in esnList)
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

        protected int add_BlankESN(string lblItemCode, int itemindex, DataListItem item, List<avii.Classes.RMADetail> rmaEsnLookup)
        {
            if (lblItemCode == "Invalid Esn" || hdnmsg.Value == "ESN already added to the RMA")
            {
                itemindex = item.ItemIndex;
            }
            else if (string.IsNullOrEmpty(rmaEsnLookup[rmaEsnLookup.Count -1].ESN))
                itemindex = item.ItemIndex;
            else
            {
                itemindex = item.ItemIndex + 1;

                avii.Classes.RMADetail objRMAesnblank = new avii.Classes.RMADetail();
                objRMAesnblank.ESN = "";
                objRMAesnblank.UPC = "";
               // objRMAesnblank.maker = "";
                objRMAesnblank.rmaDetGUID = 0;
                rmaEsnLookup.Add(objRMAesnblank);
            }

            return itemindex;
        }
        protected void check_Maxesn(int itemindex, List<avii.Classes.RMADetail> rmaEsnLookup, string lblItemCode)
        {
            int userID = 0;
            int maxEsn = 10;
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);
            if (ViewState["maxEsn"] != null)
                maxEsn = Convert.ToInt32(ViewState["maxEsn"]);
            if (itemindex <= maxEsn || Session["adm"] != null)
            {
                Dl_rma_detail.DataSource = rmaEsnLookup;
                Dl_rma_detail.DataBind();
                hdnRmaItemcount.Value = Dl_rma_detail.Items.Count.ToString();

                if (Dl_rma_detail.Items.Count == itemindex)
                {

                    TextBox txtEsn13 = (TextBox)Dl_rma_detail.Items[itemindex - 1].FindControl("txt_ESN");

                    txtEsn13.Focus();
                }
                if (userID > 0)
                {
                    DropDownList ddl_Status1 = (DropDownList)Dl_rma_detail.Items[Dl_rma_detail.Items.Count - 1].FindControl("ddl_Status");
                    ddl_Status1.Visible = false;
                    //hdnStatus.Value = "0";
                }

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Can not add more ESN')</script>", false);
                lblConfirm.Text = "Can't add more ESN";
                lblConfirm.Visible = true;
            }
            

        }

        protected void CallTime_TextChanged(object sender, EventArgs e)
        {
            int calltime = 0;
            List<avii.Classes.RMADetail> rmaEsnLookup = ViewState["rmadetailslist"] as List<avii.Classes.RMADetail>;
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
            List<avii.Classes.RMADetail> rmaEsnLookup = ViewState["rmadetailslist"] as List<avii.Classes.RMADetail>;
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

        protected void ESN_TextChanged(object sender, EventArgs e)
        {
            int poFlag = 0;
            int companyID = 0;
            
            //avii.Classes.RMAUtility rmaobj = new avii.Classes.RMAUtility();
            string company = string.Empty;
                //= ConfigurationManager.AppSettings["company"].ToString();
            if (ViewState["company"] != null)
                company = ViewState["company"].ToString();
           // string qstring;
            TextBox textEsn = (TextBox)sender;
            int rmaguid = 0;
            avii.Classes.RMADetail rmaEsn = new RMADetail();
            DataListItem item = (DataListItem)textEsn.NamingContainer;
            int itemindex = item.ItemIndex;
            List<avii.Classes.RMADetail> rmaEsnLookup = new List<avii.Classes.RMADetail>();
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            if (Request["mode"] == "po")
            {
                poFlag = 1;
            }

            if (int.TryParse(((HiddenField)item.FindControl("hdnRMADetGUID")).Value, out rmaguid))
                rmaEsn.rmaDetGUID = rmaguid;
            rmaEsn.ESN = ((TextBox)item.FindControl("txt_ESN")).Text;
  
            hdnmsg.Value = "";
            lblConfirm.Text = string.Empty;

            lbl_msg.Text = string.Empty;
            //company check and esn length check
            bool errorReturn = validate_Company_Esn(rmaEsn.ESN, company, poFlag);

            // check duplicate esn
            bool checkDuplicate = check_DuplicateEsn(rmaEsn.ESN, itemindex, poFlag);

            if (checkDuplicate)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('" + hdnmsg.Value + "');</script>", false);

            else if (errorReturn == false && checkDuplicate == false)
            {
                rmaEsnLookup = fill_esnList(rmaEsn, itemindex);
                // add blank row
                itemindex = add_BlankESN(rmaEsn.UPC, itemindex, item, rmaEsnLookup);

                if (itemindex < rmaEsnLookup.Count && itemindex > 0)
                {
                    //max esn check before bind
                    check_Maxesn(rmaEsnLookup.Count, rmaEsnLookup, rmaEsn.UPC);
                }
            }

            if (Request["mode"] == "esn" || Request["mode"] == "po" || hdncompanyname.Value == company)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(" + poFlag + ")</script>", false);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>disableSubmit('2')</script>", false);

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
                List<avii.Classes.RMADetail> rmaEsnList = new List<RMADetail>();
                fill_esnList(ref rmaEsnList, true, rmaGUID);
                ViewState["validate"] = "1";
                hdnValidateESNs.Value = "1";
                btnSubmitRMA.Enabled = true;
            }
            
            if (ViewState["companyID"] == null || (ViewState["companyID"] != null && string.IsNullOrEmpty(Convert.ToInt32(ViewState["companyID"]).ToString())) 
                    || (ddlCompany.Visible == true && ddlCompany.SelectedIndex == 0))
            {
                ViewState["validate"] = null;
                lbl_msg.Text = "Company is not selected";
                btnSubmitRMA.Enabled = false;
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


        private void ValidateESN(string esn, int companyID, int rmaGUID, out string UPC, out string AVSO, out string Po_Num, out bool allowRMA, out bool allowDuplicate)
        {
            //esn lookup
            UPC = string.Empty;
            AVSO = string.Empty;
            Po_Num = string.Empty;
            allowRMA = true;
            allowDuplicate = false;
            if (!string.IsNullOrEmpty(esn) && companyID > 0)
            {
                List<avii.Classes.RMADetail> esnlist = RMAUtility.getRMAesn(companyID, esn, "", 0, rmaGUID, 0);
                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        UPC = esnlist[0].UPC.ToString();
                        AVSO = esnlist[0].AVSalesOrderNumber;
                        Po_Num = esnlist[0].PurchaseOrderNumber;
                        allowRMA = esnlist[0].AllowRMA;
                        allowDuplicate = esnlist[0].AllowDuplicate;
                    }
                }
            }
        }


        private bool ValidateRequiredColumns()
        {
            bool validated = false;

            if (txtCity.Text.Trim().Length > 0 && txtCustName.Text.Trim().Length > 0 && txtAddress.Text.Trim().Length > 0
                    && dpState.SelectedIndex > 0 && txtZip.Text.Trim().Length > 0 && txtRMADate.Text.Trim().Length > 0 && txtEmail.Text.Trim().Length > 0 && txtPhone.Text.Trim().Length > 0)
            {
                if (isEmail(txtEmail.Text.Trim()))
                {
                    if (txtEmail.Text.Trim().ToLower().IndexOf("aerovoice.com") <= 0)
                        validated = true;
                }
                
            }

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

        protected void btnSubmitRMA_click(object sender, EventArgs e)
        {
            string rmaNumber = string.Empty;
            lblConfirm.Text = string.Empty;
            bool isAdmin = false;
            int maxEsn = Convert.ToInt32(ConfigurationManager.AppSettings["maxEsn"]);
            if (!ValidateRequiredColumns())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('Please enter all required columns with correct data')</script>", false);
                return; 
                 
            }

            if (hdnValidateESNs.Value != string.Empty)
                ViewState["validate"] = "1";

            if (ViewState["validate"] == null)
            {
                lblConfirm.Text = "Please validate the ESN records before submit";
                //btnSubmitRMA.Visible = false;
                btnSubmitRMA.Enabled = false;
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
                avii.Classes.RMA rmaInfoObj = new avii.Classes.RMA();
                //avii.Classes.RMAUserCompany objRMAcompany = new avii.Classes.RMAUserCompany();
                List<CustomerEmail> custEmail = new List<CustomerEmail>();
                int rmastatus = 1;
                int userid = 0;
                bool adminStatusChange = false;
                string rma_status = "Pending";
                string dateMsg = "Invalid RMA Date! Can not create RMA before 120 days back.";
                string custName, address, city, state, zip, phone, email;
                DateTime currentDate = DateTime.Now;
                DateTime rmaDate = new DateTime();
                //DataTable rmaDT = new DataTable();
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
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
                    //objRMAcompany = RMAUtility.getUserEmail(companyID);

                    if (userEmail != null && userEmail.Count > 0)
                    {
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

                        if (ViewState["duplicateESN"] != null && ViewState["duplicateESN"] == "1")
                        {
                            lbl_msg.Text = "Duplicate ESN(s) are found in the RMA Form";
                        }
                        else if (ViewState["ESNalreadyExists"] != null && ViewState["ESNalreadyExists"] == "1")
                        {
                            //lbl_msg.Text = ", RMA already exists";
                        }
                        else if (ViewState["ExternalEsn"] != null && ViewState["ExternalEsn"] == "1")
                        {
                            ////RMA not allowed for External Esn";
                        }

                        else if (ViewState["allowRMA"] != null && Session["adm"] == null)
                        {
                            //lbl_msg.Text = string.Format("RMA cannot be allowed for these ({0}) esn.", ViewState["allowRMA"].ToString());
                            //lblConfirm.Text = string.Empty;
                        }
                        else if (rmaInfoObj.RmaDetails.Count > maxEsn && Session["adm"] == null)
                        {
                            lbl_msg.Text = string.Format("Maximum of {0} ESNs are allowed per RMA request", maxEsn);
                        }
                        else if (rmaInfoObj != null && rmaInfoObj.RmaDetails != null && (rmaInfoObj.RmaDetails.Count > 0 && (rmaInfoObj.RmaDetails.Count <= maxEsn || Session["adm"] != null)))
                        {
                            RMAResponse rmaResponse = new RMAResponse();
                            rmaResponse = RMAUtility.Update_RMA(rmaInfoObj);

                            //if (rmaDT.Rows.Count > 0)
                            if (rmaResponse != null && string.IsNullOrEmpty(rmaResponse.ErrorCode))
                            {
                                //rmaNumber = rmaDT.Rows[0]["rmanumber"] as string;
                                rmaNumber = rmaResponse.RMANumber;

                                if (rmaGUID == 0 || adminStatusChange)
                                {
                                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                                    if (userInfo != null)
                                    {
                                        try
                                        {
                                            string emailAddr = "rma@aerovoice.com";
                                            if (!string.IsNullOrEmpty(userInfo.Email) && userInfo.UserType != "Aerovoice")
                                            {
                                                List<CustomerEmail> custEmails = userInfo.CustomerEmails;
                                                var emaiList = (from item in custEmails where item.ModuleGUID.Equals(75) select item).ToList();
                                                if (emaiList != null && emaiList.Count > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(emaiList[0].OverrideEmail))
                                                        emailAddr = emaiList[0].OverrideEmail;
                                                    else
                                                    {
                                                        if (!string.IsNullOrEmpty(emaiList[0].Email))
                                                            emailAddr = emaiList[0].Email + "," + txtEmail.Text.Trim();
                                                        else
                                                            emailAddr = emaiList[0].CompanyEmail + "," + txtEmail.Text.Trim();
                                                    }
                                                }
                                                else
                                                    emailAddr = userInfo.Email + "," + txtEmail.Text.Trim();
                                            }
                                            else if (!string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Aerovoice")
                                            {
                                                if (!string.IsNullOrEmpty(overrideEmail))
                                                    emailAddr = overrideEmail;
                                                else
                                                    emailAddr = customerEmail + "," + txtEmail.Text.Trim();
                                            }
                                            else if (string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Aerovoice")
                                                if (!string.IsNullOrEmpty(overrideEmail))
                                                    emailAddr = overrideEmail;


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
                                //rmaGUID = Convert.ToInt32(rmaDT.Rows[0]["rma_guid"]);
                            }

                            reset();
                            if (ViewState["esn"] == null)
                            {
                                Dl_rma_detail.DataSource = null;
                                Dl_rma_detail.DataBind();
                            }
                            btnSubmitRMA.Enabled = false;
                            btnValidate.Enabled = false;
                            hdnValidateESNs.Value = string.Empty;


                            if (rmaGUID > 0)
                            {
                                Response.Redirect("RMAQuery.aspx?search=rma", false);
                                //if (Session["adm"] != null)
                                //    lblConfirm.Text = string.Format(successMessageAdmin, rmaNumber);
                                //else
                                //    lblConfirm.Text = string.Format(successMessage, rmaNumber);
                                //btnSubmitRMA.Enabled = false;
                                //rmaGUID = 0;
                                //ViewState["rmaGUID"] = rmaGUID;
                                //ViewState["validate"] = null;
                                //generateRMA();
                            }
                            else
                            {
                                if (Session["adm"] != null)
                                    lblConfirm.Text = string.Format(successMessageAdmin, rmaNumber);
                                else
                                    lblConfirm.Text = string.Format(successMessage, rmaNumber);
                                btnSubmitRMA.Enabled = false;
                                rmaGUID = 0;
                                ViewState["rmaGUID"] = rmaGUID;
                                ViewState["validate"] = null;
                                generateRMA();
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

        private avii.Classes.RMA RmaInfo(int rmaGUID, int rmastatus, int modifiedby, int createdby, int userid, string custName, string address, string city, string state, string zip, string email, string phone)
        {
            
            List<avii.Classes.RMADetail> rmaEsnLookup = new List<RMADetail>();
            fill_esnList(ref rmaEsnLookup, false, rmaGUID);
            ViewState["rmadetailslist"] = rmaEsnLookup;

            avii.Classes.RMA rmaInfoObj = new avii.Classes.RMA();
            rmaInfoObj.RmaGUID = rmaGUID;
            rmaInfoObj.RmaNumber = txtRmaNum.Text;
            rmaInfoObj.RmaDate = (txtRMADate.Text.Trim().Length>0?Convert.ToDateTime(txtRMADate.Text.Trim()):DateTime.MinValue);
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

            //rmaInfoObj.CustomerEmail = txtCustEmail.Text;
            rmaInfoObj.UserID = userid;
            //rmaInfoObj.PoNum = txtPo_num.Text;
            rmaInfoObj.Comment = txtRemarks.Text;
            //rmaInfoObj.Xml = RmaDetailXml;
            rmaInfoObj.RmaDetails = rmaEsnLookup;
            
            return rmaInfoObj;
        }


        public string ShowRMADetailStatus()
        {
            int userID = 0;
            if (ViewState["userID"] != null)
                userID = Convert.ToInt32(ViewState["userID"]);

            if (userID == 0)
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
            //int rmaGUID=0;
            //int rmadrtGUID;
            //string reasonExp = string.Empty;
            //string reasonvalue = string.Empty;
            
            ////avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            //string company = string.Empty;
            //    //= ConfigurationManager.AppSettings["company"].ToString();
            //if (ViewState["company"] != null)
            //    company = ViewState["company"].ToString();
           
            //if (ViewState["userID"] != null )
            //    userID = Convert.ToInt32(ViewState["userID"]);
            //if (ViewState["companyID"] != null )
            //    companyID = Convert.ToInt32(ViewState["companyID"]);
            //if (ViewState["rmaGUID"] != null)
            //    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

            //avii.Classes.RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(companyID, "", 1, -1);
            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    Label lblStatusheader = (Label)e.Item.FindControl("lblStatusheader");
            //    if (userID > 0)
            //    {
            //        lblStatusheader.Visible = false;
            //    }
            //    else
            //        lblStatusheader.Visible = true;
            //}

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddReason = (DropDownList)e.Item.FindControl("ddReason");

                HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
                HiddenField hdnReason = (HiddenField)e.Item.FindControl("hdnReason");
                DropDownList ddl_RmaItemStatus = (DropDownList)e.Item.FindControl("ddl_Status");

                ddl_RmaItemStatus.SelectedValue = hdnStatus.Value;
                ddReason.SelectedValue = hdnReason.Value;

                //if (userID == 0)
                //{
                //    ddl_RmaItemStatus.Visible = true;
                //}
                //else
                //{
                //    ddl_RmaItemStatus.Visible = false;
                //    //hdnStatus.Value = "0";
                //}
                //TextBox txtCallTime = (TextBox)e.Item.FindControl("txtCallTime");
                //TextBox txtNotes = (TextBox)e.Item.FindControl("txtNotes");
            }

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
                    string subject ="Aerovoice RMA Department";
                    clsGeneral.SendEmail(custEmail, rmaemail, subject, smsg, userID, 1);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custEmail);

                    


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:"  + ex.Message);
            }
        }
        protected void btnBackRMAQuery_click(object sender, EventArgs e)
        {
            Response.Redirect("RMAQuery.aspx?search=rma");
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            //string company = ConfigurationManager.AppSettings["company"].ToString();
            string company = string.Empty;
            if(ViewState["company"]!=null)
                 company = ViewState["company"].ToString();
            //avii.Classes.RMAUtility rma_obj = new avii.Classes.RMAUtility();
            //avii.Classes.RMAUserCompany objrmaUserCompany = null;

            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
            ViewState["companyID"] = companyID;
            hdncompanyname.Value = ddlCompany.SelectedItem.Text;
            //if (Request["mode"] == "esn")
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>hideEsn(0)</script>", false);

            if (Request["mode"] == "esn")
            {
                Dl_esn_bind("");
            }

            // companyID= Convert.ToInt32(ddlCompany.SelectedValue.Trim());

            //objrmaUserCompany = RMAUtility.getUserEmail(companyID);
            //txtCustEmail.Text = objrmaUserCompany.Email;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

