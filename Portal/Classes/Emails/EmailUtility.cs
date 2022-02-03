using System;
using System.Net.Mail;
using System.Configuration;

namespace avii.Classes.Emails
{
    public class EmailUtility
    {
        public static int SendEmail(string MailFrom, string strEmailSubject, string strEmailBody, out Exception exObj, bool blnIsHtml = false)
        {
            exObj = null;
            try
            {
                string strEmailTo = ConfigurationManager.AppSettings["EmailTo"].ToString(); 
                string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Password = ConfigurationManager.AppSettings["AdminEmailPwd"].ToString();
                string smtp = ConfigurationManager.AppSettings["MailServer"].ToString();
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);


                //var client = new SmtpClient("smtp.gmail.com", 587)
                //{
                //    Credentials = new System.Net.NetworkCredential("myusername@gmail.com", "mypwd"),
                //    EnableSsl = false
                //};
                //client.Send("myusername@gmail.com", "myusername@gmail.com", "test", "testbody");


                //Password = "hanu@335";
                MailMessage msg = new MailMessage();
                msg.To.Add(strEmailTo);
                msg.CC.Add("bhargava.jagdeep@gmail.com");

                //msg.From = new MailAddress("sukhvirs63@gmail.com", strEmailSubject);
                msg.From = new MailAddress(MailFrom, strEmailSubject);

                msg.Subject = strEmailSubject;
                msg.Body = strEmailBody;
                msg.IsBodyHtml = blnIsHtml;

                /******** Using Gmail Domain ********/
                SmtpClient client = new SmtpClient(smtp, port);
                client.Credentials = new System.Net.NetworkCredential(AdminEmail, Password);
                //client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                /************************************/

                /******** Using Yahoo Domain ********/
                //SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 25);
                /************************************/

                client.Send(msg);         // Send our email.
                msg = null;

                return 1;
            }
            catch (Exception exc)
            {
                exObj = exc;
                return -1;
            }
        }

        public static int SendEmailAdmin(string MailFrom, string strEmailSubject, string strEmailBody, out Exception exObj, bool blnIsHtml = false)
        {
            exObj = null;
            try
            {
                string strEmailTo = ConfigurationManager.AppSettings["EmailTo"].ToString();
                string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Password = ConfigurationManager.AppSettings["AdminEmailPwd"].ToString();
                string smtp = ConfigurationManager.AppSettings["MailServer"].ToString();
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

                strEmailTo = "Contact@langlobal.com";

                //var client = new SmtpClient("smtp.gmail.com", 587) 61zIqq@7
                //{
                //    Credentials = new System.Net.NetworkCredential("myusername@gmail.com", "mypwd"),
                //    EnableSsl = false
                //};
                //client.Send("myusername@gmail.com", "myusername@gmail.com", "test", "testbody");


                //Password = "hanu@335";
                MailMessage msg = new MailMessage();
               
                msg.To.Add(strEmailTo);
                msg.From = new MailAddress(MailFrom, strEmailSubject);
                msg.Subject = strEmailSubject;
                msg.Body = strEmailBody;

                msg.BodyEncoding = System.Text.Encoding.UTF8;// System.Text.Encoding.GetEncoding("utf-8");
                msg.SubjectEncoding = System.Text.Encoding.Default;

                msg.IsBodyHtml = blnIsHtml;

                ////MailMessage msg = new MailMessage(MailFrom, strEmailTo, strEmailSubject, strEmailBody);

                ////msg.BodyEncoding = System.Text.Encoding.UTF8;// System.Text.Encoding.GetEncoding("utf-8");
                ////msg.SubjectEncoding = System.Text.Encoding.Default;

                ////msg.IsBodyHtml = blnIsHtml;

                SmtpClient smtpclient = new SmtpClient("langlobal.com", 587);
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new System.Net.NetworkCredential("support@langlobal.com", "E6e*lm90");
                smtpclient.Send(msg);


               

                return 1;
            }
            catch (Exception exc)
            {
                exObj = exc;
                return -1;
            }
        }

        public static int SendEmailNew(string strEmailTo, string strEmailSubject, string strEmailBody, out Exception exObj, bool blnIsHtml = false)
        {
            exObj = null;
            try
            {
               // string strEmailTo = ConfigurationManager.AppSettings["EmailTo"].ToString();
                string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Password = ConfigurationManager.AppSettings["AdminEmailPwd"].ToString();
                string smtp = ConfigurationManager.AppSettings["MailServer"].ToString();
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

                string MailFrom = "support@langlobal.com";

                //var client = new SmtpClient("smtp.gmail.com", 587)
                //{
                //    Credentials = new System.Net.NetworkCredential("myusername@gmail.com", "mypwd"),
                //    EnableSsl = false
                //};
                //client.Send("myusername@gmail.com", "myusername@gmail.com", "test", "testbody");


                //Password = "hanu@335";
               // MailMessage msg = new MailMessage(MailFrom, strEmailTo, strEmailSubject, strEmailBody);
                MailMessage msg = new MailMessage();

                msg.To.Add(strEmailTo);
                msg.From = new MailAddress(MailFrom, strEmailSubject);
                msg.Subject = strEmailSubject;
                msg.Body = strEmailBody;

                msg.BodyEncoding = System.Text.Encoding.UTF8;// System.Text.Encoding.GetEncoding("utf-8");
                msg.SubjectEncoding = System.Text.Encoding.Default;

                msg.IsBodyHtml = blnIsHtml;

                SmtpClient smtpclient = new SmtpClient("langlobal.com", 587);
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new System.Net.NetworkCredential("support@langlobal.com", "E6e*lm90");
                smtpclient.Send(msg);




                return 1;
            }
            catch (Exception exc)
            {
                exObj = exc;
                return -1;
            }
        }

    }
}