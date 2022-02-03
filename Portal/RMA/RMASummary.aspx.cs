using System;
using System.Configuration;
using System.Data;

namespace avii.RMA
{
    public partial class RMASummary : System.Web.UI.Page
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
            
            if (!IsPostBack)
            {
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

                string searchCriteria = Session["searchRma"] as string;
                if (searchCriteria != null)
                {
                    string[] searchArr = searchCriteria.Split('~');
                    int companyID = Convert.ToInt32(searchArr[0]);
                    string rmanumber = searchArr[1].ToString();
                    string rMADate = searchArr[2].ToString();
                    int status = Convert.ToInt32(searchArr[3].ToString());
                    string upc = searchArr[4].ToString();
                    string esn = searchArr[5].ToString();
                    string rMADateTo = searchArr[6].ToString();

                    DataTable dt = rmaUtility.GetRMASummary(rmanumber, rMADate, rMADateTo, status, companyID, upc, esn);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int count, totalcount = 0;
                        
                        rptRMA.DataSource = dt;
                        rptRMA.DataBind();
                        
                        foreach (DataRow drow in dt.Rows)
                        {
                            count = 0;
                            if (drow["RmaCount"] != null && int.TryParse(drow["RmaCount"].ToString(), out count))
                                totalcount = totalcount + count;
                        }

                        lblRowCount.Text = totalcount.ToString();
                    }
                }
                else
                {
                    //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "formclose", @"<script type=""text/javascript"">alert('No RMA exists for selected criteria, please search again...');window.close();</script>");
                }
            }
        }
    }
}