using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
//using SQLLAYER.DataTransaction;

namespace avii.fullfillment.User
{
    public partial class ManageUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                string url = "/logon.aspx";
                if (Session["adm"] == null)
                {
                    try
                    {
                        url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
                    }
                    catch
                    {
                        url = "/logon.aspx";
                    }
                    Response.Redirect(url + "?usr=1");
                }
                else if (Session["adm"] != null)
                {
                    this.PopulateGrid();
                }
            }
        }

        private void PopulateGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            gvUserComments.DataSource = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(connectionString, CommandType.StoredProcedure,
                "Aero_GetAllUsers");
            gvUserComments.DataBind();
        }

        protected void gvUserComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void gvUserComments_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }
        protected void gvUserComments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void DeleteLoginInfo(int userID)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@UserID", userID);
                
                int retvalue = SQLLAYER.DataTransaction.SQLLAYER.ExecuteNonQuery(connectionString, CommandType.StoredProcedure,
                    "Aero_DeleteUserInfo", param);
                if (retvalue < 0)
                {
                    lblMsg.Text = "User has uploaded the files. Please delete all those uploads first before deletion";
                }
                else
                {
                    PopulateGrid();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void gvUserComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //DataKey datakey = gvUserComments.DataKeys[e.RowIndex];
            this.DeleteLoginInfo(Convert.ToInt32(e.CommandArgument));
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"/fullfillment/CreateUser.aspx");
        }
    }

}
