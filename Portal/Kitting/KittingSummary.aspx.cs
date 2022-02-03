using avii.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.Kitting
{
    public partial class KittingSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int companyID = 237;
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                if (userInfo != null && userInfo.CompanyGUID > 0)
                    companyID = userInfo.CompanyGUID;

                BindUsers(companyID);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindKittedSummary();
        }
        private void BindKittedSummary()
        {
            KittingOperations kittingOperations = KittingOperations.CreateInstance<KittingOperations>();
            string sortExpression = "KittedCSTDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            string fromDate = "", toDate = "", fulfillmentNumber = "", IMEI = "", BoxID = "";
            int userID = 0;
            if (ddlUser.SelectedIndex > 0)
                userID = Convert.ToInt32(ddlUser.SelectedValue);
            fromDate = txtDateFrom.Text.Trim();
            toDate = txtDateTo.Text.Trim();
            IMEI = txtESN.Text.Trim();
            BoxID = txtBoxNo.Text.Trim();
            fulfillmentNumber = txtPONum.Text.Trim();
            gvKitting.DataSource = null;
            gvKitting.DataBind();
            lblMsg.Text = "";
            lblCount.Text = "";

            List<PurchaseOrderKittingSummary> kittingSmmary = kittingOperations.GetPurchaseOrderKittingSummary(fulfillmentNumber, IMEI, BoxID, userID, fromDate, toDate);
            if (kittingSmmary != null && kittingSmmary.Count > 0)
            {
                Session["kittingSmmary"] = kittingSmmary;
                var totalEsnCount = kittingSmmary.Sum(x => x.EsnCount);

                lblCount.Text = "<strong>Total Box Count: " + kittingSmmary.Count + ", &nbsp;&nbsp;&nbsp; Total IMEI Count: " + totalEsnCount + "</strong>";
                gvKitting.DataSource = kittingSmmary;
                gvKitting.DataBind();
            }
            else
            { 
                lblMsg.Text = "No record found"; 
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if (userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataValueField = "UserID";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();

                ListItem newList = new ListItem("", "0");
                ddlUser.Items.Insert(0, newList);
            }
            else
            {
                ddlUser.DataSource = null;
                ddlUser.DataBind();
                lblMsg.Text = "No users are assigned to selected Customer";
            }
        }

        protected void gvKitting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            gvKitting.PageIndex = e.NewPageIndex;
            if (Session["kittingSmmary"] != null)
            {
                List<PurchaseOrderKittingSummary> kittingSmmary = (List<PurchaseOrderKittingSummary>)Session["kittingSmmary"];

                gvKitting.DataSource = kittingSmmary;
                gvKitting.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        protected void gvKitting_Sorting(object sender, GridViewSortEventArgs e)
        {

            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["kittingSmmary"] != null)
            {
                List<PurchaseOrderKittingSummary> kittingSmmary = (List<PurchaseOrderKittingSummary>)Session["kittingSmmary"];

                if (kittingSmmary != null && kittingSmmary.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        kittingSmmary = Sort<PurchaseOrderKittingSummary>(kittingSmmary, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        kittingSmmary = Sort<PurchaseOrderKittingSummary>(kittingSmmary, SortExp, SortDirection.Descending);
                    }
                    Session["kittingSmmary"] = kittingSmmary;
                    gvKitting.DataSource = kittingSmmary;
                    gvKitting.DataBind();
                }
            }
        }


        protected void lnkBOXID_Command(object sender, CommandEventArgs e)
        {
            //string fulfillmentNo = "", boxID = "";
            string parameters = Convert.ToString(e.CommandArgument);
            string[] array = parameters.Split(',');
            if (array != null && array.Length > 0)
                Session["fulfillmentNo"] = array[0];
            if (array != null && array.Length > 1)
                Session["boxID"] = array[1];
            if (array != null && array.Length > 2)
                Session["PalletID"] = array[2];

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../POKitting.aspx')</script>", false);

           // Response.Redirect("~/POKitting.aspx");
        }


        private void DownloadPDF()
        {
            List<PurchaseOrderKittingSummary> kittingSmmary = Session["kittingSmmary"] as List<PurchaseOrderKittingSummary>;
            if(kittingSmmary != null && kittingSmmary.Count > 0)
            {

    //            var results1 = from p in kittingSmmary
    //                           group p.BoxID by p.ContainerID, into g
    //                          select new { PersonId = g.Key, Cars = g.ToList() };

    //            var results = kittingSmmary.GroupBy(
    //p => p.ContainerID,
    //p => p.SKU,
    //p => p.BoxID,
    //(key, key, g) => new { ContainerID = key, SKU = key, Cars = g.ToList() });

            }
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
        //List<PurchaseOrderKittingSummary> kittingSmmary
        public List<PurchaseOrderKittingSummary> Sort<TKey>(List<PurchaseOrderKittingSummary> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PurchaseOrderKittingSummary>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PurchaseOrderKittingSummary>();
            }
        }
        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlUser.SelectedIndex = 0;
               
            txtDateFrom.Text="";
            txtDateTo.Text="";
            txtESN.Text="";
            txtBoxNo.Text="";
            txtPONum.Text="";
            lblCount.Text="";
            gvKitting.DataSource = null;
            gvKitting.DataBind();
            lblMsg.Text = "";
            pnlPO.Visible = false;

        }
    }
}