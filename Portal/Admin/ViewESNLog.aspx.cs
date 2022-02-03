using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Avii.Admin
{
    public partial class ViewESNLog : System.Web.UI.Page
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                if (Request["esn"] != null)
                {
                    string esn = Convert.ToString(Request["esn"]);
                    ViewState["esn"] = esn;
                    BindEsn(esn);
                }
            }
        }
        private void BindEsn(string esn)
        {
            DataTable dataTable = avii.Classes.clsInventoryDB.GetEsnLogList(esn);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                gvEsn.DataSource = dataTable;
                gvEsn.DataBind();
            }
            else
            {
                
                gvEsn.DataSource = null;
                gvEsn.DataBind();
                lblMsg.Text = "No record exists";
            }
        }

        protected void gvEsn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string esn = string.Empty;
            gvEsn.PageIndex = e.NewPageIndex;
            if (ViewState["esn"] != null)
            {
                BindEsn(Convert.ToString(ViewState["esn"]));
            }
        }
    }
}