using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Text;
using System.IO;


namespace avii.Controls
{
    public partial class RmaEsnDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            if (Session["rmarsnstatuses"] != null)
            {
                List<RmaEsnStatuses> rmaStatusList = (Session["rmarsnstatuses"]) as List<RmaEsnStatuses>;
                gvRMA.DataSource = rmaStatusList;

            }
            else
                gvRMA.DataSource = null;
                gvRMA.DataBind();
        }
        public void PopulateData(int companyID, int timeInterval, int esnStatusID, int rmaStatusID)
        {

            if (timeInterval == 0)
            {
                //lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblRMA.Text = string.Empty;
                Session["rmarsnstatuses"] = null;
            }
            else
            {
                List<RmaEsnStatuses> rmaStatusList = ReportOperations.GetCustomerRmaEsnStatusReport(companyID, timeInterval, esnStatusID, rmaStatusID);

                Session["rmarsnstatuses"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    //lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                    lblRMA.Text = string.Empty;
                    btnPrint.Visible = true;
                    btnDownload.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnDownload.Visible = false;
                    //lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            gvRMA.DataBind();

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadRmaEsnInfo();
        }
        private void DownloadRmaEsnInfo()
        {
            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["RMAFilesStorage"].ToString();

            List<RmaEsnStatuses> rmaStatusList = (Session["rmarsnstatuses"]) as List<RmaEsnStatuses>;

            if (rmaStatusList != null && rmaStatusList.Count > 0)
            {

                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = "Esn" + Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("ESN,EsnStatus,RMA#,RmaDate,RmaStatus\r\n");

                foreach (avii.Classes.RmaEsnStatuses rmaESN in rmaStatusList)
                {
                    sb.Append(rmaESN.ESN + ","
                                + rmaESN.EsnStatus + ","
                                + rmaESN.RmaNumber + ","
                                + rmaESN.RmaDate.ToShortDateString() + ","
                                + rmaESN.RmaStatus
                                + "\r\n");

                    found = true;
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblRMA.Text = ex.Message;
                }

                if (found)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }

    }
}