using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class EsnLogDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            if (Session["esnList"] != null)
            {
                List<ESNLog> esnList = (Session["esnList"]) as List<ESNLog>;
                if (esnList != null && esnList.Count > 0)
                {
                    gvEsn.DataSource = esnList;
                    gvEsn.DataBind();
                    Session["esnList"] = esnList;
                }
            }
        }
        public void BindEsn(string esn)
        {
            lblEsn.Text = string.Empty;
            List<ESNLog> esnList = null;
            if (!string.IsNullOrEmpty(esn))
                esnList = MslOperation.GetEsnLog(esn);
            //DataTable dataTable = avii.Classes.clsInventoryDB.GetEsnLogList(esn);
            if (esnList != null && esnList.Count > 0)
            {
                gvEsn.DataSource = esnList;
                gvEsn.DataBind();
                Session["esnList"] = esnList;
            }
            else
            {
                Session["esnList"] = null;
                gvEsn.DataSource = null;
                gvEsn.DataBind();
                lblEsn.Text = "No record exists";
            }
        }
        protected void gvEsn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEsn.PageIndex = e.NewPageIndex;
            Reload();
        }
    }
}