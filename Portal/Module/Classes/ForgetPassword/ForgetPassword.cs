using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public class ForgetPassword
    {
        public static DataTable GetTimeStamp(string userName, string encrypted)
        {
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
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
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