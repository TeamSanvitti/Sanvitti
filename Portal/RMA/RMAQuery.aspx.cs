using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using Telerik.Web.UI;

namespace avii.Admin.RMA
{
    public partial class RMAQuery : System.Web.UI.Page
    {

        bool rebind = false;
       
        
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
          
            rebind = false;
            if (!IsPostBack)
            {
                int userid = 0;
                int companyID = 0;
                rebind = false;
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
                ViewState["userid"]=userid;
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

                if (Request["search"] != null && Request["search"] != "")
                {
                    rebind = false;

                    bindsearchRMA(userid);
                }
                BindRMA(userid);
                
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

                grid_bind(true);
                if (userid > 0)
                {
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                else
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
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
                
                grid_bind(true);
                Session["rmastatus"] = null;
                Session["days"] = null;

                if (userid > 0)
                {
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                else
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
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
            
            //radGridRmaDetails.MasterTableView.Rebind();

            grid_bind(true);
        }

        //Maintain list to be serialised to xml for bulk update of status
        private List<avii.Classes.RMA_Status> generateRMAStatusList()
        {
            List<avii.Classes.RMA_Status> rmalist = new List<avii.Classes.RMA_Status>();
           //string vxml = string.Empty;
           int userid = 0;
           //avii.Classes.RMAUtility rmautilityobj = new avii.Classes.RMAUtility();
           if (ViewState["userid"] != null)
               userid = Convert.ToInt32(ViewState["userid"]);
           try
           {
               foreach (GridDataItem items in radGridRmaDetails.Items)
               {
                   if (items.OwnerTableView.Name == "RMAMaster")
                   {
                       HiddenField hdnrmaGUID = (HiddenField)items.FindControl("hdnrmaGUID");
                       if (items.Selected)
                       {
                           avii.Classes.RMA_Status rmaobj = new avii.Classes.RMA_Status();
                           rmaobj.RmaGUID = Convert.ToInt32(hdnrmaGUID.Value);
                           rmaobj.RmaStatusID = Convert.ToInt32(ddlchangestatus.SelectedValue);
                           rmaobj.UserID = userid;
                           rmalist.Add(rmaobj);

                       }
                   }
               }

               //vxml = rmautilityobj.serializeObjetToXMLString((object)this.rmalist, "ArrayOfRMA_Status", "RMA_Status");
           }
           catch (Exception ex)
           {
               lblMsg.Text = ex.Message.ToString();
           }
           return rmalist;

            
        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            //avii.Classes.RMAUtility rmautilityobj = new avii.Classes.RMAUtility();
           // string objxml;
            List<avii.Classes.RMA_Status> rmaStatusList = null;
            try
            {
                rmaStatusList = generateRMAStatusList();
                //objxml = generatexml();
                RMAUtility.update_RMA_batchupdate(rmaStatusList);
                radGridRmaDetails.MasterTableView.Rebind();
                lblMsg.Text = "RMA Updated Successfully";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        //Bind Grid with RMA and RMA Details based on the search
        protected void grid_bind(bool rebind)
        {
            int companyID = -1;
            int userid = 0;
            string Status = "-1";
            string RmaNumber, rmaDate,rmaDateTo,UPC, esn, avso, poNum, returnReason;
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
                if (userid>0)
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

                if (userid>0)
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
                        objRmaList = RMAUtility.getRMAList(0, rmanumber.Text, txtRMADate.Text, txtRMADateTo.Text, Convert.ToInt32(Status), companyID, "", UPC, esn, avso, poNum, returnReason);
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
                        radGridRmaDetails.DataSource = objRmaList;
                        radGridRmaDetails.DataBind(); 
                        Session["searchRma"] = searchCriteria;
                        if (radGridRmaDetails.MasterTableView.Items.Count > 0)
                        {


                            radGridRmaDetails.Visible = true;
                            btnExport.Visible = true;
                            hlkRMASummary.Visible = true;
                            if (companyID > 0)
                                hlkRMASummary.NavigateUrl = "/rma/rmaSummary.aspx?c" + companyID.ToString();
                        }
                        //radGridRmaDetails.Visible = true;
                        //btnExport.Visible = true;
                        pnlGrid.Visible = true;
                    }
                    else
                    {
                        lblCount.Text = string.Empty;
                        pnlGrid.Visible = false;
                        btnRMAReport.Visible = false;
                        radGridRmaDetails.DataSource = null;
                        radGridRmaDetails.Visible = false;
                        btnExport.Visible = false;
                        hlkRMASummary.Visible = false;
                        
                        
                        lblMsg.Text = "No matching record exists for selected search criteria";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    radGridRmaDetails.Visible = false;
                    btnExport.Visible = false;
                    lblCount.Text = string.Empty;
                }

                if (userid>0)
                {
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                else
                    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
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
        protected void LinkButton1_Click(object sender, CommandEventArgs e)
        {
            int rmaguid = Convert.ToInt32(e.CommandArgument);
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();

            try
            {
                RMAUtility.delete_RMA_RMADETAIL(rmaguid, 0);
                grid_bind(false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
               
       
        }
        protected void LinkButton2_Click(object sender, CommandEventArgs e)
        {
            int rmadetguid = Convert.ToInt32(e.CommandArgument);
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                RMAUtility.delete_RMA_RMADETAIL(0, rmadetguid);
                grid_bind(false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
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
            if (userid==0)
            {
                ddlCompany.SelectedIndex = 0;
            }
            lblMsg.Text = string.Empty;
            rebind = true;
            btnRMAReport.Visible = false;
            radGridRmaDetails.MasterTableView.Rebind();
            Session["searchRma"] = null;
            Session["result"] = null;
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (radGridRmaDetails.MasterTableView.Items.Count > 0)
                {
                }
            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            pnlGrid.Visible = false;
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            //if (rebind)
            //{
            //    radGridRmaDetails.DataSource = RMAUtility.getRMAList(0, "", "", "", -1, 0, "", "", "", string.Empty, string.Empty, string.Empty);
            //    radGridRmaDetails.Visible = false;
            //    btnExport.Visible = false;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(hdnrmaGUIDs.Value))
            //    {
            //        radGridRmaDetails.DataSource = RMAUtility.getRMAList(0, "", "", "", -1, -1, hdnrmaGUIDs.Value, "", "", string.Empty, string.Empty, string.Empty);
            //        hdnrmaGUIDs.Value = string.Empty;
            //    }
            //    else
            //        grid_bind();

            //}
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            int userid = 0;
            string company = ConfigurationManager.AppSettings["company"].ToString();
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            string companyname=string.Empty;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);

            if (userid > 0)
            {

                avii.Classes.RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(-1, "", -1, userid);
                companyname = objRMAcompany.CompanyName;
            }
            else
            {
                if (ddlCompany.SelectedIndex > 0)
                    companyname = ddlCompany.SelectedItem.Text;
            }
          
  
            hdncompany.Value = companyname;

            if (userid==0)
            {
                companyname = ddlCompany.SelectedItem.Text;
            }
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headerItem = e.Item as GridHeaderItem;
                if (headerItem.OwnerTableView.Name == "RMAMaster")
                {
                    if (userid>0)
                        headerItem["CompanyName"].Visible = false;
                    else
                        headerItem["CompanyName"].Visible = true;
                    
                }
                if (headerItem.OwnerTableView.Name == "rmaDetails")
                {
                    if (company == companyname)
                    {
                        headerItem["CallTime"].Visible = false;
                        headerItem["Reason"].Visible = false;
                    }

                }
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
               
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    if (dataItem.OwnerTableView.Name == "RMAMaster")
                    {
                        LinkButton lnkedit = (LinkButton)dataItem.FindControl("lnkedit");

                        if (userid>0)
                        {

                            dataItem["CompanyName"].Visible = false;
                            Label lblRMAMasterstatus = dataItem.FindControl("lblstatus") as Label;
                            if (lblRMAMasterstatus != null)
                            {
                                if ("Approved" == lblRMAMasterstatus.Text)
                                {
                                    lnkedit.Visible = false;
                                    dataItem["EditCommandColumn"].Visible = false;
                                    dataItem["RmaDeleteColumn"].Visible = false;
                                }
                                if ("Pending" != lblRMAMasterstatus.Text)
                                {
                                    lnkedit.Visible = false;
                                    dataItem["EditCommandColumn"].Visible = false;
                                    dataItem["RmaDeleteColumn"].Visible = false;
                                }
                            }
                        }
                        else
                        {
                            dataItem["CompanyName"].Visible = true;
                        }
                    }
                   
                    //string value = groupDataRow["CompanyName"].ToString();

                    if (dataItem.OwnerTableView.Name == "rmaDetails")
                    {
                        
                        if (company == companyname)
                        {
                            dataItem["CallTime"].Visible = false;
                            dataItem["Reason"].Visible = false;
                        }
                        else 
                        {
                            Label lblreason = (Label)dataItem.FindControl("lblreason");
                            HiddenField hdnReason = (HiddenField)dataItem.FindControl("hdnReason");
                            try
                            {
                                if (hdnReason.Value != null && hdnReason.Value != string.Empty && hdnReason.Value!="0")
                                {
                                    Hashtable reasonHashtable = RMAUtility.getReasonHashtable();
                                    
                                    lblreason.Text = reasonHashtable[hdnReason.Value].ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message.ToString();
                            }

                        }
                        if (userid>0)
                        {
                            Label lblRMAdetstatus = (Label)dataItem.FindControl("lblstatus");
                           
                            if ("Approved" == lblRMAdetstatus.Text)
                            {
                                dataItem["EditCommandColumn"].Visible = false;
                                dataItem["EsnDeleteColumn"].Visible = false;
                            }
                            if ("Pending" != lblRMAdetstatus.Text)
                            {
                                dataItem["EditCommandColumn"].Visible = false;
                                dataItem["EsnDeleteColumn"].Visible = false;
                            }
                        }
                        else
                            dataItem["CompanyName"].Visible = true;
                    }


                }
            }
        
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
        }
        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            string rmaGUID;
           
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "rmaDetails":
                        {
                            rmaGUID = dataItem.GetDataKeyValue("RmaGUID").ToString();
                            List<RMADetail> rmaDetailList = new List<RMADetail>();


                            DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                            DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", rmaGUID));
                            DataRow searchedRow = null;
                            if (rows.Length > 0)
                            {
                                foreach (DataRow dataRow in rows)
                                {
                                    RMADetail objRMADETAIL = new RMADetail();
                                    objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                                    objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                                    objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                                    objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                                    objRMADETAIL.Reason = clsGeneral.getColumnData(dataRow, "Reason", 0, false).ToString();
                                    objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                                    objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                                    objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                                    objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                                    objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                                    objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                                    objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                                    objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                                    objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                                    objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                                    objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                                    rmaDetailList.Add(objRMADETAIL);       
                                }
                            }
                            e.DetailTableView.DataSource = rmaDetailList;
                            //e.DetailTableView.DataSource = RMAUtility.getRMADetail(Convert.ToInt32(rmaGUID), -1, "");
                            break;
                        }
                }
           
        }

        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            grid_bind(false);
        }
        
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            string RmaGUID;
            string rmaDetGUID;
            GridDataItem item = (GridDataItem)e.Item;
            if (item.OwnerTableView.Name == "RMAMaster")
            {
                RmaGUID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RmaGUID"].ToString();

                RMAUtility.delete_RMA_RMADETAIL(Convert.ToInt32(RmaGUID), 0);
            }
            if (item.OwnerTableView.Name == "rmaDetails")
            {
                rmaDetGUID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["rmaDetGUID"].ToString();

                RMAUtility.delete_RMA_RMADETAIL(0, Convert.ToInt32(rmaDetGUID));
            }

        }
        protected void btnExport_click(object sender, EventArgs e)
        {
            
            string filename = string.Empty;
            bool selectflag = false;
            DateTime dt = DateTime.Now;
            int userid = 0;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            hdnrmaGUIDs.Value = string.Empty;
            foreach (GridDataItem items in radGridRmaDetails.Items)
            {
                if (items.OwnerTableView.Name == "RMAMaster")
                {
                    HiddenField hdnrmaGUID = (HiddenField)items.FindControl("hdnrmaGUID");
                    if (items.Selected)
                    {
                        selectflag = true;
                        if (hdnrmaGUIDs.Value == string.Empty)
                            hdnrmaGUIDs.Value = hdnrmaGUID.Value;
                        else
                            hdnrmaGUIDs.Value = hdnrmaGUIDs.Value + "," + hdnrmaGUID.Value;

                    }
                }
            }
            if (selectflag)
            {
                radGridRmaDetails.MasterTableView.Rebind();
            }
            
            foreach (GridDataItem item in radGridRmaDetails.Items)
            {
               if (item.OwnerTableView.Name == "RMAMaster")
                {
                    radGridRmaDetails.MasterTableView.Columns.FindByUniqueName("colSelect").Visible = false;
                    LinkButton lnkedit = (LinkButton)item.FindControl("lnkedit");
                    lnkedit.Visible = false;

                    if (radGridRmaDetails.Columns.Count > 0)
                        radGridRmaDetails.Columns[6].Visible = false;

                    
                }
                if (item.OwnerTableView.Name == "rmaDetails")
                {
                    Label lblESN = (Label)item.FindControl("lblESN");

                    lblESN.Text = "#" + lblESN.Text;
                
                }

            }
            if (userid==0)
                filename = hdncompany.Value + "_";
            else
                filename = "Aerovoice_";
            radGridRmaDetails.MasterTableView.HierarchyDefaultExpanded = true;

            radGridRmaDetails.ExportSettings.FileName = filename + dt.ToString();
            
            radGridRmaDetails.ExportSettings.ExportOnlyData = true;
            radGridRmaDetails.ExportSettings.IgnorePaging = true;
            radGridRmaDetails.ExportSettings.OpenInNewWindow = true;
            radGridRmaDetails.MasterTableView.ExportToExcel();
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
            mdlPopup.Show();
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
            List<avii.Classes.RMA> rmaList = RMAUtility.GetRMAStatusReport(rmaGUID);
            if (rmaList.Count > 0)
            {
                rptRma.DataSource = rmaList;
                rptRma.DataBind();
                mdlPopup2.Show();
            }
            else
            {
                lblMsg.Text = "No RMA history exists for this RMA";
                //rptRma.DataSource = null;
                //rptRma.DataBind();
            }

        }

       
    }



}
