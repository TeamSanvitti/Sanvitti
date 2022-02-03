using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Authenticate;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Authenticate
{
    public class AuthenticationOperation : BaseCreateInstance
    {
        public  CredentialValidation AuthenticateRequest(UserCredentials authentication, out Exception ex)
        {
            ex = null;
            SV.Framework.DAL.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.DAL.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.DAL.Authenticate.AuthenticationOperation>();

            CredentialValidation validation = authenticationOperation.ValidateUser(authentication, out ex);

            return validation;
        }
        
        public  CredentialValidation ValidateUser(UserCredentials authentication, string source, string comments, out Exception ex)
        {
            ex = null;
            SV.Framework.DAL.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.DAL.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.DAL.Authenticate.AuthenticationOperation>();
            return authenticationOperation.ValidateUser(authentication, source, comments, out ex);
        }

        public int AuthenticateRequest(clsAuthentication Authentication, out Exception ex)
        {
            ex = null;
            SV.Framework.DAL.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.DAL.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.DAL.Authenticate.AuthenticationOperation>();
            return authenticationOperation.ValidateUser(Authentication, out ex);
        }
        
        public  int ValidateUserAPI(clsAuthentication Authentication)
        {
            SV.Framework.DAL.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.DAL.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.DAL.Authenticate.AuthenticationOperation>();
            return authenticationOperation.ValidateUserAPI(Authentication);
        }
        public bool AuthenticateRequestNew(clsAuthentication AuthenticateUser, out int userid, out int companyID)
        {
            userid = companyID = 0;
            bool returnValue = false;
            returnValue = ValidateUserNew(AuthenticateUser, out userid, out companyID);
            //int userId = 0;
            //userId = clsUser.ValidateUser(AuthenticateUser);
            return returnValue;
        }
        public bool ValidateUserNew(clsAuthentication Authentication, out int userID, out int companyID)
        {
            userID = 0;
            companyID = 0;
            SV.Framework.DAL.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.DAL.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.DAL.Authenticate.AuthenticationOperation>();
            return authenticationOperation.ValidateUserNew(Authentication, out userID, out companyID);
        }


    }
}
