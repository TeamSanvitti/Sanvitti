using System;
using System.Collections;

namespace avii.Classes
{
    public class clsUser
    {

        public static void DeleteUser(int userID)
        {
             DBConnect db= new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@UserID" };
                objCompHash.Add("@UserID", userID);
                db.ExeCommand(objCompHash, "Aero_UserInfo_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static void InsertNewUser(string userName, string pwd, string emailAddress, string accountNumber, int accountStatusID, int companyID, int userID)
        {
             DBConnect db= new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@Username", userName);
                    objCompHash.Add("@Pwd", pwd);
                    objCompHash.Add("@Email", emailAddress);
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@AccountStatusID", accountStatusID);
                    objCompHash.Add("@AccountNumber", accountNumber);
                    arrSpFieldSeq = new string[] { "@Username", "@Pwd", "@Email", "@CompanyID", "@AccountStatusID", "@AccountNumber" };

                    db.ExecuteScalar(objCompHash, "Aero_UserInfo_INSERT", arrSpFieldSeq);
                    
                }
                else
                    throw new Exception("Please enter Username and Password");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        

        public static int ValidateUser(clsAuthentication Authentication)
        {
            int userID = 0 ;
            DBConnect db= new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (!string.IsNullOrEmpty(Authentication.UserName))
                {
                    objCompHash.Add("@Username", Authentication.UserName);
                    objCompHash.Add("@Password", Authentication.Password);
                    arrSpFieldSeq = new string[] { "@Username", "@Password" };

                    object retValue = db.ExecuteScalar(objCompHash, "Aero_AuthenticateUserAPI", arrSpFieldSeq);
                    if (retValue != null)
                    {
                        userID = Convert.ToInt32(retValue);
                    }
                }
                else
                    throw new Exception("Please enter Username and Password");
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return userID;
        }
    }
}
