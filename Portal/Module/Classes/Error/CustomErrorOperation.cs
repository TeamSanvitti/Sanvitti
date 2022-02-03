using System;
using System.Collections;

namespace avii.Classes
{
    public class CustomErrorOperation
    {

        public static void InesrtIntoErrorLog(int ErrorLogID, string source, string url, string errorMessage, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                //if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@ErrorLogID", ErrorLogID);
                    objCompHash.Add("@Source", source);
                    objCompHash.Add("@Url", url);
                    objCompHash.Add("@ErrorMessage", errorMessage);
                    objCompHash.Add("@UserID", userID);


                    arrSpFieldSeq = new string[] { "@ErrorLogID", "@Source", "@Url", "@ErrorMessage", "@UserID" };
                    db.ExeCommand(objCompHash, "sv_ErrorLog_InsertUpdate", arrSpFieldSeq);

                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}