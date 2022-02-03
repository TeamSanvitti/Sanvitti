using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace avii.Controls
{
    public partial class FooterControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "loadCaptcha", "grecaptcha.render('recaptcha', {'sitekey': '6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T' });", true);
            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "loadCaptcha", "grecaptcha.render('recaptcha', {'sitekey': 'MySiteKey' });", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "loadCaptcha", "grecaptcha.render('recaptcha', {'sitekey': '6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T' });", true);
            lblMsg.Text = string.Empty;
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (avii.Classes.ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false);

            if (IsCaptchaValid)
            {
                string customerName = txtName.Text;

                lblMsg.Attributes.Add("color","green");
                string subject = txtName.Text;

                string body = string.Empty; // "Dear " + obj.FirstName + ", <br /><br /><br /> We have received your application. Thank you for your interest to be a part of SAAVOR family. We along with many other partners, take pride in serving the Saavor community. Please expect to hear from us in next 3-5 business days. Your temporary kitchen ID is " + TempKitchenId + ". <br /><br /><br /> Best regards, <br /> The SAAVOR Team  ";
                string custBody = string.Empty;
                string strpath = "";
                string custpath = "";
                string adminNewsletterPath = "~/Emails/AdminEmail.html";
                string custNewsletterPath = "~/Emails/CustomerEmail.html";
                try
                {
                    if (HttpContext.Current != null)
                    {
                        strpath = HttpContext.Current.Server.MapPath(adminNewsletterPath);
                    }
                   
                    using (StreamReader reader = new StreamReader(strpath))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{name}", txtName.Text);
                    body = body.Replace("{body}", txtcomments.Text);
                    body = body.Replace("{mobile}", txtMobile.Text);
                    body = body.Replace("{email}", txtEmail.Text);

                    Exception ex;

                    if ( avii.Classes.Emails.EmailUtility.SendEmailAdmin(txtEmail.Text, subject, body, out ex, true) == -1)
                    {
                        //txtName.Text = string.Empty;
                        //txtEmail.Text = string.Empty;
                        //txtMobile.Text = string.Empty;
                        //txtcomments.Text = string.Empty;
                        //ApplicationLogger.LogError(ex, "EmailUtility", "SendEmail");
                    }

                    //Customer email code
                    if (HttpContext.Current != null)
                    {
                        custpath = HttpContext.Current.Server.MapPath(custNewsletterPath);
                    }

                    using (StreamReader reader = new StreamReader(custpath))
                    {
                        custBody = reader.ReadToEnd();
                    }
                    custBody = custBody.Replace("{name}", txtName.Text);
                   // body = body.Replace("{body}", txtcomments.Text);
                   // body = body.Replace("{mobile}", txtMobile.Text);

                   

                    if (avii.Classes.Emails.EmailUtility.SendEmailNew(txtEmail.Text, "Lan Global", custBody, out ex, true) == -1)
                    {
                        //txtName.Text = string.Empty;
                        //txtEmail.Text = string.Empty;
                        //txtMobile.Text = string.Empty;
                        //txtcomments.Text = string.Empty;
                        //ApplicationLogger.LogError(ex, "EmailUtility", "SendEmail");
                    }
                    hdnMsg.Value = "sent";
                }
                catch (Exception exce)
                {
                   // serviceResponse.ReturnCode = "-1";
                  //  serviceResponse.ReturnMessage = exce.Message;
                }

                //Valid Request
                lblMsg.Text = "Thank you for contacting LAN Global. \r\n Your message will be sent to our customer support team.";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Company not selected!')</script>", false);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>alert('" + hdnmsg.Value + "');</script>", false);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Thank you for contacting LAN Global. \n\n Your message will be sent to our customer support team.');</script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Thank you for contacting LAN Global. \n\n Your message will be sent to our customer support team.');</script>", false);
                txtName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtMobile.Text = string.Empty;
                txtcomments.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "Captcha validation required!";
            }
        }
    }
}