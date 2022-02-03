using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class RmaReasonSummary : System.Web.UI.UserControl
    {
        private int timeInterval = 30;
        private string companyAccountNumber = string.Empty;
        public int TimeInterval
        {
            get
            {
                return timeInterval;
            }
            set
            {
                timeInterval = value;
            }
        }
        public string CompanyAccountNumber
        {
            get
            {
                return companyAccountNumber;
            }
            set
            {
                companyAccountNumber = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
            }
            else
            {
                ReloadData();
            }
        }
        public void LoadData()
        {
            PopulateData();
        }

        public void PopulateData()
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //CompanyAccountNumber = userInfo.CompanyAccountNumber;

                }

            }
            if (string.IsNullOrEmpty(CompanyAccountNumber) && TimeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblPO.Text = string.Empty;
                Session["rmareason"] = null;
            }
            else
            {
                List<ProductRmaReason> rmaSummaryList = ReportOperations.GetProductRMAReasonSummary(CompanyAccountNumber, TimeInterval, 0);

                Session["rmareason"] = rmaSummaryList;
                if (rmaSummaryList.Count > 0)
                {
                    gvRMA.DataSource = rmaSummaryList;
                    lblCount.Text = "<strong>Total products:</strong> " + rmaSummaryList.Count.ToString();
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            gvRMA.DataBind();
        }
        public void ReloadData()
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //CompanyID = userInfo.CompanyGUID;

                }

            }
            List<ProductRmaReason> rmaSummaryList = null;
            if (Session["rmareason"] != null)
            {
                //  rmaSummaryList = ReportOperations.GetProductRMAReasonSummary(CompanyAccountNumber, TimeInterval);
                //else
                rmaSummaryList = (List<ProductRmaReason>)Session["rmareason"];


                Session["rmareason"] = rmaSummaryList;
                if (rmaSummaryList.Count > 0)
                {
                    lblCount.Text = "<strong>Total products:</strong> " + Convert.ToString(rmaSummaryList.Count - 1);
                    gvRMA.DataSource = rmaSummaryList;
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblPO.Text = string.Empty; ;
            }
            gvRMA.DataBind();
        }

        protected void lnkDOA_OnCommand(object sender, CommandEventArgs e)
        {
            //string poNum = e.CommandArgument.ToString();
            //string companyAccountNumber = dpCompany.SelectedValue;
            //if (!string.IsNullOrEmpty(poNum))
            //{

            //    Control tmp1 = LoadControl("../controls/PODetails.ascx");
            //    avii.Controls.PODetails ctlPODetails = tmp1 as avii.Controls.PODetails;
            //    pnlPO.Controls.Clear();
            //    ctlPODetails.BindPO(poNum, companyAccountNumber);

            //    pnlPO.Controls.Add(ctlPODetails);

            //    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



            //}
            
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;
            ReloadData();

        }
    }
}