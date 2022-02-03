using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Admin
{
    public partial class ManageMSL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindCustomer();
                pnlMissingMsl.Visible = true;
                pnlAssignMSL.Visible = false;
            }
            
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnGetMissingMSL_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            lblMsg.Text = string.Empty;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

            List<EsnMslDetail> esnList = MslOperation.GetMissingMSL(companyID);
            if (esnList != null && esnList.Count > 0)
            {

                gvMSL.DataSource = esnList;
                Session["missingmsl"] = esnList;
                gvMSL.DataBind();
                //pnlMissingMsl.Visible = false;
                pnlAssignMSL.Visible = true;
                
            }
            else
            {
                gvMSL.DataSource = null;
                gvMSL.DataBind();
                Session["missingmsl"] = null;
                lblMsg.Text = "No records found";
                pnlMissingMsl.Visible = true;
                pnlAssignMSL.Visible = false;
            }

        }
        protected void gvMSL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMSL.PageIndex = e.NewPageIndex;
            //ReloadData();
            if (Session["missingmsl"] != null)
            {
                List<EsnMslDetail> esnList = Session["missingmsl"] as List<EsnMslDetail>;
                gvMSL.DataSource = esnList;
                //Session["missingmsl"] = esnList;
                gvMSL.DataBind();
               
            }

        }
        
        protected void btnAssignMSL_Click(object sender, EventArgs e)
        {
            int companyID, userID, poRecordCount;
            userID = companyID = poRecordCount = 0;
            string errorMessage = string.Empty;

            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            List<EsnMslDetail> esnList = MslOperation.AssignMSL2FulfillmentESN(string.Empty, userID, companyID, out poRecordCount, out errorMessage);
            if (esnList != null && esnList.Count > 0)
            {
                lblMsg.Text = "Updated successfully. Record count: " + esnList.Count;
                Session["missingmsl"] = esnList;
                gvMSL.DataSource = esnList;
                gvMSL.DataBind();
               

            }


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dpCompany.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            gvMSL.DataSource = null;
            gvMSL.DataBind();
            pnlMissingMsl.Visible = true;
            pnlAssignMSL.Visible = false;

        }
        
    }
}