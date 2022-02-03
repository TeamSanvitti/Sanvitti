using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public class ForgetPassword
    {
        public static SecurityCodeInfo GetTimeStamp(string userName, string encrypted)
        {
            SecurityCodeInfo securityCodeInfo = null;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;

            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@Encrypted", encrypted);
                arrSpFieldSeq = new string[] { "@UserName", "@Encrypted" };

                dataTable = db.GetTableRecords(objCompHash, "Av_CheckTimeStamp_Select", arrSpFieldSeq);
                if(dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach(DataRow row in dataTable.Rows)
                    {
                        securityCodeInfo = new SecurityCodeInfo();
                        securityCodeInfo.IsExpired = Convert.ToInt32(clsGeneral.getColumnData(row, "IsExpired", 0, false));
                        securityCodeInfo.UserID = Convert.ToInt32(clsGeneral.getColumnData(row, "UserID", 0, false));
                        securityCodeInfo.Used = Convert.ToInt32(clsGeneral.getColumnData(row, "Used", 0, false));
                    }
                }
                //return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return securityCodeInfo;
        }
        //public static DataTable GetUserInfo(string email)
        //{
        //    DataTable dataTable = new DataTable();
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq = null;
            
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
                
        //        objCompHash.Add("@Email", email);
        //        arrSpFieldSeq = new string[] { "@Email" };
                

        //        dataTable = db.GetTableRecords(objCompHash, "Av_UsersForgotPassword_Select", arrSpFieldSeq);
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dataTable;
        //}
        public static int EncryptUserName(string userName, string email, string encrypted)
        {
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@Email", email);
                objCompHash.Add("@Encrypted", encrypted);


                arrSpFieldSeq = new string[] { "@UserName", "@Email", "@Encrypted" };
                returnValue = db.ExecCommand(objCompHash, "Av_EncryptUsersName_Update", arrSpFieldSeq);
                
                return returnValue;
            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }
        public static ForgotPasswordRequest ForgotPasswordRequest(string userName, string email, int expireTime, string encrypted)
        {
            ForgotPasswordRequest request = null;
          //  SecurityCode = 0;
            //int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            DataTable dt = new DataTable();
            try
            {
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@Email", email);
                objCompHash.Add("@ExpireTime", expireTime);
                objCompHash.Add("@Encrypted", encrypted);

                arrSpFieldSeq = new string[] { "@UserName", "@Email", "@ExpireTime", "@Encrypted" };
                dt = db.GetTableRecords(objCompHash, "Av_ForgotPassword_Request", arrSpFieldSeq);
                if(dt != null && dt.Rows.Count > 0)
                {

                    foreach(DataRow row in dt.Rows)
                    {
                        request = new ForgotPasswordRequest();
                        request.UserID = Convert.ToInt32(clsGeneral.getColumnData(row, "UserID", 0, false));
                        request.SecurityCode = Convert.ToInt32(clsGeneral.getColumnData(row, "SecurityCode", 0, false));
                        request.EmailBody =  clsGeneral.getColumnData(row, "EmailBody", string.Empty, false) as string;
                    }
                }
                return request;
            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return request;
        }
        public static SecurityCodeInfo VerifySecurityCode(int userID, int securityCode)
        {
            SecurityCodeInfo securityCodeInfo = null;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            DataTable dt = new DataTable();
            try
            {
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SecurityCode", securityCode);
                

                arrSpFieldSeq = new string[] { "@UserID", "@SecurityCode" };
                dt = db.GetTableRecords(objCompHash, "Av_SecurityCode_Verify", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        securityCodeInfo = new SecurityCodeInfo();
                        securityCodeInfo.IsExpired = Convert.ToInt32(clsGeneral.getColumnData(row, "IsExpired", 0, false));
                        securityCodeInfo.IsValid = Convert.ToInt32(clsGeneral.getColumnData(row, "IsValid", 0, false));
                        securityCodeInfo.Used = Convert.ToInt32(clsGeneral.getColumnData(row, "Used", 0, false));
                    }
                }
                return securityCodeInfo;
            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return securityCodeInfo;
        }

        public static void UpdatePassword(string userName, string password)
        {
            
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@Password", password);


                arrSpFieldSeq = new string[] { "@UserName", "@Password" };
                db.ExeCommand(objCompHash, "Av_UserPassword_Update", arrSpFieldSeq);
               

            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
           
        }
        public static string ReadFromFile(string userName, string filePath, string encrypted)
        {
            string emailBody = string.Empty;
            emailBody = System.IO.File.ReadAllText(filePath);
            emailBody = emailBody.Replace("@customerName", userName);
                                           
            emailBody = emailBody.Replace("@UserName", encrypted);
            return emailBody;
        }
    }
}