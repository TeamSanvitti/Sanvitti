using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using SV.Framework.Inventory;

namespace avii.BlockESN
{
    public partial class BlockEsnSearch : System.Web.UI.Page
    {
        private BlockEsnOperation BlockEsnOperation = BlockEsnOperation.CreateInstance<BlockEsnOperation>();
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
                btnDownload.Visible = false;
                if (Session["adm"] != null)
                {
                    BindCustomer();
                    // hdnCustomer.Value = "1";

                    //companyID = 464;
                }
                else
                {
                    // hdnCustomer.Value = "";
                    trCustomer.Visible = false;
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            companyID = userInfo.CompanyGUID;
                            ViewState["companyID"] = companyID;
                        }
                    }
                    BindCompanySKU(companyID);
                    
                }

            }
        }

        private void BlockESNSearch()
        {
           // SV.Framework.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.Inventory.BlockEsnOperation>();

            Session["blockESNList"] = null;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            btnDownload.Visible = false;

            string sortExpression = "CreateDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            string action = string.Empty, status = string.Empty, SKU = string.Empty, ESN = string.Empty;
            string dateFrom = string.Empty, dateTo = string.Empty;
            int companyID = 0;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                }
            }
            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();

            ESN = txtESN.Text.Trim();
            SKU = ddlSKU.SelectedValue;
            action = ddlAction.SelectedValue;
            status = ddlStatus.SelectedValue;

            if (Session["adm"] != null)
            {
                if (companyID == 0 && string.IsNullOrEmpty(action) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(ESN))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(action) && string.IsNullOrEmpty(status) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(ESN))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }

            List<SV.Framework.Models.Inventory.BlockESN> blockESNList = BlockEsnOperation.BlockEsnSearch(companyID, dateFrom, dateTo, SKU, ESN, action, status);
            if (blockESNList != null && blockESNList.Count > 0)
            {
                gvBlockEsn.DataSource = blockESNList;
                gvBlockEsn.DataBind();
                Session["blockESNList"] = blockESNList;
                btnDownload.Visible = true;
                lblCount.Text = "<strong>Total count:</strong> " + blockESNList.Count;
            }
            else
            {
                gvBlockEsn.DataSource = null;
                gvBlockEsn.DataBind();
                lblMsg.Text = "No record exists.";
            }

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void gvBlockEsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
                if (imgView != null)
                {
                    imgView.OnClientClick = "openDialogAndBlock('ESN List', '" + imgView.ClientID + "')";
                }
            }
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            btnDownload.Visible = false;
            //pnlUpload.Visible = true;
            gvBlockEsn.DataSource = null;
            gvBlockEsn.DataBind();

            //btnSubmit.Visible = false;
           // Clear();

            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
            {
                BindCompanySKU(companyID);
                //BindUsers(companyID);
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                
            }
        }


        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        private void BindCompanySKU(int companyID)
        {
            MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "SKU";
                ddlSKU.DataTextField = "SKU";


                ddlSKU.DataBind();
                ListItem item = new ListItem("", "");
                ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }
        private void DownloadCSV()
        {
            List<SV.Framework.Models.Inventory.BlockESN> blockESNList = Session["blockESNList"] as List<SV.Framework.Models.Inventory.BlockESN>;
            List<BlockESNCSV> blockESNs = new List<BlockESNCSV>();
            SV.Framework.Models.Inventory.BlockESNCSV blockESNCSV = null;
            if (blockESNList != null && blockESNList.Count > 0)
            {
                foreach(SV.Framework.Models.Inventory.BlockESN blockESN in blockESNList)
                {
                    blockESNCSV = new SV.Framework.Models.Inventory.BlockESNCSV();
                    blockESNCSV.Action = blockESN.Action;
                    blockESNCSV.ApprovedBy = blockESN.ApprovedBy;
                    blockESNCSV.ESNDATA = blockESN.ESNDATA;
                    blockESNCSV.ProductName = blockESN.ProductName;
                    blockESNCSV.ReceivedBy = blockESN.ReceiveBy;
                    blockESNCSV.SKU = blockESN.SKU;
                    blockESNCSV.CategoryName = blockESN.CategoryName;
                    blockESNCSV.CreateDate = blockESN.CreateDate;
                    blockESNCSV.CreatedBy = blockESN.CreateBy;
                    blockESNCSV.Status = blockESN.Status;
                    blockESNs.Add(blockESNCSV);
                   // blockESNCSV. = blockESN.Status;
                }
                if (blockESNs != null && blockESNs.Count > 0)
                {
                    string string2CSV = blockESNs.ToCSV();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=BlockESNs.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
            //ServiceRequestDownload
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BlockESNSearch();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void gvBlockEsn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBlockEsn.PageIndex = e.NewPageIndex;
            if (Session["blockESNList"] != null)
            {
                List<SV.Framework.Models.Inventory.BlockESN> blockESNList = (List<SV.Framework.Models.Inventory.BlockESN>)Session["blockESNList"];

                gvBlockEsn.DataSource = blockESNList;
                gvBlockEsn.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

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
        public List<SV.Framework.Models.Inventory.BlockESN> Sort<TKey>(List<SV.Framework.Models.Inventory.BlockESN> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SV.Framework.Models.Inventory.BlockESN>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SV.Framework.Models.Inventory.BlockESN>();
            }
        }
        protected void gvBlockEsn_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["blockESNList"] != null)
            {
                List<SV.Framework.Models.Inventory.BlockESN> blockESNList = (List<SV.Framework.Models.Inventory.BlockESN>)Session["blockESNList"];

                if (blockESNList != null && blockESNList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        blockESNList = Sort<SV.Framework.Models.Inventory.BlockESN>(blockESNList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        blockESNList = Sort<SV.Framework.Models.Inventory.BlockESN>(blockESNList, SortExp, SortDirection.Descending);
                    }
                    Session["blockESNList"] = blockESNList;

                    gvBlockEsn.DataSource = blockESNList;
                    gvBlockEsn.DataBind();
                }
            }
        }
        private void Clear()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            txtESN.Text = string.Empty;
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
            }
            ddlStatus.SelectedIndex = 0;
            ddlAction.SelectedIndex = 0;
            ddlSKU.Items.Clear();

            gvBlockEsn.DataSource = null;
            gvBlockEsn.DataBind();
        }

        protected void lnkApprove_Command(object sender, CommandEventArgs e)
        {
            int blockID = Convert.ToInt32(e.CommandArgument);
            string status = "Approved";
            int userID = Convert.ToInt32(Session["UserID"]);

            int returnResult = BlockEsnOperation.StockBlockUpdate(blockID, status, userID);
            if(returnResult > 0)
            {
                BlockESNSearch();
                lblMsg.Text = "Approved successfully";
            }
            else
            {
                lblMsg.Text = "Could not udated";
            }
        }

        protected void lnkReject_Command(object sender, CommandEventArgs e)
        {
            int blockID = Convert.ToInt32(e.CommandArgument);
            string status = "Rejected";
            int userID = Convert.ToInt32(Session["UserID"]);

            int returnResult = BlockEsnOperation.StockBlockUpdate(blockID, status, userID);
            if (returnResult > 0)
            {
                BlockESNSearch();
                lblMsg.Text = "Rejected successfully";                
            }
            else
            {
                lblMsg.Text = "Could not udated";
            }
        }

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            rptESN.DataSource = null;
            rptESN.DataBind();
            lblSKU.Text = string.Empty; //BlockESN> blockESNList 
            lblMsg2.Text = string.Empty; //BlockESN> blockESNList 
            try
            {
                int blockID = Convert.ToInt32(e.CommandArgument);
                List<SV.Framework.Models.Inventory.BlockESN> blockESNList = HttpContext.Current.Session["blockESNList"] as List<SV.Framework.Models.Inventory.BlockESN>;
                List<BlockEsn> ESNList = new List<BlockEsn>();
                BlockEsn blockEsn = null;
                var esnList = (from item in blockESNList where item.BlockID.Equals(blockID) select item).ToList();

                if (esnList != null && esnList.Count > 0)
                {
                    lblSKU.Text = esnList[0].SKU;
                    string[] esns = esnList[0].ESNDATA.Split(',');
                    foreach (string item in esns)
                    {
                        blockEsn = new BlockEsn();
                        blockEsn.ESN = item;
                        ESNList.Add(blockEsn);
                    }

                    rptESN.DataSource = ESNList;
                    rptESN.DataBind();
                }

               // else
                 //   lblRequestData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }

        }
    }
}