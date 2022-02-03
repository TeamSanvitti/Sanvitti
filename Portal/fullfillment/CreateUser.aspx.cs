using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

public partial class Admin_CreateUser : System.Web.UI.Page
{
    const string queryInsert = @"Aero_InsertUserInfo";
    const string queryGet = "Aero_GetUserInfo";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["UserID"]))
        {
            string url = "/logon.aspx";
            try
            {
                url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
            }
            catch
            {
                url = "/logon.aspx";
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect(url);
            }
            else
            {
                lblMsg.Text = string.Empty;
                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@UserID", Request.QueryString["UserID"]);
                DataSet dsUser = (DataSet)SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(connectionString,
                    CommandType.StoredProcedure, queryGet, param);

                if (dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                {
                    txtUsername.Text = dsUser.Tables[0].Rows[0]["Username"].ToString();
                    txtPassword.Attributes.Add("value", dsUser.Tables[0].Rows[0]["Password"].ToString());
                    txtConfirmPassword.Attributes.Add("value", dsUser.Tables[0].Rows[0]["Password"].ToString());
                    txtEmail.Text = dsUser.Tables[0].Rows[0]["Email"].ToString();
                    txtCompanyName.Text = dsUser.Tables[0].Rows[0]["CompanyName"].ToString();
                }
            }
        }
    }

    private void Cleanupform()
    {
        txtUsername.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtCompanyName.Text = string.Empty;
        this.txtConfirmPassword.Text = string.Empty;
    }
   private void UpdateLoginInfo(int userID)
    {
        int returnValue = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@Username", txtUsername.Text.Trim());
        param[1] = new SqlParameter("@Password", GetEncryptedPassword(txtPassword.Text.Trim()));
        param[2] = new SqlParameter("@email", txtEmail.Text.Trim());
        param[3] = new SqlParameter("@CompanyName", txtCompanyName.Text.Trim());
        param[4] = new SqlParameter("@UserID", userID);

        SQLLAYER.DataTransaction.SQLLAYER.ExecuteNonQuery(connectionString, CommandType.StoredProcedure,
            "Aero_UpdateUserInfo", param);
    }

    protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("ManagerUser.aspx");
    }

    protected void txtUsername_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string username, password, email, companyName;

            username = this.txtUsername.Text.Trim();
            password = txtPassword.Text.Trim();
            email = txtEmail.Text.Trim();
            companyName = txtCompanyName.Text.Trim();

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                lblMsg.Text = "Please enter username and password";
            }
            else
            {
                int returnValue = 0;
                lblMsg.Text = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["UserID"]))
                {
                    this.UpdateLoginInfo(Convert.ToInt32(Request.QueryString["UserID"]));
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Username", username);
                param[1] = new SqlParameter("@Password", password);
                param[2] = new SqlParameter("@email", email);
                param[3] = new SqlParameter("@CompanyName", companyName);

                returnValue = Convert.ToInt32(SQLLAYER.DataTransaction.SQLLAYER.ExecuteScalar(connectionString, CommandType.StoredProcedure,
                    queryInsert, param));

                if (returnValue >= 1 || returnValue == -1)
                {
                    lblMsg.Text = "This username already exists, please select another";
                }
                else
                {
                    lblMsg.Text = "Data successfully saved";
                    Cleanupform();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private string GetEncryptedPassword(string password)
    {
        Byte[] data1ToHash = ConvertStringToByteArray(password);
        byte[] hashvalue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(data1ToHash);
        return BitConverter.ToString(hashvalue);
    }

    private static Byte[] ConvertStringToByteArray(String s)
    {
        return (new UnicodeEncoding()).GetBytes(s);
    }
}
