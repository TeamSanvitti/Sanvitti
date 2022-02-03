using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

namespace avii.RMA
{
    public partial class esnpopup : System.Web.UI.Page
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
            }
            if (!Page.IsPostBack)
            {
                string qstring = Request["qstring"].ToString();
                string[] qst = qstring.Split(',');
                int companyID = Convert.ToInt32(qst[0]);
                string esn = qst[1].ToString();
                //avii.Classes.RMAUtility rmaobj = new avii.Classes.RMAUtility();
                List<avii.Classes.RMADetail> esnlist = avii.Classes.RMAUtility.getRMAesn(companyID, esn, "", 0, 0, 0);
                GVEsn.DataSource = esnlist;
                GVEsn.DataBind();
            }
            
        }

      
    }
}
