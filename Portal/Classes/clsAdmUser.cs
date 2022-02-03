using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsAdmUser.
	/// </summary>
	public class clsAdmUser
	{
		public clsAdmUser()
		{
		}

		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_adm_Insert" ;
		private string strDeleteSQL = "pAdmDelete" ;


		public DataTable GetUsers()
		{
            return db.GetTableRecords("Aero_GetUserInfo", "CustPrice"); 
		}

        public DataTable GetAdminUsers()
        {
            return db.GetTableRecords("pAdmList", "CustPrice");
        }

		public string SetRecords(int method, string sUser, string sPwd)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						objCompHash.Add ("@UserName", sUser);
						objCompHash.Add ("@Pwd", sPwd);

						arrSpFieldSeq = new string[]{"@UserName","@Pwd"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						arrSpFieldSeq = new string[]{"@UserName"};
						objCompHash.Add ("@UserName", sUser);
						db.ExeCommand(objCompHash, strDeleteSQL, arrSpFieldSeq);
						break;
				}	
				return sCode;
			}
			catch (Exception objExp)
			{
				throw new Exception (objExp.Message.ToString() );
			}
			finally
			{
				objCompHash  = null;
				arrSpFieldSeq = null;
			}
		}
	
	
	}
}
