using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Authenticate;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Authenticate
{
    public class AuthenticationOperation : BaseCreateInstance
    {        
        public  CredentialValidation ValidateUser(UserCredentials authentication, out Exception ex)
        {
            ex = null;
            CredentialValidation validation = default;// new CredentialValidation();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    if (!string.IsNullOrEmpty(authentication.UserName))
                    {
                        objCompHash.Add("@Username", authentication.UserName);
                        objCompHash.Add("@Password", authentication.Password);
                        objCompHash.Add("@Source", "API");

                        //objCompHash.Add("@Source", source.ToString());

                        arrSpFieldSeq = new string[] { "@Username", "@Password", "@Source" };

                        DataTable dt = db.GetTableRecords(objCompHash, "Aero_AuthenticateUser", arrSpFieldSeq);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            validation = new CredentialValidation();
                            foreach (DataRow dataRow in dt.Rows)
                            {
                                validation.CompanyAccountNumber = CommonUtil.Get<string>(dataRow, "CompanyAccountNumber");
                                validation.UserID = CommonUtil.Get<int>(dataRow, "userID");
                                validation.CompanyID = CommonUtil.Get<int>(dataRow, "CompanyID");
                                validation.CompanyName = CommonUtil.Get<string>(dataRow, "CompanyName");
                                validation.UserName = authentication.UserName;
                            }
                        }

                    }
                    else
                        throw new Exception("Please enter Username and Password");
                }
                catch (Exception exp)
                {
                    ex = exp;
                    Logger.LogMessage(exp, this);//throw exp;
                }
            }
            return validation;
        }

        public  CredentialValidation ValidateUser(UserCredentials authentication, string source, string comments, out Exception ex)
        {
            ex = null;
            CredentialValidation validation = default;// new CredentialValidation();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    if (!string.IsNullOrEmpty(authentication.UserName))
                    {
                        objCompHash.Add("@Username", authentication.UserName);
                        objCompHash.Add("@Password", authentication.Password);
                        objCompHash.Add("@Source", source);
                        objCompHash.Add("@Comments", comments);

                        //objCompHash.Add("@Source", source.ToString());

                        arrSpFieldSeq = new string[] { "@Username", "@Password", "@Source", "@Comments" };

                        DataTable dt = db.GetTableRecords(objCompHash, "Aero_AuthenticateUser", arrSpFieldSeq);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            validation = new CredentialValidation();
                            foreach (DataRow dataRow in dt.Rows)
                            {
                                validation.CompanyAccountNumber = CommonUtil.Get<string>(dataRow, "CompanyAccountNumber");
                                validation.UserID = CommonUtil.Get<int>(dataRow, "userID");
                                validation.CompanyID = CommonUtil.Get<int>(dataRow, "CompanyID");
                                validation.CompanyName = CommonUtil.Get<string>(dataRow, "CompanyName");
                                validation.UserName = authentication.UserName;
                            }
                        }
                    }
                    else
                        throw new Exception("Please enter Username and Password");
                }
                catch (Exception exp)
                {
                    ex = exp;
                    Logger.LogMessage(exp, this);//throw exp;
                }
            }
            return validation;
        }

        public int ValidateUser(clsAuthentication Authentication, out Exception ex)
        {
            ex = null;
            int userID = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    if (!string.IsNullOrEmpty(Authentication.UserName))
                    {
                        objCompHash.Add("@Username", Authentication.UserName);
                        objCompHash.Add("@Password", Authentication.Password);
                        objCompHash.Add("@Source", "API");
                        arrSpFieldSeq = new string[] { "@Username", "@Password", "@Source" };

                        object retValue = db.ExecuteScalar(objCompHash, "Aero_AuthenticateUser", arrSpFieldSeq);
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
                    ex = exp;
                    Logger.LogMessage(exp, this);// throw exp;
                }
            }
            return userID;
        }
        public  int ValidateUserAPI(clsAuthentication Authentication)
        {
            int userID = 0;
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(exp, this);//throw exp;
                }
            }
            return userID;
        }
        public bool ValidateUserNew(clsAuthentication Authentication, out int userID, out int companyID)
        {
            bool returnValue = false;
            userID = 0;
            companyID = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    if (!string.IsNullOrEmpty(Authentication.UserName))
                    {
                        objCompHash.Add("@Username", Authentication.UserName);
                        objCompHash.Add("@Password", Authentication.Password);
                        arrSpFieldSeq = new string[] { "@Username", "@Password" };

                        db.ExCommand(objCompHash, "av_AuthenticateUserAPI", arrSpFieldSeq, "@UserID", "@CompanyID", out userID, out companyID);
                        if (userID > 0 && companyID > 0)
                            returnValue = true;
                    }
                    else
                        throw new Exception("Please enter Username and Password");
                }
                catch (Exception exp)
                {
                    Logger.LogMessage(exp, this);//throw exp;
                }
            }
            return returnValue;
        }


    }
}
