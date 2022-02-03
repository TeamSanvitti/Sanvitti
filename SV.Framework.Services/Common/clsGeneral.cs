using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
//using System.Web.Mail;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.Net;
using System.Net.Mail;


namespace SV.Framework.Services
{
	/// <summary>
	/// Summary description for clsGeneral.
	/// </summary>
	public class clsGeneral
	{
		public clsGeneral()
		{
		}

        [Serializable]
        public enum ResponseErrorCode
        {
            None,
            MissingParameter,
            UploadedSuccessfully,
            SuccessfullyRetrieved,
            InternalError,
            InconsistantData,
            ErrowWhileLoadingData,
            PurchaseOrderNotShipped,
            PurchaseOrderShipped,
            CannotAuthenticateUser,
            PurchaseOrderItemNotAssigned,
            PurchaseOrderNotExists,
            FulfillmentOrderNotExists,
            PurchaseOrderAlreadyExists,
            ShipByIsNotCorrect,
            NoRecordsFound,
            NoItemFound,
            RMANotExists,
            UpdatedSuccessfully,
            QuantityIsNotCorrect,
            PurchaseOrderCannotBeCancelled,
            RMACannotBeCancelled,
            StateCodeIsNotCorrect,
            FulfillmentOrderAlreadyProcessed,
            AccountNumberNotExists,
            DataNotUpdated,
            DuplicateItemFound,
            StroreIDNotExists,
            SubmittedSuccessfully,
            CancelledSuccessfully
        }

        public enum AdminFilterType
        {
            AerovoiceAdmin=1,
            CustomerAdmin=2
        }
        //public static string CustomerEmailAddress(avii.Classes.UserInfo userInfo, string emailAddress, string customerEmail, string overrideEmail)
        //{
        //    string emailAddr = "rma@aerovoice.com";
        //    try
        //    {
                
        //        if (!string.IsNullOrEmpty(userInfo.Email) && userInfo.UserType != "Aerovoice")
        //        {
        //            List<CustomerEmail> custEmails = userInfo.CustomerEmails;
        //            var emaiList = (from item in custEmails where item.ModuleGUID.Equals(75) select item).ToList();
        //            if (emaiList != null && emaiList.Count > 0)
        //            {
        //                if (!string.IsNullOrEmpty(emaiList[0].OverrideEmail))
        //                    emailAddr = emaiList[0].OverrideEmail;
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(emaiList[0].Email))
        //                        emailAddr = emaiList[0].Email + "," + emailAddress;
        //                    else
        //                        emailAddr = emaiList[0].CompanyEmail + "," + emailAddress;
        //                }
        //            }
        //            else
        //                emailAddr = userInfo.Email + "," + emailAddress;
        //        }
        //        else if (!string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Aerovoice")
        //        {
        //            if (!string.IsNullOrEmpty(overrideEmail))
        //                emailAddr = overrideEmail;
        //            else
        //                emailAddr = customerEmail + "," + emailAddress;
        //        }
        //        else if (string.IsNullOrEmpty(customerEmail) && userInfo.UserType == "Aerovoice")
        //            if (!string.IsNullOrEmpty(overrideEmail))
        //                emailAddr = overrideEmail;


                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return emailAddr;
        //}
		
		public static string fnEmailHTML(string sEmailName)
		{
			StreamReader objStrmRdr ;
			string sMsg;
			objStrmRdr = new  StreamReader(sEmailName);
			sMsg = objStrmRdr.ReadToEnd();
			objStrmRdr.Close();
			return sMsg;
		}
        public static void SendEmail5(string psEmailAddressTo, string psEmailAddressCC, string psSubject, string psMessage, int userID, int moduleID)
        {
            try
            {
                //string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                //string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailC"];
                //string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];
                string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];
               
                int portNumber = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["port"]);
                bool enableSSl = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["enableSSl"]);

                if (!(string.IsNullOrEmpty(sSMTP) && string.IsNullOrEmpty(serviceEmail) && string.IsNullOrEmpty(serviceEmailPwd)))
                {

                    System.Net.Mail.MailMessage loMail = new System.Net.Mail.MailMessage(serviceEmail, psEmailAddressTo);

                    if (!string.IsNullOrEmpty(psEmailAddressCC))
                    {
                        MailAddress copy = new MailAddress(psEmailAddressCC);
                        loMail.CC.Add(copy);
                    }

                    loMail.Subject = psSubject;
                    loMail.Body = psMessage;
                    loMail.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    smtpClient.Host = sSMTP;
                    smtpClient.Port = portNumber;
                    smtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(serviceEmail, serviceEmailPwd);

                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = enableSSl;

                    smtpClient.Send(loMail);
                }
            }
            catch (Exception ex)
            {
                //insert error to log table
                //clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                throw new Exception(ex.Message);
            }

        }

        public static void SendEmail6(string psEmailAddressTo, string psEmailAddressCC, string psSubject, string psMessage, int userID, int moduleID)
        {
            try
            {
                //string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                //string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailC"];
                //string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];
                string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];

                int portNumber = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["port"]);
                bool enableSSl = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["enableSSl"]);

                if (!(string.IsNullOrEmpty(sSMTP) && string.IsNullOrEmpty(serviceEmail) && string.IsNullOrEmpty(serviceEmailPwd)))
                {

                    System.Net.Mail.MailMessage loMail = new System.Net.Mail.MailMessage(serviceEmail, psEmailAddressTo);

                    if (!string.IsNullOrEmpty(psEmailAddressCC))
                    {
                        MailAddress copy = new MailAddress(psEmailAddressCC);
                        loMail.CC.Add(copy);
                    }

                    loMail.Subject = psSubject;
                    loMail.Body = psMessage;
                    loMail.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Host = sSMTP;
                    smtpClient.Port = portNumber;
                    smtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(serviceEmail, serviceEmailPwd);

                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = enableSSl;

                    smtpClient.Send(loMail);
                }
            }
            catch (Exception ex)
            {
                //insert error to log table
               // clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                throw new Exception(ex.Message);
            }

        }
        
		public static  void SendEmail(string psEmailAddressTo, string psEmailAddressCC, string psSubject, string psMessage, int userID, int moduleID)
		{
            try
            {
                string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];
               
                if (!(string.IsNullOrEmpty(sSMTP) && string.IsNullOrEmpty(serviceEmail) && string.IsNullOrEmpty(serviceEmailPwd)))
                {

                    System.Net.Mail.MailMessage loMail = new System.Net.Mail.MailMessage(serviceEmail, psEmailAddressTo);

                    if (!string.IsNullOrEmpty(psEmailAddressCC))
                    {
                        MailAddress copy = new MailAddress(psEmailAddressCC);
                        loMail.CC.Add(copy);
                    }

                    loMail.Subject = psSubject;                    
                    loMail.Body = psMessage;
                    loMail.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    smtpClient.Host = sSMTP;
                    smtpClient.Port = 587;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(serviceEmail, serviceEmailPwd);
                    smtpClient.Credentials = credentials;                    
                    smtpClient.Send(loMail);

                    try
                    {
                        //clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    }
                    catch (Exception exp)
                    { 
                        throw new Exception(exp.Message); 
                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 0, ex.Message, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    
                throw new Exception(ex.Message);
            }

		}
        public static void SendEmail2(string psEmailAddressTo, string psEmailAddressCC, string psSubject, string psMessage, int userID, int moduleID)
        {
            try
            {
                string sSMTP = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                string serviceEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                string serviceEmailPwd = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];

                if (!(string.IsNullOrEmpty(sSMTP) && string.IsNullOrEmpty(serviceEmail) && string.IsNullOrEmpty(serviceEmailPwd)))
                {

                    System.Net.Mail.MailMessage loMail = new System.Net.Mail.MailMessage(serviceEmail, psEmailAddressTo);

                    if (!string.IsNullOrEmpty(psEmailAddressCC))
                    {
                        MailAddress copy = new MailAddress(psEmailAddressCC);
                        loMail.CC.Add(copy);
                    }

                    loMail.Subject = psSubject;
                    loMail.Body = psMessage;
                    loMail.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Host = sSMTP;
                    smtpClient.Port = 587;
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(serviceEmail, serviceEmailPwd);
                    smtpClient.Credentials = credentials;
                    smtpClient.Send(loMail);

                    try
                    {
                        //clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 0, ex.Message, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);

                throw new Exception(ex.Message);
            }

        }
        public static void SendEmailWithTemplate(string emailTo, string emailAddressCC, string subject, string body, string toName, string url, string esnDetail, string rmaNumber, string rmaDate, string rmaStatus)
        {
            try
            {
                string host = System.Configuration.ConfigurationSettings.AppSettings["MailServer"];
                string emailForm = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                string password = System.Configuration.ConfigurationSettings.AppSettings["AdminEmailPwd"];

                int portNo = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["portno"]);
                string userName = emailForm;
                bool ssl = false;
                bool defaultCredentials = false;
                ssl = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ssl"]);
                defaultCredentials = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["defaultcredentials"]);

                if (!(string.IsNullOrEmpty(host) && string.IsNullOrEmpty(emailForm) && string.IsNullOrEmpty(password)))
                {
                    //lblMsg.Text = string.Empty;
                    //string emailTo = txtTo.Text.Trim();

                    //int portNo = 587;
                    //bool ssl = false;
                    //bool defaultCredentials = false;
                    //emailForm = txtFrom.Text.Trim();
                    //userName = txtUserName.Text.Trim();
                    //password = txtPassword.Text.Trim();
                    //if (txtPortno.Text.Trim() != string.Empty)
                    //    portNo = Convert.ToInt32(txtPortno.Text.Trim());
                    //host = txtHost.Text.Trim();

                    //ssl = Convert.ToBoolean(ddlSsl.SelectedValue);
                    //defaultCredentials = Convert.ToBoolean(ddlCred.SelectedValue);
                    //// SendEmail2(emailTo);

                    var fromAddress = new MailAddress(emailForm, subject);
                    //var toAddress = new MailAddress(emailTo, toName);
                    string fromPassword = password;
                    //const string subject = "Test email";
                    //const string body = "Testing...";

                    var smtp = new SmtpClient
                    {
                        Host = host,
                        Port = portNo,
                        EnableSsl = ssl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = defaultCredentials,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    //var message = new MailMessage();

                    //message.To.Add(emailTo);
                    //message.To.Add(emailAddressCC);

                    //message.From = new MailAddress(emailForm);
                    //message.Subject = subject;
                    //message.Body = body;


                    string emailBody = ReadHtmlPage(url);

                    emailBody = emailBody.Replace("@customerName", toName);
                    //emailBody = emailBody.Replace("@RMABody", body);
                    emailBody = emailBody.Replace("@RMANumber", rmaNumber);
                    emailBody = emailBody.Replace("@RMADate", rmaDate);
                    emailBody = emailBody.Replace("@RMAStatus", rmaStatus);
                    emailBody = emailBody.Replace("@RMALineItems", esnDetail);

                    using (var message = new MailMessage()
                    {

                        Subject = subject,
                        Body = emailBody
                    })
                    {
                        message.To.Add(emailTo);
                        if (!string.IsNullOrEmpty(emailAddressCC))
                            message.To.Add(emailAddressCC);

                        message.From = new MailAddress(emailForm, subject);

                        //message.CC.Add(emailTo);
                        //message.To.Add(emailAddressCC);
                        message.IsBodyHtml = true;

                        smtp.Send(message);
                        //lblMsg.Text = "Sent";
                    }
                }
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("Application", subject + ": " + ex.Message);
                throw new Exception(ex.Message);
            }

            //System.Diagnostics.EventLog.WriteEntry("Application", subject);



        }
        public static string ReadHtmlPage(string url)
        {

            WebResponse objResponse = default(WebResponse);
            WebRequest objRequest = default(WebRequest);
            string result = null;

            objRequest = System.Net.HttpWebRequest.Create(url);
            objResponse = objRequest.GetResponse();
            StreamReader sr = new StreamReader(objResponse.GetResponseStream());
            result = sr.ReadToEnd();

            //clean up StreamReader
            sr.Close();

            // String line = String.Empty;
            // using (StreamReader reader = new StreamReader(url))
            // {
            //     while ((line = reader.ReadLine()) != null)
            //     {
            //         continue;
            //     }
            // }


            return result;
        }

        public static object getColumnData(DataRow dr, string colName, object defVal, bool bThrow)
        {
            object retVal = defVal;

            if (dr.Table.Columns.Contains(colName) == false && bThrow)
                throw new Exception(colName + " column does not exists");
            else if (dr.Table.Columns.Contains(colName) == false && bThrow == false)
            {
            }
            else
            {
                object obj = dr[colName];
                try
                {
                    // If column does not exist, it will throw exception and return default value if throw is false
                    retVal = (((obj == null) || (obj != null && (obj.GetType() == typeof(System.DBNull)))) ? defVal : dr[colName]);
                }
                catch (Exception)
                {
                    if (bThrow) { throw; }
                }
            }

            return retVal;
        }
        public static string SerializeObject<T>(T obj)
        {
            StringWriter xmlstringVal = null;
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xmlstringVal = new StringWriter();
                ///xmlSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
                xmlSettings.OmitXmlDeclaration = true;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriter xmlWriter = XmlWriter.Create(xmlstringVal, xmlSettings);
                xs.Serialize(xmlWriter, obj, ns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmlSettings = null;
                xmlstringVal.Dispose();
            }

            return xmlstringVal.ToString().Trim();
            //try
            //{
            //    string xmlString = null;
            //    MemoryStream memoryStream = new MemoryStream();
            //    XmlSerializer xs = new XmlSerializer(typeof(T));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, obj);
            //    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            //    xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        
        public string serializeObjetToXMLString(object obj, string rootNodeName, string listName)
        {

            XmlSerializer objXMLSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memstr = new MemoryStream();
            XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);
            string sXML = "";
            try
            {

                objXMLSerializer.Serialize(xmltxtwr, obj);


                sXML = Encoding.UTF8.GetString(memstr.GetBuffer());
                sXML = "<" + rootNodeName + ">" + sXML.Substring(sXML.IndexOf("<" + listName + ">"));
                sXML = sXML.Substring(0, (sXML.LastIndexOf(Convert.ToChar(62)) + 1));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmltxtwr.Close();
                memstr.Close();
            }
            return sXML;
        }
	}
}
