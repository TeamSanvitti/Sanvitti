using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Net;
using System.IO;

namespace avii.RMA
{
    public partial class NewRmaQuery : System.Web.UI.Page
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
                //Hashtable hashtable = RMAUtility.getReasonHashtable();
                //hashtable.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList();
                //ddReason.DataSource =  RMAUtility.getReasonHashtable();
                //ddReason.DataTextField = "value";
                //ddReason.DataValueField = "key";
                //ddReason.DataBind();
                //ddReason.Items.Insert(0, new ListItem(string.Empty));
                pnlGrid.Visible = false;
                if (Request["search"] != null && Request["search"] != "")
                {
                    //rebind = false;

                    bindsearchRMA(userid);
                }
                //BindRMA(userid);

            }



        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        public void ShowPDF(string strFileName)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            Response.ContentType = "application/pdf";
            Response.WriteFile(strFileName);
            Response.Flush();
            Response.Clear();
        }

        
        protected void lnkDownloadDoc_Click(object sender, CommandEventArgs e)
        {
            try
            {
                string fileName = e.CommandArgument.ToString();
                //string targetFolder = string.Empty;
                string targetFolder = Server.MapPath("~/Documents/RMA/");

                Response.Redirect("~/Documents/RMA/" + fileName, false);

                //System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/Documents/RMA/" + fileName));
                //Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" +
                //Server.UrlEncode(fi.Name));
                //Response.AddHeader("Content-Length", fi.Length.ToString());
                ////Response.ContentType = "application/vnd.ms-excel";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                ////Response.ContentType = "application/octet-stream";
                //Response.WriteFile(fi.FullName);
                //Response.End();
                
                //targetFolder = targetFolder + "\\Documents\\RMA\\";

                //string strURL = targetFolder + fileName;
                //FileInfo file = new FileInfo(strURL);
                //if (file.Exists)
                {

                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //// Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/ms-word";
                    //Response.WriteFile(file.FullName);
                    //Response.End();


                    //Response.Redirect("~/Documents/RMA/" + fileName, false);

                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.ContentType = "application/octet-stream";
                    //HttpContext.Current.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", strURL));
                    //HttpContext.Current.Response.WriteFile(strURL);
                    //HttpContext.Current.Response.End();
                    //HttpContext.Current.Response.Flush();



                    //Response.Redirect("~/document/printOrder.pdf", false);

                    //Response.Clear();
                    //Response.AddHeader("content-disposition", "attachment;filename=" + fi.Name);
                    //Response.AddHeader("content.length", fi.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(fi.FullName);
                    //Response.End();
                }
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=\"" + strURL + "\"");
                //byte[] data = req.DownloadData(strURL);
                //response.BinaryWrite(data);
                //response.End();
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvRma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
            imgView.OnClientClick = "openDialogAndBlock('RMA Detail', '" + imgView.ClientID + "')";
            ImageButton imgHistory = e.Row.FindControl("imgHistory") as ImageButton;
            imgHistory.OnClientClick = "openHistoryDialogAndBlock('RMA History', '" + imgHistory.ClientID + "')";

            ImageButton imgUpload = e.Row.FindControl("imgUpload") as ImageButton;
            imgUpload.OnClientClick = "openRmaDocDialogAndBlock('RMA Documents', '" + imgUpload.ClientID + "')";

            ImageButton imgCComments = e.Row.FindControl("imgCComments") as ImageButton;
            imgCComments.OnClientClick = "openCommentsDialogAndBlock('RMA Customer Comments', '" + imgCComments.ClientID + "')";

            ImageButton imgAComments = e.Row.FindControl("imgAComments") as ImageButton;
            imgAComments.OnClientClick = "openCommentsDialogAndBlock('RMA AV Comments', '" + imgAComments.ClientID + "')";
            
            if (Session["adm"] == null)
            {
                string status = ((DataBoundLiteralControl)e.Row.Cells[5].Controls[0]).Text;
                status = status.Replace("\r\n", "");
                status = status.Replace("\n", "");
                status = status.Trim();
                status = status.ToLower();
                if ("pending" != status)
                {
                    ImageButton imgEditRma = e.Row.FindControl("imgEditRma") as ImageButton;
                    ImageButton imgDel = e.Row.FindControl("imgDel") as ImageButton;
                    if (imgEditRma != null)
                        imgEditRma.Visible = false;
                    if (imgDel != null)
                        imgDel.Visible = false;
                }



            }

        }
        protected void imgCComments_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/ForecastComment.ascx");
                avii.Controls.ForecastComment ctlForecastComment = tmp2 as avii.Controls.ForecastComment;
                pnlComments.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int rmaguid = Convert.ToInt32(arr[0]);
                    string rmaNumber = arr[1];
                    lblRNum.Text = rmaNumber;
                    if (tmp2 != null)
                    {

                        ctlForecastComment.BindRMAComments(rmaguid, "R");
                    }
                    pnlComments.Controls.Add(ctlForecastComment);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }

        protected void imgAComments_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/ForecastComment.ascx");
                avii.Controls.ForecastComment ctlForecastComment = tmp2 as avii.Controls.ForecastComment;
                pnlComments.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int rmaguid = Convert.ToInt32(arr[0]);
                    string rmaNumber = arr[1];
                    lblRNum.Text = rmaNumber;
                    if (tmp2 != null)
                    {
                        ctlForecastComment.BindRMAComments(rmaguid, "A");
                    }
                    pnlComments.Controls.Add(ctlForecastComment);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }
        
        protected void gvRmaDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            if (Session["adm"] == null)
            {
                string status = "pending"; // ((DataBoundLiteralControl)e.Row.Cells[4].Controls[0]).Text;
                //status = status.Replace("\r\n", "");
                //status = status.Replace("\n", "");
                //status = status.Trim();
                status = lblStatus.Text.Trim();
                status = status.Replace("\r\n", "");
                status = status.Replace("\n", "");
                
                status = status.ToLower();
                if ("pending" != status)
                {
                    ImageButton imgDelDetail1 = e.Row.FindControl("imgDelDetail1") as ImageButton;
                    if (imgDelDetail1 != null)
                        imgDelDetail1.Visible = false;
                    
                }

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
            ////avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            //ddlCompany.DataSource = RMAUtility.getRMAUserCompany(-1, "", -1, -1);
            //ddlCompany.DataValueField = "companyID";
            //ddlCompany.DataTextField = "companyName";
            //ddlCompany.DataBind();
            //ListItem item = new ListItem("", "0");
            //ddlCompany.Items.Insert(0, item);

            ddlCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();
        }

        protected void search_click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            //UpdatePanel2.Update();
            //radGridRmaDetails.MasterTableView.Rebind();

            BindRMASearch(true);
        }

        private void BindRmaDocuments(int rmaGuid)
        {
            //List<RmaDocuments> rmaDocList = RMAUtility.GetRmaDocumentList(rmaGuid);
            //if (rmaDocList != null && rmaDocList.Count > 0)
            //{
            //    rptRMADoc.DataSource = rmaDocList;
            //    rptRMADoc.DataBind();
            //    lblDoc.Text = string.Empty;
            //}
            //else
            //{
            //    rptRMADoc.DataSource = null;
            //    rptRMADoc.DataBind();
            //    lblDoc.Text = "No records found";
            //}
        }
        protected void imgRmaDoc_Click(object sender, CommandEventArgs e)
        {

            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            BindRmaDocuments(rmaGUID);
            RegisterStartupScript("jsUnblockDialog", "unblockRmaDocDialog();");
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
                RmaNumber = (rmanumber.Text.Trim().Length > 0 ? rmanumber.Text.Trim() : string.Empty);
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
                        RmaNumber = RmaNumber.Replace("'", "");
                        UPC = UPC.Replace("'", "");
                        esn = esn.Replace("'", "");
                        poNum = poNum.Replace("'", "");
                        avso = avso.Replace("'", "");
                        

                        objRmaList = RMAUtility.GetRMAList(0, RmaNumber, txtRMADate.Text.Trim().Replace("'", ""), txtRMADateTo.Text.Trim().Replace("'", ""), Convert.ToInt32(Status), companyID, "", UPC, esn, avso, poNum, returnReason);
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
                        searchCriteria = companyID + "~" + RmaNumber + "~" + txtRMADate.Text.Trim() + "~" + Status + "~" + UPC + "~" + esn + "~" + this.txtRMADateTo.Text.Trim() + "~" + avso + "~" + poNum + "~" + returnReason;
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
            ////UpdatePanel1.Update();
            //int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["rmaGUID"] = rmaGUID;

            //List<avii.Classes.RMADetail> rmaDetailList = RMAUtility.GetRMADetailNew(rmaGUID, -1, string.Empty);//(List<avii.Classes.RMADetail>)Session["rmadetaillist"];

            ////var EsnList = (from item in rmaDetailList where item.rmaGUID.Equals(rmaGUID) select item).ToList();
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

        private void ReloadRmaView(int rmaGUID)
        {
            Control tmp1 = LoadControl("../controls/ViewRMA.ascx");
            avii.Controls.ViewRMA ctlViewRMA = tmp1 as avii.Controls.ViewRMA;
            phrViewRMA.Controls.Clear();
            //Panel1.Controls.Clear();
            
            if (tmp1 != null)
            {
                ctlViewRMA.RmaGUID = rmaGUID;

                ctlViewRMA.BindRMA();
            }   
            phrViewRMA.Controls.Add(ctlViewRMA);
            //Panel1.Controls.Add(ctlViewRMA);

        }
        private void BindRMANew(int rmaGUID)
        {
            lblMsgDetail.Text = string.Empty;
            if (rmaGUID > 0)
            {
                List<avii.Classes.RMA> objRmaList = null;
                if (Session["result"] != null)
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



                            List<avii.Classes.RMADetail> rmaDetailList = avii.Classes.RMAUtility.GetRMADetailNew(rmaGUID, -1, string.Empty);
                            if (rmaDetailList != null)
                            {



                                gvRMADetail.DataSource = rmaDetailList;
                                gvRMADetail.DataBind();
                            }
                            else
                            {
                                lblMsgDetail.Text = "No records found";
                                gvRMADetail.DataSource = rmaDetailList;
                                gvRMADetail.DataBind();
                            }
                        }

                    }
                }
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
            //ImageButton editImg = (ImageButton)sender;
            //GridViewRow row = (GridViewRow)editImg.NamingContainer;
            //int itemindex = row.RowIndex;

            //Session["itemindex"] = itemindex;
            //if (tmp != null)
            //{

            //    ctlRMADetail.BindRMA(rmaGUID, false);
            //}
            //pnlRMA.Controls.Add(ctlRMADetail);
            
            BindRMANew(rmaGUID);
            //ReloadRmaView(rmaGUID);
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                    
            //mdlPopup.Show();
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
        private void ReloadRmahistory(int rmaGUID)
        {
            Control tmp1 = LoadControl("../controls/RmaHistory.ascx");
            avii.Controls.RmaHistory ctlRmaHistory = tmp1 as avii.Controls.RmaHistory;
            phrHistory.Controls.Clear();
            if (tmp1 != null)
            {
                //ctlRmaHistory.UpdateMode = UpdatePanelUpdateMode.Conditional;
                ctlRmaHistory.RmaGUID = rmaGUID;

                ctlRmaHistory.BindRmaHistory();
                //ctlRmaHistory.Update();
            }
            phrHistory.Controls.Add(ctlRmaHistory);

        }
        
        protected void imgRMAHistory_Click(object sender, CommandEventArgs e)
        {
            lblHistory.Text = string.Empty;
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //ReloadRmahistory(rmaGUID);
            //RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
            List<avii.Classes.RMA> rmaList = RMAUtility.GetRMAStatusReport(rmaGUID);
            if (rmaList.Count > 0)
            {
                lblRMANum.Text = rmaList[0].RmaNumber;
                rptRma.DataSource = rmaList;
                rptRma.DataBind();
                
                //mdlPopup2.Show();
            }
            else
            {
                lblHistory.Text = "No RMA history exists for this RMA";
                rptRma.DataSource = null;
                rptRma.DataBind();
                lblRMANum.Text = string.Empty;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");


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
        protected void imgDeleteRMA_OnCommand(object sender, CommandEventArgs e)
        {

            int rmaguid = Convert.ToInt32(e.CommandArgument);
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                //ViewState["companyID"] = userInfo.CompanyGUID;
                userID = userInfo.UserGUID;
            }
            try
            {
                RMAUtility.Delete_RMA_RMADetail(rmaguid, 0, userID);
                BindRMASearch(true);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            
        }
        protected void imgDeleteRMADetail_Commnad(object sender, CommandEventArgs e)
        {
            lblMsgDetail.Text = string.Empty;
            int rmadetguid = Convert.ToInt32(e.CommandArgument);
            int rmaGUID = 0;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                //ViewState["companyID"] = userInfo.CompanyGUID;
                userID = userInfo.UserGUID;
            }
            //List<avii.Classes.RMA> objRmaList = null;
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                RMAUtility.Delete_RMA_RMADetail(0, rmadetguid, userID);
                //BindRMASearch(false);
                if (ViewState["rmaGUID"] != null)
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                BindRMANew(rmaGUID);
                lblMsgDetail.Text = "Deleted successfully";

                //lblMsgDetail.Text = "Deleted successfully";
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


                //int itemIndex = -1;
                //if (ViewState["itemindex"] != null)
                //    itemIndex = Convert.ToInt32(ViewState["itemindex"]);
                //List<avii.Classes.RMADetail> rmaDetailList = new List<RMADetail>();
                //objRmaList = Session["result"] as List<avii.Classes.RMA>;
                //if (objRmaList != null)
                //{
                //    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                //    if (rmaInfoList != null && rmaInfoList.Count > 0)
                //    {
                //        rmaDetailList = rmaInfoList[0].RmaDetails;
                //        var rmaDetailInfoList = (from item2 in rmaDetailList where item2.rmaGUID.Equals(rmaGUID) select item2).ToList();
                //        //RMADetail d = rmaDetailList.Where(d => d.rmaDetGUID == rmadetguid).Single();
                //        var onlyMatch = rmaDetailInfoList.Single(s => s.rmaDetGUID == rmadetguid);
                //        rmaDetailInfoList.Remove(onlyMatch);
                //        gvRMADetail.DataSource = rmaDetailInfoList;
                //        gvRMADetail.DataBind();
                //        lblRMA.Text = rmaInfoList[0].RmaNumber;
                //        lblStatus.Text = rmaInfoList[0].Status;
                //        lblComment.Text = rmaInfoList[0].Comment;
                //        lblAVComment.Text = rmaInfoList[0].AVComments;
                //        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                //        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();

                //        if (itemIndex > -1)
                //            objRmaList[itemIndex].RmaDetails = rmaDetailInfoList;
                //        Session["result"] = objRmaList;
                //        lblMsgDetail.Text = "Deleted successfully";
                //        //mdlPopup.Show();
                //    }
                //}
                //gvRMADetail.DataSource = rmaInfoList[0].RmaDetails;
                //gvRMADetail.DataBind();
                //BindRMA(rmaGUID, false);

            }
            catch (Exception ex)
            {
                lblMsgDetail.Text = ex.Message.ToString();
            }

        }
        
    }
}