using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class ucForecastDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindForecastDetail(int forecastID)
        {
            lblMsg.Text = string.Empty;
            List<ForecastLineItem> forecastDetailList = SKUPricesOperations.GetForecastDetail(forecastID);
            if (forecastDetailList != null && forecastDetailList.Count > 0)
            {
                rptItem.DataSource = forecastDetailList;
                rptItem.DataBind();
            }
            else
            {
                lblMsg.Text = "no records found";
                rptItem.DataSource = null;
                rptItem.DataBind();
            }

        }
    }
}