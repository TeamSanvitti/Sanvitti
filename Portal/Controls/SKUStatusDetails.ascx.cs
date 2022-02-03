using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using avii.Classes;
namespace avii.Controls
{
    public partial class SKUStatusDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
            }
        }
        private void PopulateData()
        {
            int companyID = 0;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;

                }

            }
            if (Session["sku"] != null)
            {
                string sku = (string)Session["sku"];
                List<SKUsPOStatus> assignedSKUsList = DashboardOperations.GetAssignedSKUsPOStatusSummary(companyID, sku);
                if (assignedSKUsList.Count > 0)
                {
                    rptSKU.DataSource = assignedSKUsList;
                    lblSKU.Text = string.Empty;
                }
                else
                {
                    rptSKU.DataSource = null;
                    lblSKU.Text = "No records found";
                }
                rptSKU.DataBind();
                Session["sku"] = null;
            }
            else
            { 
                
            }
        }
    }
}