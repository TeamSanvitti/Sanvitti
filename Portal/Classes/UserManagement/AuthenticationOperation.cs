using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class AuthenticationOperation
    {
        public static CredentialValidation AuthenticateRequest(UserCredentials authentication, out Exception ex)
        {
            ex = null;
            CredentialValidation validation = ValidateUser(authentication, out ex);

            return validation;
        }

        public static CredentialValidation ValidateUser(UserCredentials authentication, out Exception ex)
        {
            ex = null;
            CredentialValidation validation = new CredentialValidation();
            DBConnect db = new DBConnect();
            string Comment;
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
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            Comment = CommonUtil.Get<string>(dataRow, "Comment");
                            if (Comment.ToLower() == "authenticated")
                            {
                                validation.CompanyAccountNumber = CommonUtil.Get<string>(dataRow, "CompanyAccountNumber");
                                validation.UserID = CommonUtil.Get<int>(dataRow, "userID");
                                validation.CompanyID = CommonUtil.Get<int>(dataRow, "CompanyID");
                                validation.CompanyName = CommonUtil.Get<string>(dataRow, "CompanyName");
                                validation.UserName = authentication.UserName;
                            }
                            else
                            {
                                validation.UserID = 0;
                                validation.CompanyID = 0;

                            }
                        }
                    }

                }
                else
                    throw new Exception("Please enter Username and Password");
            }
            catch (Exception exp)
            {
                ex = exp;
                throw exp;
            }
            return validation;
        }

        public static CredentialValidation ValidateUser(UserCredentials authentication, string source, string comments, out Exception ex)
        {
            ex = null;
            CredentialValidation validation = new CredentialValidation();
            DBConnect db = new DBConnect();

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
                throw exp;
            }
            return validation;
        }

    }
}