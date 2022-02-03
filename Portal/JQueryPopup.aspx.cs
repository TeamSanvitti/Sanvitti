using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class JQueryPopup : System.Web.UI.Page
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

            //rebind = false;
            if (!IsPostBack)
            {
                int userid = 0;
                int companyID = 0;
                //rebind = false;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        ViewState["companyID"] = userInfo.CompanyGUID;
                        userid = userInfo.UserGUID;
                    }
                    //userid = Convert.ToInt32(Session["UserID"]);
                }
                ViewState["userid"] = userid;
                if (userid > 0)
                {

                    hdnUserID.Value = userid.ToString();
                    companyPanel.Visible = false;
                    lblComapny.Visible = false;
                }
                else
                {
                    companyPanel.Visible = true;
                    lblComapny.Visible = true;
                    bindCompany();
                }

                ddReason.DataSource = RMAUtility.getReasonHashtable();
                ddReason.DataTextField = "value";
                ddReason.DataValueField = "key";
                ddReason.DataBind();
                ddReason.Items.Insert(0, new ListItem(string.Empty));
                pnlGrid.Visible = false;
                if (Request["search"] != null && Request["search"] != "")
                {
                    //rebind = false;

                    bindsearchRMA(userid);
                }
                //BindRMA(userid);

            }

        }
        //Maintain search criteria for back to search
        protected void bindsearchRMA(int userid)
        {

            int companyID = -1;
            string searchCriteria;
            string[] searchArr;



            if (Session["searchRma"] != null)
            {
                searchCriteria = (string)Session["searchRma"];
                searchArr = searchCriteria.Split('~');
                companyID = Convert.ToInt32(searchArr[0]);
                rmanumber.Text = searchArr[1].ToString();
                txtRMADate.Text = searchArr[2].ToString();
                ddlStatus.SelectedValue = searchArr[3].ToString();
                txtUPC.Text = searchArr[4].ToString();
                txtESNSearch.Text = searchArr[5].ToString();
                txtRMADateTo.Text = searchArr[6].ToString();
                txtAVSO.Text = searchArr[7].ToString();
                txtPONum.Text = searchArr[8].ToString();
                ddReason.SelectedValue = searchArr[9].ToString();





                if (userid == 0)
                    ddlCompany.SelectedValue = searchArr[0].ToString();

                BindRMASearch(true);
                //if (userid > 0)
                //{
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                //}
                //else
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }
            else
            {
                lblMsg.Text = "Session Expire!";
            }

        }
        //Maintain search criteria from dashboard
        protected void BindRMA(int userid)
        {

            string statusID = "0";




            if (Session["rmastatus"] != null)
            {
                int days = 30;
                int companyID = 0;
                if (Session["days"] != null)
                {
                    days = Convert.ToInt32(Session["days"]);
                }
                if (Session["adm"] != null)
                {
                    if (Session["cid"] != null)
                        companyID = Convert.ToInt32(Session["cid"]);
                    if (companyID > 0)
                        ddlCompany.SelectedValue = companyID.ToString();
                    Session["cid"] = null;
                }
                statusID = Convert.ToString(Session["rmastatus"]);
                DateTime today = DateTime.Today;
                DateTime rmaDate = today.AddDays(-days);
                txtRMADate.Text = rmaDate.ToShortDateString();
                ddlStatus.SelectedValue = statusID;

                BindRMASearch(true);
                Session["rmastatus"] = null;
                Session["days"] = null;


            }

        }

        //Bind Company DopdownList
        protected void bindCompany()
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            ddlCompany.DataSource = RMAUtility.getRMAUserCompany(-1, "", -1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ListItem item = new ListItem("", "0");
            ddlCompany.Items.Insert(0, item);
        }
        protected void search_click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            //UpdatePanel2.Update();
            //radGridRmaDetails.MasterTableView.Rebind();

            BindRMASearch(true);
        }



        //Bind Grid with RMA and RMA Details based on the search
        protected void BindRMASearch(bool rebind)
        {
            int companyID = -1;
            int userid = 0;
            string Status = "-1";
            string RmaNumber, rmaDate, rmaDateTo, UPC, esn, avso, poNum, returnReason;
            string searchCriteria;
            bool validForm = true;
            DateTime rma_Date, rmaDtTo;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            try
            {
                if (ddlStatus.SelectedIndex > 0)
                    Status = ddlStatus.SelectedValue;
                //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
                if (userid > 0)
                {
                    avii.Classes.RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(-1, "", -1, userid);
                    companyID = objRMAcompany.CompanyID;
                }
                else
                {
                    if (ddlCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                }


                rmaDate = rmaDateTo = null;
                RmaNumber = (rmanumber.Text.Trim().Length > 0 ? rmanumber.Text.Trim() : null);
                UPC = (txtUPC.Text.Trim().Length > 0 ? txtUPC.Text.Trim() : string.Empty);
                esn = (txtESNSearch.Text.Trim().Length > 0 ? txtESNSearch.Text.Trim() : string.Empty);
                avso = (txtAVSO.Text.Trim().Length > 0 ? txtAVSO.Text.Trim() : string.Empty);
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : string.Empty);
                returnReason = ddReason.SelectedValue;

                if (txtRMADate.Text.Trim().Length > 0)
                {
                    if (DateTime.TryParse(txtRMADate.Text, out rma_Date))
                        rmaDate = rma_Date.ToShortDateString();
                    else
                        throw new Exception("RMA Date does not have correct format(MM/DD/YYYY)");
                }
                if (txtRMADateTo.Text.Trim().Length > 0)
                {

                    if (DateTime.TryParse(txtRMADateTo.Text, out rmaDtTo))
                        rmaDateTo = rmaDtTo.ToShortDateString();
                    else
                        throw new Exception("RMA Date does not have correct format(MM/DD/YYYY)");
                }

                if (userid > 0)
                {
                    if (string.IsNullOrEmpty(UPC) && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(RmaNumber) && string.IsNullOrEmpty(rmaDate) && ddlStatus.SelectedIndex == 0)
                    {
                        validForm = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(UPC) && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(RmaNumber) && string.IsNullOrEmpty(rmaDate) && string.IsNullOrEmpty(rmaDateTo) && ddlStatus.SelectedIndex == 0
                            && ddlCompany.SelectedIndex == 0 && string.IsNullOrEmpty(avso) && string.IsNullOrEmpty(poNum) && ddReason.SelectedIndex == 0)
                    {
                        validForm = false;
                    }
                }
                if (validForm)
                {
                    List<avii.Classes.RMA> objRmaList = null;
                    if (rebind)//(Context.Items["result"] != null)
                    {
                        objRmaList = RMAUtility.GetRMAList(0, rmanumber.Text, txtRMADate.Text, txtRMADateTo.Text, Convert.ToInt32(Status), companyID, "", UPC, esn, avso, poNum, returnReason);
                        Session["result"] = objRmaList;


                    }
                    else
                    {
                        objRmaList = Session["result"] as List<avii.Classes.RMA>;
                    }

                    if (objRmaList != null && objRmaList.Count > 0)
                    {
                        lblCount.Text = "<strong>Total count:</strong> " + objRmaList.Count.ToString();
                        btnRMAReport.Visible = true;
                        searchCriteria = companyID + "~" + rmanumber.Text + "~" + txtRMADate.Text + "~" + Status + "~" + UPC + "~" + esn + "~" + this.txtRMADateTo.Text + "~" + avso + "~" + poNum + "~" + returnReason;
                        //radGridRmaDetails.DataSource = objRmaList;
                        //radGridRmaDetails.DataBind();
                        Session["searchRma"] = searchCriteria;
                        //if (radGridRmaDetails.MasterTableView.Items.Count > 0)
                        {


                            // radGridRmaDetails.Visible = true;
                            //btnExport.Visible = true;
                            hlkRMASummary.Visible = true;
                            if (companyID > 0)
                                hlkRMASummary.NavigateUrl = "/rma/rmaSummary.aspx?c" + companyID.ToString();
                        }
                        gvRma.DataSource = objRmaList;
                        gvRma.DataBind();
                        //radGridRmaDetails.Visible = true;
                        //btnExport.Visible = true;
                        pnlGrid.Visible = true;
                    }
                    else
                    {
                        lblCount.Text = string.Empty;
                        pnlGrid.Visible = false;
                        btnRMAReport.Visible = false;
                        //radGridRmaDetails.DataSource = null;
                        //radGridRmaDetails.Visible = false;
                        //btnExport.Visible = false;
                        hlkRMASummary.Visible = false;


                        lblMsg.Text = "No matching record exists for selected search criteria";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    //radGridRmaDetails.Visible = false;
                    //btnExport.Visible = false;
                    lblCount.Text = string.Empty;
                }

                if (userid > 0)
                {
                    //radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                // else
                //radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        public void BindRMA(int rmaGUID, bool rmaQuery)
        {
            List<avii.Classes.RMA> objRmaList = null;
            if (rmaQuery)
            {
                objRmaList = Session["result"] as List<avii.Classes.RMA>;

                if (objRmaList != null)
                {
                    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                    if (rmaInfoList != null && rmaInfoList.Count > 0)
                    {
                        lblRMA.Text = rmaInfoList[0].RmaNumber;
                        lblStatus.Text = rmaInfoList[0].Status;
                        lblComment.Text = rmaInfoList[0].Comment;
                        lblAVComment.Text = rmaInfoList[0].AVComments;
                        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();



                        gvRMADetail.DataSource = rmaInfoList[0].RmaDetails;
                        gvRMADetail.DataBind();
                    }

                }
            }
            else
            {
                avii.Classes.RMA objRma = null;

              //  objRma = avii.Classes.RMAUtility.GetRMADetails(rmaGUID);
                if (objRma != null)
                {
                    lblRMA.Text = objRma.RmaNumber;
                    lblStatus.Text = objRma.Status;
                    lblComment.Text = objRma.Comment;
                    lblAVComment.Text = objRma.AVComments;
                    lblCompanyName.Text = objRma.RMAUserCompany.CompanyName;
                    lblRMADate.Text = Convert.ToDateTime(objRma.RmaDate).ToShortDateString();



                    gvRMADetail.DataSource = objRma.RmaDetails;
                    gvRMADetail.DataBind();
                }
            }


        }
        protected void gvRMADetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblreason = (Label)e.Row.FindControl("lblreason");
                HiddenField hdnReason = (HiddenField)e.Row.FindControl("hdnReason");
                try
                {
                    if (hdnReason.Value != null && hdnReason.Value != string.Empty && hdnReason.Value != "0")
                    {
                        Hashtable reasonHashtable = avii.Classes.RMAUtility.getReasonHashtable();

                        lblreason.Text = reasonHashtable[hdnReason.Value].ToString();
                    }
                }
                catch (Exception ex)
                {
                    //lblMsg.Text = ex.Message.ToString();
                }
            }
        }
        protected void Edit_click(object sender, CommandEventArgs e)
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                if (e.CommandArgument != null)
                {

                    int rmaguid = Convert.ToInt32(e.CommandArgument);
                    string url = string.Empty;
                    int userid = 0;
                    if (ViewState["userid"] != null)
                        Int32.TryParse(ViewState["userid"].ToString(), out userid);
                    if (userid > 0)
                        url = "RMAForm.aspx?rmaGUID=" + rmaguid;
                    else
                    {

                        //List<avii.Classes.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
                        //if (rmaUserCompany.Count > 0)
                        url = "RMAForm.aspx?rmaGUID=" + rmaguid + "&companyID=" + hdnCompanyId.Value;
                    }

                    Response.Redirect(url);
                }
                else
                    lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void imgViewRmaDetails_Click(object sender, CommandEventArgs e)
        {
            //int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["rmaGUID"] = rmaGUID;

            //List<avii.Classes.RMADetail> rmaDetailList = RMAUtility.GetRMADetailNew(rmaGUID, -1, string.Empty);//(List<avii.Classes.RMADetail>)Session["rmadetaillist"];

            //if (rmaDetailList.Count > 0)
            //{
            //    rptRmaItem.DataSource = rmaDetailList;
            //    rptRmaItem.DataBind();
            //    mdlRmaDetail.Show();
            //}
            //else
            //{
            //    rptRmaItem.DataSource = null;
            //    rptRmaItem.DataBind();
            //    lblMsg.Text = "No RMA found";

            //}
        }
        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
           // this.EditCustomerID = null;

           // ClearEditCustomerForm();	//In case using non-modal
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "jsUnblockDialog", "unblockDialog();", false);
            //ClientScript.RegisterStartupScript(this.GetType(), "jsUnblockDialog", "unblockDialog();");
                    
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void gvRma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgView = (ImageButton)e.Row.FindControl("imgView");
            imgView.OnClientClick = "openDialogAndBlock('View RMA', '" + imgView.ClientID + "')";
            ImageButton imgHistory = (ImageButton)e.Row.FindControl("imgHistory");
            imgHistory.OnClientClick = "openHistoryDialogAndBlock('View RMA History', '" + imgHistory.ClientID + "')";
        }

        protected void gvRma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rmaGUID = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
                case "ViewRma":
                    //Customer customer = _customerService.GetByID(customerID);
                    //FillEditCustomerForm(customer);

                    //this.EditCustomerID = customerID;
                    
                    ViewState["rmaGUID"] = rmaGUID;
                    //ImageButton editImg = (ImageButton)sender;
                    //GridViewRow row = (GridViewRow)editImg.NamingContainer;
                    //int itemindex = row.RowIndex;

                    ViewState["itemindex"] = 0;
                    //if (tmp != null)
                    //{

                    //    ctlRMADetail.BindRMA(rmaGUID, false);
                    //}
                    //pnlRMA.Controls.Add(ctlRMADetail);
                    BindRMA(rmaGUID, false);
                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                    //mdlPopup.Show();

                    //ClientScript.RegisterStartupScript(this.GetType(), "jsUnblockDialog", "unblockDialog();");
                    
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "jsUnblockDialog", "unblockDialog();", false);
				


                    //Setting timer to test longer loading
                    //Thread.Sleep(2000);
                    break;

                //case "DeleteCustomer":
                //    _customerService.Delete(customerID);

                //    GridDataBind();
                //    break;
            }
        }

        protected void imgViewRMA_Click(object sender, CommandEventArgs e)
        {
            //UpdatePanel1.Update();
            //Control tmp = LoadControl("~/controls/RMADetails.ascx");
            //avii.Controls.RMADetails ctlRMADetail = tmp as avii.Controls.RMADetails;
            //pnlRMA.Controls.Clear();
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            ViewState["rmaGUID"] = rmaGUID;
            ImageButton editImg = (ImageButton)sender;
            GridViewRow row = (GridViewRow)editImg.NamingContainer;
            int itemindex = row.RowIndex;

            ViewState["itemindex"] = itemindex;
            //if (tmp != null)
            //{

            //    ctlRMADetail.BindRMA(rmaGUID, false);
            //}
            //pnlRMA.Controls.Add(ctlRMADetail);
            BindRMA(rmaGUID, false);
            //mdlPopup.Show();
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsUnblockDialog", "unblockDialog();", false);
				

            //int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //List<avii.Classes.RMA> rmaList = RMAUtility.GetRMAStatusReport(rmaGUID);
            //if (rmaList.Count > 0)
            //{
            //    rptRma.DataSource = rmaList;
            //    rptRma.DataBind();
            //    mdlPopup2.Show();
            //}
            //else
            //    lblMsg.Text = "No records found";


        }
        protected void imgRMAHistory_Click(object sender, CommandEventArgs e)
        {
            //uPnl3.Update();
            //Control tmp = LoadControl("~/controls/RMADetails.ascx");
            //avii.Controls.RMADetails ctlRMADetail = tmp as avii.Controls.RMADetails;
            //pnlRMA.Controls.Clear();
            //int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //// ViewState["rmaGUID"] = rmaGUID;
            //if (tmp != null)
            //{

            //    ctlRMADetail.BindRMA(rmaGUID, false);
            //}
            //pnlRMA.Controls.Add(ctlRMADetail);
            //mdlPopup.Show();
            lblMsg.Text = string.Empty;
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            List<avii.Classes.RMA> rmaList = null;// RMAUtility.GetRMAStatusReport(rmaGUID);
            if (rmaList.Count > 0)
            {
                rptRma.DataSource = rmaList;
                rptRma.DataBind();
                RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
                    
                //mdlPopup2.Show();
            }
            else
            {
                lblMsg.Text = "No RMA history exists for this RMA";
                //rptRma.DataSource = null;
                //rptRma.DataBind();
            }

        }
        protected void Button2_Click1(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            int userid = 0;
            lblCount.Text = string.Empty;
            txtESNSearch.Text = string.Empty;
            txtUPC.Text = string.Empty;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);

            rmanumber.Text = string.Empty;
            txtRMADate.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            if (userid == 0)
            {
                ddlCompany.SelectedIndex = 0;
            }
            lblMsg.Text = string.Empty;
            //rebind = true;
            btnRMAReport.Visible = false;
            gvRma.DataSource = null;
            gvRma.DataBind();
            //radGridRmaDetails.MasterTableView.Rebind();
            Session["searchRma"] = null;
            Session["result"] = null;
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRma.PageIndex = e.NewPageIndex;

            if (Session["result"] != null)
            {
                List<avii.Classes.RMA> objRmaList = (List<avii.Classes.RMA>)Session["result"];
                gvRma.DataSource = objRmaList;
                gvRma.DataBind();

                // avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                // BindGrid1(pos);
            }
        }

        protected void imgEditRMA_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandArgument != null)
                {

                    int rmaguid = Convert.ToInt32(e.CommandArgument);
                    string url = string.Empty;
                    int userid = 0;
                    if (ViewState["userid"] != null)
                        Int32.TryParse(ViewState["userid"].ToString(), out userid);
                    if (userid > 0)
                        url = "NewRMAForm.aspx?rmaGUID=" + rmaguid;
                    else
                    {

                        //List<avii.Classes.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
                        //if (rmaUserCompany.Count > 0)
                        url = "NewRMAForm.aspx?rmaGUID=" + rmaguid + "&companyID=" + hdnCompanyId.Value;
                    }

                    Response.Redirect(url);
                }
                else
                    lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void imgEditRMADetail_Commnad(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandArgument != null)
                {

                    int rmaguid = Convert.ToInt32(e.CommandArgument);
                    string url = string.Empty;
                    int userid = 0;
                    if (ViewState["userid"] != null)
                        Int32.TryParse(ViewState["userid"].ToString(), out userid);
                    if (userid > 0)
                        url = "NewRMAForm.aspx?rmaGUID=" + rmaguid;
                    else
                    {

                        //List<avii.Classes.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
                        //if (rmaUserCompany.Count > 0)
                        url = "NewRMAForm.aspx?rmaGUID=" + rmaguid + "&companyID=" + hdnCompanyId.Value;
                    }

                    Response.Redirect(url);
                }
                else
                    lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void imgDeleteRMADetail_Commnad(object sender, CommandEventArgs e)
        {
            int rmadetguid = Convert.ToInt32(e.CommandArgument);
            int rmaGUID = 0;
            int userID = Convert.ToInt32(Session["UserID"]);
            List<avii.Classes.RMA> objRmaList = null;
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                RMAUtility.delete_RMA_RMADETAIL(0, rmadetguid, userID);
                //BindRMASearch(false);
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                //List<avii.Classes.RMADetail> rmaDetailList = (List<avii.Classes.RMADetail>)Session["rmadetaillist"];

                //var EsnList = (from item in rmaDetailList where item.rmaGUID.Equals(rmaGUID) select item).ToList();
                //if (EsnList.Count > 0)
                //{
                //    rptRmaItem.DataSource = EsnList;
                //    rptRmaItem.DataBind();
                //    lblMsgDetail.Text = "Deleted successfully";
                //}`   
                //elsea
                //    lblMsgDetail.Text = "No RMA found";

                //}
                int itemIndex = -1;
                if (ViewState["itemindex"] != null)
                    itemIndex = Convert.ToInt32(ViewState["itemindex"]);
                List<avii.Classes.RMADetail> rmaDetailList = new List<RMADetail>();
                objRmaList = Session["result"] as List<avii.Classes.RMA>;
                if (objRmaList != null)
                {
                    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                    if (rmaInfoList != null && rmaInfoList.Count > 0)
                    {
                        rmaDetailList = rmaInfoList[0].RmaDetails;
                        var rmaDetailInfoList = (from item2 in rmaDetailList where item2.rmaGUID.Equals(rmaGUID) select item2).ToList();
                        //RMADetail d = rmaDetailList.Where(d => d.rmaDetGUID == rmadetguid).Single();
                        var onlyMatch = rmaDetailInfoList.Single(s => s.rmaDetGUID == rmadetguid);
                        rmaDetailInfoList.Remove(onlyMatch);
                        gvRMADetail.DataSource = rmaDetailInfoList;
                        gvRMADetail.DataBind();
                        lblRMA.Text = rmaInfoList[0].RmaNumber;
                        lblStatus.Text = rmaInfoList[0].Status;
                        lblComment.Text = rmaInfoList[0].Comment;
                        lblAVComment.Text = rmaInfoList[0].AVComments;
                        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();

                        if (itemIndex > -1)
                            objRmaList[itemIndex].RmaDetails = rmaDetailInfoList;
                        Session["result"] = objRmaList;
                        lblMsgDetail.Text = "Deleted successfully";
                        //mdlPopup.Show();
                    }
                }
                //gvRMADetail.DataSource = rmaInfoList[0].RmaDetails;
                //gvRMADetail.DataBind();
                //BindRMA(rmaGUID, false);

            }
            catch (Exception ex)
            {
                lblMsgDetail.Text = ex.Message.ToString();
            }

        }
        protected void imgDeleteRMA_OnCommand(object sender, CommandEventArgs e)
        {

            int rmaguid = Convert.ToInt32(e.CommandArgument);
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            int userID = Convert.ToInt32(Session["UserID"]);
            try
            {
                RMAUtility.delete_RMA_RMADETAIL(rmaguid, 0, userID);
                BindRMASearch(true);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
       
    }
}