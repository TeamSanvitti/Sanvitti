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

namespace avii
{
    public partial class POChangeStatus : System.Web.UI.Page
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
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            ddlCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();

        }
        private void ClearForm()
        {
            lblMsg.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            UpdatePOChangeStatus();
            ddlStatus.SelectedIndex = 0;
        }
        private void UpdatePOChangeStatus()
        {
            //string[] validFileExtensions = new string[] { "xls" };
            int statusID = 1;
            int companyID = 0;
            string fileDir;
            string filePath;
            string fileExtension;
            bool flag = false;
            int poRecordCount = 0;
            int userID = 0;
            string poNums = string.Empty;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            string poNum = string.Empty;
            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
            {
                lblMsg.Text = "Status is required!";
                return;
            }
            if (ddlCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
            {
                lblMsg.Text = "Customer is required!";
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
                            DataTable poTable = new DataTable();
                            List<avii.Classes.FulfillmentChangeStatus> poList;
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
                                objAdapter1.Fill(poTable);
                                objConn.Close();
                            }

                            //DataTable storeTable = CompanyOperations.GetImportStoreList(filePath);
                            if (poTable.Rows.Count > 0)
                            {
                                poList = new List<avii.Classes.FulfillmentChangeStatus>();
                                StringBuilder storeIdList = new StringBuilder();
                                for (int i = 0; i < poTable.Columns.Count; i++)
                                {
                                    poTable.Columns[i].ColumnName = poTable.Columns[i].ColumnName.Trim();
                                }
                                if (poTable.Columns[0].ColumnName.Trim().ToLower() != "ponum")
                                {
                                    lblMsg.Text = "Invalid format";
                                    return;
                                }
                                foreach (DataRow item in poTable.Rows)
                                {
                                    try
                                    {
                                        poNum = Convert.ToString(clsGeneral.getColumnData(item, "ponum", string.Empty, false));

                                        if (!string.IsNullOrEmpty(poNum) && poNum.Trim().Length > 0)
                                        {

                                            avii.Classes.FulfillmentChangeStatus poObj = new FulfillmentChangeStatus();
                                            poObj.FulfillmentOrder = poNum.Trim();

                                            poList.Add(poObj);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errors.Append(ex.Message + "\n");
                                    }
                                }

                                if (poList != null && poList.Count > 0)
                                {

                                    avii.Classes.PurchaseOrder.UpdateFulfillmentChangeStatus(poList, companyID, statusID, userID, out poRecordCount, out poNums);
                                    if (errors.Length == 0)
                                    {
                                        lblMsg.Text = poRecordCount + " records updated successfully ";
                                        if (poNums != null && poNums != string.Empty)
                                        {
                                            lblMsg.Text = poRecordCount + " Fulfillment Orders are successfully changed. <br /> " + poNums + " Fulfillment Order(s) <list of order> are missing. Please verify them before upload.";
                                        }
                                    }
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