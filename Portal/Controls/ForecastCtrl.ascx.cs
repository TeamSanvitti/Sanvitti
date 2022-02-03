using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using avii.Classes;

namespace avii.Controls
{
    public partial class ForecastCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadData(Forecast forecast)
        {
            txtQty.Text = forecast.ForecastQty.ToString();
            lblDate.Text = forecast.ForecastDate.ToShortDateString();
            lblStatus.Text = forecast.ForecastStatus.ToString();
        }

        public void SetData(ref Forecast forecast)
        {
            forecast.ForecastQty = Convert.ToInt32(txtQty.Text);
            forecast.ForecastDate = Convert.ToDateTime(lblDate.Text);
        }



        public void ClearData()
        {
            txtQty.Text = "0";
        }
    }
}