using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class ForecastHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindForecastLog(int forecastID)
        {
            lblFcLog.Text = string.Empty;
            List<ForecastLog> forecastLogList = SKUPricesOperations.GetFulfillmentForecastLog(forecastID);
            if (forecastLogList != null && forecastLogList.Count > 0)
            {
                rptFClog.DataSource = forecastLogList;
                rptFClog.DataBind();
            }
            else
            {
                lblFcLog.Text = "no records found";
                rptFClog.DataSource = null;
                rptFClog.DataBind();
            }

        }
    }
}