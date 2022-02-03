using System;
using System.Data;
using System.IO;
using System.Configuration;
using avii.Classes;

namespace avii.RMA
{
    public partial class RMAList : System.Web.UI.Page
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
                        ////companyID + "~" + rmanumber.Text + "~" + txtRMADate.Text + "~" + Status + "~" + UPC + "~" + esn
                        string[] searchArr = searchCriteria.Split('~');
                        int companyID = Convert.ToInt32(searchArr[0]);
                        string rmanumber = searchArr[1].ToString();
                        string rMADate = searchArr[2].ToString();
                        int status = Convert.ToInt32(searchArr[3].ToString());
                        string upc = searchArr[4].ToString();
                        string esn = searchArr[5].ToString();
                        string rMADateTo = searchArr[6].ToString();

                        DataTable dt = rmaUtility.GetRMAReport(rmanumber, rMADate, rMADateTo, status, companyID, upc, esn);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            lblRowCount.Text = dt.Rows.Count.ToString();
                            rptRMA.DataSource = dt;
                            rptRMA.DataBind();
                            Session["rmareport"] = dt;
                        }
                    }
                    else
                    {
                        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "formclose", "alert('No RMA exists for selected criteria, please search again...');form.close())");
                        //lblMsg.Text = "Session Expire!";
                    }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }
        private void DownloadCSV()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("RMA#,RMA Date,Customer,Location,SKU#,ESN,Defect Reason,Status,RMA Comments,Lan Global Comments\n");
            DataTable dt = new DataTable();
            dt = Session["rmareport"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(clsGeneral.getColumnData(dr, "RmaNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "RmaDate", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "CompanyName", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ContactCity", string.Empty, false) + ",");

                   // sb.Append(clsGeneral.getColumnData(dr, "SalesOrderNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ItemCode", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ESN", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ReasonTxt", string.Empty, false) + ",");
                  //  sb.Append(clsGeneral.getColumnData(dr, "CallTime", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "RMAStatus", string.Empty, false) + ",");

                    //sb.Append(clsGeneral.getColumnData(dr, "DateReceived", string.Empty, false) + ",");
                    //sb.Append(clsGeneral.getColumnData(dr, "InboundTrackingNumber", string.Empty, false) + ",");
                    //sb.Append(clsGeneral.getColumnData(dr, "QTYReceived", string.Empty, false) + ",");
                    //sb.Append(clsGeneral.getColumnData(dr, "DateCompleted", string.Empty, false) + ",");
                    //sb.Append(clsGeneral.getColumnData(dr, "ShipmentToCustomerTrackingNumber", string.Empty, false) + ",");
                    //sb.Append(clsGeneral.getColumnData(dr, "ESNShippedToCustomer", string.Empty, false) + ",");

                    sb.Append(clsGeneral.getColumnData(dr, "Comment", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "AVComments", string.Empty, false) + "\n");

                }

                try
                {
                    string downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
                    string path = Server.MapPath(downLoadPath).ToString();
                    string fileName = Session.SessionID + ".csv";
                    System.IO.FileInfo file = null;
                    file = new System.IO.FileInfo(path + fileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }

                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    // Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();

                }
                catch (Exception ex)
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "formclose", "alert('" + ex.Message + "');form.close())");
                }
            }
        }
        private void DownloadCSVOld()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("RMA#,RMA Date, Customer,Location,SO#,SKU#,ESN,Defect Reason,Call_Time,Status,DateReceived,InboundTrackingNumber,QTYReceived,DateCompleted,ShipmentToCustomerTrackingNumber,ESNShippedToCustomer,Notes,WSA Notes\n");
            DataTable dt = new DataTable();
            dt = Session["rmareport"] as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(clsGeneral.getColumnData(dr, "RmaNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "RmaDate", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "CompanyName", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ContactCity", string.Empty, false) + ",");

                    sb.Append(clsGeneral.getColumnData(dr, "SalesOrderNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ItemCode", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ESN", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ReasonTxt", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "CallTime", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "RMAStatus", string.Empty, false) + ",");

                    sb.Append(clsGeneral.getColumnData(dr, "DateReceived", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "InboundTrackingNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "QTYReceived", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "DateCompleted", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ShipmentToCustomerTrackingNumber", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "ESNShippedToCustomer", string.Empty, false) + ",");

                    sb.Append(clsGeneral.getColumnData(dr, "Comment", string.Empty, false) + ",");
                    sb.Append(clsGeneral.getColumnData(dr, "AVComments", string.Empty, false) + "\n");

                }

                try
                {
                    string downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
                    string path = Server.MapPath(downLoadPath).ToString();
                    string fileName = Session.SessionID + ".csv";
                    System.IO.FileInfo file = null;
                    file = new System.IO.FileInfo(path + fileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }

                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    // Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();

                }
                catch (Exception ex)
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "formclose", "alert('" + ex.Message + "');form.close())");
                }
            }
        }
    }
}