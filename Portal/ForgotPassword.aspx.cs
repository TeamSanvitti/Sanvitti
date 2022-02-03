using System;
using System.Data;

namespace avii
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHeader.InnerHtml = "FORGOT PASSWORD";
                //btnLogin.Visible = false;
                if (Request["un"] != null)
                {
                    string userName = Request["un"].ToString().Replace("\"", "");
                    string decrypted = string.Empty;
                    try
                    {
                        decrypted = avii.Classes.CryptorEngine.Decrypt(userName, true);

                        ViewState["username"] = decrypted;

                        avii.Classes.SecurityCodeInfo securityCodeInfo = avii.Classes.ForgetPassword.GetTimeStamp(decrypted, userName);
                        if (securityCodeInfo != null && securityCodeInfo.UserID > 0)
                        {
                            ViewState["userid"] = securityCodeInfo.UserID;

                            if (securityCodeInfo.IsExpired < 1)
                            {
                                pnlRequestPwd.Visible = true;
                                pnlChangePwd.Visible = false;

                                lblMsg.Text = "Time limit has been expired. Please create another request";
                            }
                            else
                            {
                                if (securityCodeInfo.Used == 0)
                                {
                                    lblMessage.Text = "Please enter the security code sent to your register email address with in 24 hours.";
                                    pnlChangePwd.Visible = false;
                                    pnlRequestPwd.Visible = false;
                                    pnlLogin.Visible = true;
                                    lblHeader.InnerHtml = "Verify Security Code";
                                }
                                else
                                {
                                    lblMsg.Text = "This link is no longer available. Please create another request.";
                                }
                            }
                            pnLogin.Visible = false;
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            lblLogin.Text = "This link is no longer available. Please create another request.";
                            pnlRequestPwd.Visible = false;
                            pnlChangePwd.Visible = false;
                            pnlLogin.Visible = false;
                            pnLogin.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                        pnlRequestPwd.Visible = false;
                        pnlChangePwd.Visible = false;
                        pnlLogin.Visible = false;
                        pnLogin.Visible = false;
                    }
                }
                else
                {
                    pnlRequestPwd.Visible = true;
                    pnlChangePwd.Visible = false;
                    pnlLogin.Visible = false;
                    pnLogin.Visible = false;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string returnURL = "./logon.aspx";
            Response.Redirect(returnURL, false);
        }
        protected void btnPwdCancel_Click(object sender, EventArgs e)
        {
            txtNewPassword.Value = string.Empty;
            txtConfirmPwd.Value = string.Empty;
            lblMsg.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmail.Text = string.Empty;
            txtUserName.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }
        
        protected void btnFgtPassword_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            string returnURL = "./Index.aspx";
            string password = txtNewPassword.Value.Trim();
            string userName = string.Empty;
            if (password.Length < 8 || password.Length > 16)
            {
                lblMsg.Text = "Password must be 8-16 characters";
                pnlChangePwd.Visible = true;
                pnlRequestPwd.Visible = false;
                pnlLogin.Visible = false;
            }
            else
            {
                if (ViewState["username"] != null)
                {
                    try
                    {
                        userName = ViewState["username"].ToString();
                        avii.Classes.ForgetPassword.UpdatePassword(userName, password);

                        // ClientScript.RegisterStartupScript(this.GetType(), "temp1", "<script language='javascript'>alert('Password updated successfully');</script>", false);

                        // Response.Redirect(returnURL, false);
                        lblMsg.Text = string.Empty;
                        lblLogin.Text = @"Password updated successfully";
                        pnlChangePwd.Visible = false;
                        pnlRequestPwd.Visible = false;
                        pnlLogin.Visible = false;
                        pnLogin.Visible = true;
                        lblHeader.InnerHtml = "CUSTOMER LOGIN";

                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                }
            }
        }
        protected void btnRequestPwd_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            string userName = txtUserName.Text.Trim();
            string encrypted = string.Empty;
            string email = txtEmail.Text.Trim();
            string filePath = string.Empty;
            string fullPath = string.Empty;
            string rootPath = Server.MapPath("~");
            string emailMessage = string.Empty;
            int returnValue = 0, SecurityCode = 0, expireTime = 0;

            try
            {
                //DataTable userDT = avii.Classes.ForgetPassword.GetUserInfo(email);
                Exception exc = null;
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(email))
                {
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();

                            expireTime = (int)settingsReader.GetValue("expiretime", typeof(int));

                            // fullPath = rootPath + filePath;

                            encrypted = avii.Classes.CryptorEngine.Encrypt(userName, true);
                            avii.Classes.ForgotPasswordRequest request = avii.Classes.ForgetPassword.ForgotPasswordRequest(userName, email, expireTime, encrypted);
                            if (request != null && request.UserID > 0)
                            {
                                ViewState["userid"] = request.UserID;
                                ViewState["username"] = userName;

                                SecurityCode = request.SecurityCode;
                                emailMessage = request.EmailBody;
                                emailMessage = emailMessage.Replace("@Customer", userName);
                                emailMessage = emailMessage.Replace("@code", SecurityCode.ToString());
                                //emailMessage = avii.Classes.ForgetPassword.ReadFromFile(userName, fullPath, encrypted);
                                avii.Classes.clsGeneral.SendEmailNew(email, string.Empty, "Forgot password", emailMessage, "", request.UserID, 2, out exc, true);
                                lblMsg.Text = string.Empty;
                                //lblMessage.Text = @"Thank you.
                                //Please check your registered email to reset your password. 
                                //Please contact administrator if you have not received the email or 
                                //required more information.";
                                lblMessage.Text = "Please enter the security code just sent to your register email address with in 24 hours.";
                                pnlChangePwd.Visible = false;
                                pnlRequestPwd.Visible = false;
                                pnlLogin.Visible = true;
                                lblHeader.InnerHtml = "Verify Security Code";
                            }
                            else
                            {
                                lblMsg.Text = "Please enter correct username and email address.";
                                pnlChangePwd.Visible = false;
                                pnlRequestPwd.Visible = true;
                                pnlLogin.Visible = false;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please enter email address.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please enter username ";
                    }
                }
                else
                {
                    lblMsg.Text = "Please enter username and email address.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int userID = 0, securityCode = 0;
            if(!string.IsNullOrWhiteSpace(txtSecurityCode.Value))
            {
                securityCode = Convert.ToInt32(txtSecurityCode.Value);
                if(ViewState["userid"] != null)
                {
                    userID = Convert.ToInt32(ViewState["userid"]);
                    avii.Classes.SecurityCodeInfo info = avii.Classes.ForgetPassword.VerifySecurityCode(userID, securityCode);
                    if(info != null)
                    {
                        if(info.IsValid == 1 && info.IsExpired > 1 && info.Used == 0)
                        {
                            pnlChangePwd.Visible = true;
                            pnlRequestPwd.Visible = false;
                            pnlLogin.Visible = false;
                            lblHeader.InnerHtml = "Change Password";
                        }
                        else
                        {
                            if(info.IsValid == 0)
                            {
                                lblMsg.Text = "Invalid security code please try again.";
                            }
                            if (info.IsExpired < 1 && info.Used == 0)
                            {
                                lblMsg.Text = "Security code is invalid or has expired!";
                            }
                            if (info.IsExpired < 1 && info.Used == 1)
                            {
                                lblMsg.Text = "Security code is invalid or has expired!";
                            }
                            if (info.Used == 1)
                            {
                                lblMsg.Text = "Security code is invalid or has expired!";
                            }

                        }
                    }
                    else
                    {
                        lblMsg.Text = "Oops! Something went wrong. Please try again.";
                    }
                }
                else
                {
                    lblMsg.Text = "Oops! Something went wrong. Please try again.";
                }
            }
            else
            {
                lblMsg.Text = "Please enter security code!";
            }
        }
    }
}