using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsPType.
	/// </summary>
	public class clsPType
	{
		public clsPType()
		{
		}

		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_PType_Insert" ;
		private string strDeleteSQL = "sp_PType_Delete" ;


		public DataTable GetPType()
		{ 
			return  db.GetTableRecords("sp_PType_List","CustPrice"); 
		}

		public DataSet GetPhoneTypes()
		{ 
			return  db.GetDataSet("sp_PType_List"); 
		}

		public string SetRecords(int method, int iPID, string sDesc, string sType)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						objCompHash.Add ("@ivID", iPID);
						objCompHash.Add ("@ivDesc", sDesc);
						objCompHash.Add ("@ivType", sType);

						arrSpFieldSeq = new string[]{"@ivID","@ivDesc","@ivType"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						objCompHash.Add ("@ivID", iPID);
						arrSpFieldSeq = new string[]{"@ivID"};
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
