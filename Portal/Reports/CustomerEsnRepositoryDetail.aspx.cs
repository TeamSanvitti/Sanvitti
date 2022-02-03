using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using System.Reflection;
using SV.Framework.Models.Inventory;

namespace avii.Reports
{
    public partial class CustomerEsnRepositoryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;

                }
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                //btnPrint.Visible = false;
                btnDownload.Visible = false;
                btnDownloadESN.Visible = false;
                //BindCustomer();
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        lblCompany.Text = userInfo.CompanyName;
                        dpCompany.Visible = false;
                        ViewState["companyid"] = userInfo.CompanyGUID;
                        BindCompanySKU(userInfo.CompanyGUID);


                    }
                }
                else
                {
                    BindCustomer();
                    ddlSKU.Visible = false;
                    lblSKU.Visible = false;

                }
                if (Session["sidcompnyid"] != null && Session["skuid"] != null)
                {
                    int companyID = Convert.ToInt32(Session["sidcompnyid"]);
                    int ItemCompanyGUID = Convert.ToInt32(Session["skuid"]);

                    Session["sidcompnyid"] = null;
                    Session["skuid"] = null;
                    if (Session["adm"] != null)
                    {
                        dpCompany.SelectedValue = companyID.ToString();
                        chkUnusedESN.Checked = true;
                        ddlSKU.Visible = true;
                        lblSKU.Visible = true;
                        if (companyID > 0)
                        {
                            BindCompanySKU(companyID);
                            ddlSKU.SelectedValue = ItemCompanyGUID.ToString();

                            BindESNs();
                        }
                        else
                        {
                            ddlSKU.Visible = false;
                            lblSKU.Visible = false;

                        }
                    }
                }

                
            }
        }

        private void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindCompanySKU(int companyID)
        {
            SV.Framework.Inventory.MslOperation mslOperation = SV.Framework.Inventory.MslOperation.CreateInstance<SV.Framework.Inventory.MslOperation>();

            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUList(companyID, 0);
            if (skuList != null)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
                ddlSKU.Items.Insert(0, new ListItem("", "0"));
                ddlSKU.Visible = true;
                lblSKU.Visible = true;
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                ddlSKU.Items.Insert(0, new ListItem("", "0"));
                lblMsg.Text = "No SKU are assigned to select Customer";

            }
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;

            ddlSKU.Visible = true;
            lblSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                ddlSKU.Visible = false;
                lblSKU.Visible = false;

            }
        }


        protected void gvESN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgESN = e.Row.FindControl("imgESN") as ImageButton;
            if (imgESN != null)
                imgESN.OnClientClick = "openDialogAndBlock('ESN LOG', '" + imgESN.ClientID + "')";

            

        }
        protected void imgESNLog_Click(object sender, CommandEventArgs e)
        {
            string esn = e.CommandArgument.ToString();
            
            Control tmp2 = LoadControl("~/controls/EsnLogDetails.ascx");
            avii.Controls.EsnLogDetails ctlesnLog = tmp2 as avii.Controls.EsnLogDetails;
            pnlESN.Controls.Clear();
            
            if (tmp2 != null)
            {

                ctlesnLog.BindEsn(esn);
            }
            pnlESN.Controls.Add(ctlesnLog);
            
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

            //ModalPopupExtender1.Show();
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindESNs();
        }
        private void BindESNs()
        {
            int companyID = 0;
            int itemCompanyGuid = 0;

            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    //lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;
                    companyID = userInfo.CompanyGUID;
                }
            }
            else
            {
                lblCompany.Text = string.Empty;

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }

            if (ddlSKU.SelectedIndex > 0)
                itemCompanyGuid = Convert.ToInt32(ddlSKU.SelectedValue);

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            else
                fromDate = DateTime.Now.AddDays(-365);
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }
            else
                toDate = DateTime.Now;

            //if (ddlDuration.SelectedIndex > 0)
            //timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            //esnStatusID = Convert.ToInt32(ddlEsnStatus.SelectedValue);
            //rmaStatusID = Convert.ToInt32(ddlRmaStaus.SelectedValue);

            string sortExpression = "UploadDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            
            if (companyID > 0)
                PopulateData(companyID, itemCompanyGuid, fromDate, toDate);
            else
                lblMsg.Text = "Please select Customer!";
        }
        private void PopulateData(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            Session["searcher"] = companyID + "~" + itemCompanyGUID + "~" + fromDate + "~" + toDate + "~" + chkUnusedESN.Checked + "~" + chkAllUnusedESN.Checked;

            List<EsnRepositoryDetail> esnList = inventoryReportOperation.GetCustomerEsnRepositoryDetail(companyID, itemCompanyGUID, fromDate, toDate, chkUnusedESN.Checked, chkAllUnusedESN.Checked);

            
            Session["EsnRepositoryDetail"] = esnList;
            if (esnList != null && esnList.Count > 0)
            {
                var usedESNs = (from item in esnList where !string.IsNullOrEmpty(item.FulfillmentNumber) select item).ToList();
                int totalCount = esnList.Count;
                int usedESN = 0;
                if(usedESNs != null && usedESNs.Count > 0)
                {
                    usedESN = usedESNs.Count;
                }
                gvESN.DataSource = esnList;
                lblCount.Text = "<strong>Total records: " + totalCount.ToString() + " &nbsp;&nbsp; Used ESN:</strong> " + Convert.ToString(usedESN) + " &nbsp;&nbsp; <strong>Unsed ESN:</strong> " + Convert.ToString(totalCount-usedESN);
                lblMsg.Text = string.Empty;
                //btnPrint.Visible = true;
                btnDownload.Visible = true;
                btnDownloadESN.Visible = true;
            }
            else
            {
                //btnPrint.Visible = false;
                btnDownload.Visible = false;
                lblCount.Text = string.Empty;
                gvESN.DataSource = null;
                lblMsg.Text = "No records found";
                Session["EsnRepositoryDetail"] = null;
            }
            
            gvESN.DataBind();
        }
        private void ReloadData()
        {
            List<EsnRepositoryDetail> rmaStatusList = null;
            if (Session["EsnRepositoryDetail"] != null)
            {
                rmaStatusList = (List<EsnRepositoryDetail>)Session["EsnRepositoryDetail"];

                Session["EsnRepositoryDetail"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    var usedESNs = (from item in rmaStatusList where !string.IsNullOrEmpty(item.FulfillmentNumber) select item).ToList();
                    int totalCount = rmaStatusList.Count;
                    int usedESN = 0;
                    if (usedESNs != null && usedESNs.Count > 0)
                    {
                        usedESN = usedESNs.Count;
                    }
                    gvESN.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total records: &nbsp;&nbsp; Used ESN:</strong> " + Convert.ToString(usedESN) + " &nbsp;&nbsp; <strong>Unsed ESN:</strong> " + Convert.ToString(totalCount - usedESN);

                    //gvESN.DataSource = rmaStatusList;
                    //lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvESN.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                btnDownload.Visible = false;
                //btnPrint.Visible = false;
                gvESN.DataSource = null;
                lblMsg.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvESN.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            //ddlDuration.SelectedIndex = 0;
            ddlSKU.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            lblMsg.Text = string.Empty;
            //lblDuration.Text = string.Empty;
            //btnPrint.Visible = false;
            btnDownload.Visible = false;
            btnDownloadESN.Visible = false;
            lblCount.Text = string.Empty;
            //lblRMA.Text = string.Empty;
            gvESN.DataSource = null;
            gvESN.DataBind();
            chkUnusedESN.Checked = false;
            //ReloadPOStatus(0, 0);


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            ReloadData();

        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<EsnRepositoryDetail> Sort<TKey>(List<EsnRepositoryDetail> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<EsnRepositoryDetail>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<EsnRepositoryDetail>();
            }
        }
        protected void gvESN_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["EsnRepositoryDetail"] != null)
            {
                List<EsnRepositoryDetail> esnHeadersList = (List<EsnRepositoryDetail>)Session["EsnRepositoryDetail"];

                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        esnHeadersList = Sort<EsnRepositoryDetail>(esnHeadersList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        esnHeadersList = Sort<EsnRepositoryDetail>(esnHeadersList, SortExp, SortDirection.Descending);
                    }
                    Session["EsnRepositoryDetail"] = esnHeadersList;
                    gvESN.DataSource = esnHeadersList;
                    gvESN.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadEsnInfo();
        }
        protected void btnDownloadEsn_Click(object sender, EventArgs e)
        {
            DownloadEsnList();
        }
        private void DownloadEsnInfo()
        {
            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();

            List<EsnRepositoryDetail> esnList = (Session["EsnRepositoryDetail"]) as List<EsnRepositoryDetail>;

            if (esnList != null && esnList.Count > 0)
            {

                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = "Esn" + Session.SessionID + ".csv";
                bool found = false;
                //System.IO.FileInfo file = null;
                //file = new System.IO.FileInfo(path + fileName);
                //if (file.Exists)
                //{
                //    file.Delete();
                //}

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (chkUnusedESN.Checked)
                    sb.Append("ESN,UploadDate,SKU\r\n");
                else
                    sb.Append("ESN,UploadDate,SKU,Fulfillment#,FulfillmentDate,FulfillmentStatus,ShippedDate,RMA#,RMADate,RMAStatus,RMAEsnStatus\r\n");
                string poDate, rmaDate, shipDate;
                poDate = rmaDate = shipDate = string.Empty;
                foreach (EsnRepositoryDetail esnObj in esnList)
                {
                    if (chkUnusedESN.Checked)
                        sb.Append(esnObj.ESN + ","
                        + esnObj.UploadDate.ToShortDateString() + ","
                        + esnObj.SKU + "\r\n");
                    else
                    {
                        poDate = esnObj.FulfillmentDate.ToShortDateString() == "1/1/0001" ? "" : esnObj.FulfillmentDate.ToShortDateString();
                        shipDate = esnObj.ShipDate.ToShortDateString() == "1/1/0001" ? "" : esnObj.ShipDate.ToShortDateString();
                        rmaDate = esnObj.RmaDate.ToShortDateString() == "1/1/0001" ? "" : esnObj.RmaDate.ToShortDateString();

                        sb.Append(esnObj.ESN + ","
                            + esnObj.UploadDate.ToShortDateString() + ","
                            + esnObj.SKU + ","
                            + esnObj.FulfillmentNumber + ","
                            + poDate + ","
                            + esnObj.FulfillmentStatus + ","
                            + shipDate + ","
                                    + esnObj.RmaNumber + ","
                                    + rmaDate + ","
                                    + esnObj.RmaStatus + ","
                                    + esnObj.RmaEsnStatus
                                    + "\r\n");
                    }
                    found = true;
                }
                
                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                }
            }
        }

        private void DownloadEsnList()
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
            int companyID = 0;
            int itemCompanyGUID = 0;
            DateTime fromDate;
            DateTime toDate;
            bool chk_UnusedESN = false;
            bool chk_AllUnusedESN = false;
            string str = Session["searcher"] as string; ;
            
            string[] stringArray = str.Split('~');
            companyID = Convert.ToInt32(stringArray[0]);
            itemCompanyGUID = Convert.ToInt32(stringArray[1]);
            fromDate = Convert.ToDateTime(stringArray[2]);
            toDate = Convert.ToDateTime(stringArray[3]);

            chk_UnusedESN = Convert.ToBoolean(stringArray[4]);
            chk_AllUnusedESN = Convert.ToBoolean(stringArray[5]);
            
            List<EsnRepository> esnList = inventoryReportOperation.GetCustomerEsnRepositoryDownload(companyID, itemCompanyGUID, fromDate, toDate, chk_UnusedESN, chk_AllUnusedESN);
            //if (esnDownloadList != null && esnDownloadList.Count > 0)
            //{
            //    Session["EsnRepository"] = esnDownloadList;
            //    btnDownloadESN.Visible = true;
            //}

            //List<EsnRepository> esnList = (Session["EsnRepository"]) as List<EsnRepository>;

            if (esnList != null && esnList.Count > 0)
            {

                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = "dEsn" + Session.SessionID + ".csv";
                bool found = false;
                //System.IO.FileInfo file = null;
                //file = new System.IO.FileInfo(path + fileName);
                //if (file.Exists)
                //{
                //    file.Delete();
                //}

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (chkUnusedESN.Checked)
                    sb.Append("UnusedESN,InPorocessESN,ShippedESN,RmaESN\r\n");
                foreach (EsnRepository esnObj in esnList)
                {
                    if (chkUnusedESN.Checked)
                        sb.Append(esnObj.UnusedESN + ","
                        + esnObj.InPorocessESN + ","
                        + esnObj.ShippedESN + ","
                        + esnObj.RmaESN + "\r\n");
                    
                    found = true;
                }

                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                }
            }
        }

    }
}