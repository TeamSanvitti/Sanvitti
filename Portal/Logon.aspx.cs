using avii.Classes;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.Services;

namespace avii
{
	/// <summary>
	/// Summary description for Logon.
	/// </summary>
	public class Logon : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnlogon;
		protected System.Web.UI.WebControls.TextBox txtUser;
        protected System.Web.UI.WebControls.Label lblFull;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPwd;
        //protected System.Web.UI.WebControls.CheckBox chkFull;
		//private Classes.clsCust oCust = new Classes.clsCust();
		protected System.Web.UI.WebControls.Button Button1;
		//private DataTable oDt;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
                //SV.Framework.Fulfillment.UEDFOperation.CreateUEDFFile();

                //SV.Framework.LabelGenerator.AddressValidationOperation operation = new SV.Framework.LabelGenerator.AddressValidationOperation();
                //operation.ValidateAddress();

                if (Session["userInfo"] != null)
                {
                    string returnURL = "~/home.aspx";
                    Response.Redirect(returnURL);
                }
                    if (Request.Params["usr"] != null)
                {
                    ViewState["adm"] = "y";
                    
                    //this.chkFull.Visible = false;
                    //this.lblFull.Visible = false;
                }
                else
                    ViewState["adm"] = null;
			}
		}


        protected static string ReCaptcha_Key = "6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T";
        protected static string ReCaptcha_Secret = "6LeblVwUAAAAAKytE2q3Wl10cTSrGVjygtyxg_-9";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }
        [WebMethod]
        public static void UpdateSession()
        {
            if (HttpContext.Current.Session["currentdate"] != null)
            {
                HttpContext.Current.Session["currentdate"] = DateTime.Now; 
            }

        }
        [WebMethod]
        public static void ExtendSession()
        {
            if (HttpContext.Current.Session["username"] != null)
            {
                string usrName = HttpContext.Current.Session["username"] as string;// = txtUser.Text.Trim();
                string password = HttpContext.Current.Session["password"] as string;// txtPwd.Value.Trim();
                Exception ex = null;
                string comments = "Session extented";
                string source = "WEB";

                avii.Classes.UserCredentials authentication = new avii.Classes.UserCredentials();
                authentication.UserName = usrName;
                authentication.Password = password;
                avii.Classes.CredentialValidation validation = avii.Classes.AuthenticationOperation.ValidateUser(authentication, source, comments, out ex);
                avii.Classes.user_utility objUser = new avii.Classes.user_utility();
                string UserType = System.Configuration.ConfigurationManager.AppSettings["UserType"].ToString();

                avii.Classes.UserInfo objUserInfo = objUser.getUserInfo(validation.UserID);
                HttpContext.Current.Session["userInfo"] = objUserInfo;
                //Session["User"] = reader["Username"];
                //Session["Username"] = reader["Username"];
                HttpContext.Current.Session["LogoPath"] = objUserInfo.LogoPath;
                HttpContext.Current.Session["MenuCss"] = objUserInfo.MenuCss;
                HttpContext.Current.Session["StyleCss"] = objUserInfo.StyleCss;
                HttpContext.Current.Session["UserID"] = objUserInfo.UserGUID;
            }
            //Session["email"] = reader["email"];
            //Session["CompanyID"] = reader["CompanyID"];
            //System.Web.Security.FormsAuthentication.SetAuthCookie(HttpContext.Current.User.Identity.Name, false);
            //var data = new { IsSuccess = true };
            //return data.IsSuccess;
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnlogon.Click += new System.EventHandler(this.btnlogon_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	    private void btnlogon_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                string returnURL = "~/home.aspx";
                //avii.Classes.user_utility objUser = new avii.Classes.user_utility();
                //List<avii.Classes.UserInfo> userList = new List<avii.Classes.UserInfo>();

                bool bFound = false;
                if (txtUser.Text.Trim().Length > 0 && txtPwd.Value.Trim().Length > 0)
                {
                    //if ("customer1505" == txtUser.Text.Trim().ToLower() && "2265order" == txtPwd.Value.Trim().ToLower())
                    //{

                    //    this.Page.RegisterStartupScript("logon", "<script language=javascript>StopLogin();</script>");

                    //}
                    //else if ("thubert" == txtUser.Text.Trim().ToLower() && "Kentucky1" == txtPwd.Value.Trim().ToLower())
                    //{
                    //    this.Page.RegisterStartupScript("logon", "<script language=javascript>StopLogin();</script>");
                    //}

                    //else
                    {
                        bFound = FullfillmentLogOn(ref returnURL);
                        if (bFound)
                            Response.Redirect(returnURL);
                    }
                }
                else
                    bFound = false;

                if (bFound == false)
                    this.Page.RegisterStartupScript("logon", "<script language=javascript>alert('Please enter correct Username and Password to logon to the website');</script>");
                else
                    Response.Redirect(returnURL);
            }

        }

        private bool FullfillmentLogOn(ref string returnURL)
        {
            bool returnResult = false;
            int accountstatusid = 0;
            string Encrypted = string.Empty;
            Session["username"] = txtUser.Text.Trim();
            Session["password"] = txtPwd.Value.Trim();

            avii.Classes.user_utility objUser = new avii.Classes.user_utility();
            string UserType = System.Configuration.ConfigurationManager.AppSettings["UserType"].ToString();
            string query = "Aero_AuthenticateUser";
            SqlDataReader reader = null;
            try
            {

                //string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                //SqlParameter[] param = new SqlParameter[3];
                //param[0] = new SqlParameter("@Username", txtUser.Text.Trim());
                //param[1] = new SqlParameter("@Password", txtPwd.Value.Trim());
                //param[2] = new SqlParameter("@SessionID", Session.SessionID);
                DataTable dt = new DataTable();
                DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                objCompHash.Add("@Username", txtUser.Text.Trim());
                objCompHash.Add("@Password", txtPwd.Value.Trim());
                objCompHash.Add("@SessionID", Session.SessionID);
                
                arrSpFieldSeq = new string[] { "@Username", "@Password", "@SessionID" };


                dt = db.GetTableRecords(objCompHash, "Aero_AuthenticateUser", arrSpFieldSeq);

               // reader = SQLLAYER.DataTransaction.SQLLAYER.ExecuteReader(connectionString, CommandType.StoredProcedure, query, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["UserType"].ToString() == UserType)
                        {
                            returnURL = @"~/home.aspx";
                            Session["adm"] = "y";
                        }
                        else
                        {
                            returnURL = "~/home.aspx";
                        }
                        accountstatusid = Convert.ToInt32(row["accountstatusid"]);
                        if (accountstatusid == 5)
                        {
                            Encrypted = Convert.ToString(row["Encrypted"]);
                            if (!string.IsNullOrWhiteSpace(Encrypted))
                                returnURL = "~/ForgotPassword.aspx?un="+ Encrypted;
                            else
                                returnURL = "~/ForgotPassword.aspx";
                        }
                        else
                        {
                            HttpContext.Current.Session["currentdate"] = DateTime.Now;
                            avii.Classes.UserInfo objUserInfo = objUser.getUserInfo(Convert.ToInt32(row["UserID"]));
                            Session["userInfo"] = objUserInfo;
                            //Session["User"] = reader["Username"];
                            //Session["Username"] = reader["Username"];
                            Session["LogoPath"] = objUserInfo.LogoPath;
                            Session["MenuCss"] = objUserInfo.MenuCss;
                            Session["StyleCss"] = objUserInfo.StyleCss;
                            Session["UserID"] = objUserInfo.UserGUID;
                            //Session["email"] = reader["email"];
                            //Session["CompanyID"] = reader["CompanyID"];
                            object isAdmin = row["IsAdmin"];
                            bool isAdminUser = false;
                            bool.TryParse(isAdmin as string, out isAdminUser);
                            if (isAdmin != null && (isAdminUser == true || isAdmin.ToString().Equals("1")))
                            {
                                Session["IsAdmin"] = row["IsAdmin"];
                            }
                        }
                        returnResult = true;
                    }
                }
                else
                {
                    returnResult = false;
                }
            }
            catch (Exception ex)
            {
                returnResult = false;
            }
            return returnResult;
        }
        //private bool FullfillmentLogOnOLD()
        //{
        //    string query = "Aero_AuthenticateUser";
        //    SqlDataReader reader = null;

        //    try
        //    {
        //        string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        //        SqlParameter[] param = new SqlParameter[2];
        //        param[0] = new SqlParameter("@Username", txtUser.Text.Trim());
        //        param[1] = new SqlParameter("@Password", txtPwd.Value.Trim());
        //        reader = SQLLAYER.DataTransaction.SQLLAYER.ExecuteReader(connectionString,
        //            CommandType.StoredProcedure, query, param);

        //        if (reader.Read())
        //        {
        //            Session["User"] = reader["Username"];
        //            Session["Username"] = reader["Username"];
        //            Session["UserID"] = reader["UserID"];
        //            Session["email"] = reader["email"];
        //            Session["CompanyID"] = reader["CompanyID"];
        //            object isAdmin = reader["IsAdmin"];
        //            //object defaultCustType = reader["CustTypeID"];
        //            if (isAdmin != null && isAdmin.ToString().Equals("1"))
        //            {
        //                Session["IsAdmin"] = reader["IsAdmin"];
        //            }
     
        //            //if (!string.IsNullOrEmpty( defaultCustType.ToString()))
        //            //{
        //            //    Session["DefCust"] = defaultCustType.ToString();
        //            //}

                    
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch  (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        protected void btnlogon_Click1(object sender, EventArgs e)
        {

        }
	}
}
