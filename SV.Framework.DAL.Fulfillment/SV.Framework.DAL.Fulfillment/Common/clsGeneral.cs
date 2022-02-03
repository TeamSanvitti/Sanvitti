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


namespace SV.Framework.DAL.Fulfillment
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
