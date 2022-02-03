using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Xml.Serialization;  

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsGeneral.
	/// </summary>
	public class clsGeneral
	{
		public clsGeneral()
		{
		}

        public enum AdminFilterType
        {
            AerovoiceAdmin=1,
            CustomerAdmin=2
        }
		
		public static string fnEmailHTML(string sEmailName)
		{
			StreamReader objStrmRdr ;
			string sMsg;
			objStrmRdr = new  StreamReader(sEmailName);
			sMsg = objStrmRdr.ReadToEnd();
			objStrmRdr.Close();
			return sMsg;
		}
        public static void InesrtIntoEmailLog(int emailLogID, int userID, string emailBody, int emailStatus, string comments, int moduleID, string subject, string emailTo, string emailCC)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                //if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@EmailLogID", emailLogID);
                    objCompHash.Add("@EmailBody", emailBody);
                    objCompHash.Add("@EmailSentBy", userID);
                    objCompHash.Add("@EmailStatus", emailStatus);
                    objCompHash.Add("@Comments", comments);
                    objCompHash.Add("@ModuleID", moduleID);
                    objCompHash.Add("@Subject", subject);
                    objCompHash.Add("@EmailTo", emailTo);
                    objCompHash.Add("@EmailCC", emailCC);


                    arrSpFieldSeq = new string[] { "@EmailLogID", "@EmailBody", "@EmailSentBy", "@EmailStatus", "@Comments", "@ModuleID", "@Subject", "@EmailTo", "@EmailCC" };
                    db.ExeCommand(objCompHash, "sv_EmailLog_CREATE_UPDATE", arrSpFieldSeq);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    userid = Convert.ToInt32(dt.Rows[0]["userguid"]);
                    //}
                }
            }
            catch (Exception exp)
            {
                throw exp;
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
                        clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    }
                    catch (Exception exp)
                    { 
                        throw new Exception(exp.Message); 
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 0, ex.Message, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    
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
                        clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 1, string.Empty, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);
                    }
                    catch (Exception exp)
                    {
                        throw new Exception(exp.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.InesrtIntoEmailLog(0, userID, psMessage, 0, ex.Message, moduleID, psSubject, psEmailAddressTo, psEmailAddressCC);

                throw new Exception(ex.Message);
            }

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
