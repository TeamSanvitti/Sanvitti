using System;

namespace avii.Classes
{
    public static class clsDatabaseAction
    {
        public static string ClearDBLog()
        {
            DBConnect db = new DBConnect();
            string errorMessage = string.Empty;
            try
            {
                db.ExeCommand("Aero_ClearDBLog");
            }
            catch (Exception objExp)
            {
                errorMessage = objExp.Message.ToString();
            }
            finally
            {
                db.DBClose();
                db = null;
            }
            return errorMessage;
        }
    }
}
