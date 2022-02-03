using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public static class clsCompany
    {
        public static DataTable GetCompany(int CompanyID)
        {
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
                if (CompanyID > 0)
                {
				    objCompHash.Add ("@CompanyID", CompanyID);
                    arrSpFieldSeq = new string[] { "@CompanyID" };
                }

                dataTable = db.GetTableRecords(objCompHash, "Aero_Company_Select", arrSpFieldSeq);
                return dataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}

            return dataTable;
        }
        public static DataTable GetState(int CompanyID)
        {
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                
                objCompHash.Add("@countryID", CompanyID);
                arrSpFieldSeq = new string[] { "@countryID" };
                

                dataTable = db.GetTableRecords(objCompHash, "av_State_Select", arrSpFieldSeq);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }
        public static DataTable GetCompanyStores(int userID)
        {
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (userID > 0)
                {
                    objCompHash.Add("@CompanyID", 0);
                    objCompHash.Add("@UserID", userID);
                    arrSpFieldSeq = new string[] { "@CompanyID", "@UserID" };
                }

                dataTable = db.GetTableRecords(objCompHash, "av_CompanyStore_Select", arrSpFieldSeq);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }

        public static int GetCompanyUser(int CompanyID)
        {
            int userID = 0;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (CompanyID > 0)
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    arrSpFieldSeq = new string[] { "@CompanyID" };
                }

                dataTable = db.GetTableRecords(objCompHash, "Aero_Company_User_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    userID = Convert.ToInt32(dataTable.Rows[0]["UserID"]);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userID;
        }
    }
}
