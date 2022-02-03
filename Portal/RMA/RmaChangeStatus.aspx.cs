using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using avii.Classes;

namespace avii.RMA
{
    public partial class RmaChangeStatus : System.Web.UI.Page
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
        }
        private void ClearForm()
        {
            lblMsg.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            UpdateRmaChangeStatus();
            ddlStatus.SelectedIndex = 0;
        }
        private void UpdateRmaChangeStatus()
        {
            //string[] validFileExtensions = new string[] { "xls" };
            int statusID = 1;
            string fileDir;
            string filePath;
            string fileExtension;
            bool flag = false;
            int poRecordCount = 0;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            string rmaNumber = string.Empty;
            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
            {
                lblMsg.Text = "Status is required!";
                return;
            }
            StringBuilder errors = new StringBuilder();
            try
            {
                string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] as string;
                if (flnUpload.HasFile)
                {
                    fileDir = Server.MapPath("~");
                    filePath = fileDir + uploadFolder + flnUpload.FileName;
                    fileExtension = System.IO.Path.GetExtension(flnUpload.FileName).Replace(".", string.Empty).ToLower();

                    //if (fileExtension == "xls")
                    if (fileExtension == "xls" || fileExtension == "xlsx")
                        flag = true;

                    if (flag == true)
                    {
                        try
                        {
                            DataTable rmaTable = new DataTable();
                            List<avii.Classes.RMAChangeStatus> rmaList;
                            flnUpload.PostedFile.SaveAs(filePath);

                            String sConnectionString = string.Empty;
                            if (fileExtension == "xlsx")
                                sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source='" + filePath + "';" +
                                 "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                            else
                                if (fileExtension == "xls")
                                    sConnectionString =
                                           "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                           "Data Source=" + filePath + ";" +
                                           "Extended Properties=Excel 8.0;";


                            using (OleDbConnection objConn = new OleDbConnection(sConnectionString))
                            {
                                objConn.Open();
                                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
                                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                                objAdapter1.SelectCommand = objCmdSelect;
                                objAdapter1.Fill(rmaTable);
                                objConn.Close();
                            }

                            //DataTable storeTable = CompanyOperations.GetImportStoreList(filePath);
                            if (rmaTable.Rows.Count > 0)
                            {
                                rmaList = new List<avii.Classes.RMAChangeStatus>();
                                StringBuilder storeIdList = new StringBuilder();
                                for (int i = 0; i < rmaTable.Columns.Count; i++)
                                {
                                    rmaTable.Columns[i].ColumnName = rmaTable.Columns[i].ColumnName.Trim();
                                }
                                if (rmaTable.Columns[0].ColumnName.Trim().ToLower() != "rmanumber")
                                {
                                    lblMsg.Text = "Invalid format";
                                    return;
                                }
                                foreach (DataRow item in rmaTable.Rows)
                                {
                                    try
                                    {
                                        rmaNumber = Convert.ToString(clsGeneral.getColumnData(item, "rmaNumber", string.Empty, false));

                                        if (!string.IsNullOrEmpty(rmaNumber) && rmaNumber.Trim().Length > 0)
                                        {

                                            avii.Classes.RMAChangeStatus rmaObj = new RMAChangeStatus();
                                            rmaObj.RmaNumber = rmaNumber.Trim();

                                            rmaList.Add(rmaObj);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errors.Append(ex.Message + "\n");
                                    }
                                }

                                if (rmaList != null && rmaList.Count > 0)
                                {

                                    RMAUtility.UpdateRMAChangeStatus(rmaList, statusID, userID, out poRecordCount);
                                    if (errors.Length == 0)
                                        lblMsg.Text = poRecordCount + " records updated successfully";
                                    else
                                        lblMsg.Text = poRecordCount + " records updated successfully with " + errors.ToString();
                                }
                                else
                                    lblMsg.Text = "No records found in the file to update";

                            }
                            else
                                lblMsg.Text = "No records found in the file to update";

                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Invalid File Extension!";
                    }
                }
                else
                    lblMsg.Text = "Select a file!";

            }
            catch
            {
                lblMsg.Text = "File Data not in correct format!";
            }
        }
        
    }
}