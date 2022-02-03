using System;
using System.Configuration;
using System.Web.UI;

namespace avii
{
    public partial class ItemSummary : System.Web.UI.Page
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
                pnlCompany.Visible = false;

            }

            else
            {

                pnlCompany.Visible = true;
            }
                if (!IsPostBack)
                {
                    DateTime today = DateTime.Today;
                    DateTime yearEarlier = today.AddDays(-365);

                    txtFromDate.Text = yearEarlier.ToShortDateString();
                    txtToDate.Text = DateTime.Today.ToShortDateString();
                    if (Session["adm"] != null)
                    {
                        bindCustomerDropDown();
                        hdnAdmin.Value = "admin";
                    }
                }
            
        }

        protected void btnEsnList_Click(object sender, EventArgs e)
        {
            pnlItems.EnableViewState = false;
            Control tmp = LoadControl("./controls/AssignedEsnList.ascx");
            avii.Controls.AssignedEsnList ctlItem = tmp as avii.Controls.AssignedEsnList;
            int userID = 0;
            int companyID = 0;
            if (Session["adm"] != null)
            {
                
                userID = 0;
            }
            else
            {
                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null && userInfo.UserGUID > 0)
                    {
                        userID = userInfo.UserGUID;

                        companyID = userInfo.CompanyGUID;
                    }
                    

                }
            }
            if (tmp != null)
            {
                
                DateTime startDate, endDate;
                DateTime.TryParse(txtFromDate.Text.Trim(), out startDate);
                DateTime.TryParse(txtToDate.Text.Trim(), out endDate);

                ctlItem.UserID = userID;
                ctlItem.ItemCode = txtSku.Text.Trim();
                ctlItem.StartDate = startDate;
                if (dpCompany.Visible && dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                ctlItem.CompanyID = companyID;
                ctlItem.EndDate = endDate;
                if (txtFromDate.Text.Trim() == string.Empty && txtToDate.Text.Trim() == string.Empty && txtSku.Text.Trim() == string.Empty && companyID == 0)
                {
                    lblMsg.Text = "Please select the search criteria";
                }
                else
                    ctlItem.LoadData();
            }

            pnlItems.Controls.Add(ctlItem);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pnlItems.EnableViewState = false;
            if (Session["UserID"] != null || Session["adm"] != null)
            {
                Control tmp = LoadControl("./controls/ctlItemSummary.ascx");
                avii.Controls.ctlItemSummary ctlItem = tmp as avii.Controls.ctlItemSummary;

                if (tmp != null)
                {
                    DateTime startDate, endDate;
                    DateTime.TryParse(txtFromDate.Text.Trim(), out startDate);
                    DateTime.TryParse(txtToDate.Text.Trim(), out endDate);

                    ctlItem.UserID = Convert.ToInt32(Session["UserID"]);
                    ctlItem.ItemCode = txtSku.Text.Trim();
                    ctlItem.StartDate = startDate;
                    if (dpCompany.Visible && dpCompany.SelectedIndex > 0)
                    {
                        ctlItem.CompanyID = Convert.ToInt32(dpCompany.SelectedValue);
                    }
                    ctlItem.EndDate = endDate;
                    ctlItem.LoadData();
                }

                pnlItems.Controls.Add(ctlItem);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        private void CleanForm()
        {
            txtSku.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            dpCompany.SelectedIndex = -1;
        }
    }
}
