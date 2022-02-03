using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsSales.
	/// </summary>
	public class clsSales
	{
		public clsSales()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_Sales_Insert" ;
		private string strDeleteSQL = "sp_Sales_Delete" ;

		public DataTable GetSales()
		{ 
			return  db.GetTableRecords("sp_Sales_Select","Sales"); 
		}

		public string SetRecords(int method, int iID, string sName, string sActive)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						if (iID != 0)
							objCompHash.Add ("@ivID", iID);
						else
							objCompHash.Add ("@ivID", null);
						objCompHash.Add ("@ivSales", sName);
						objCompHash.Add ("@ivActive", sActive);
						arrSpFieldSeq = new string[]{"@ivID","@ivSales","@ivActive"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						objCompHash.Add ("@ivID", iID);
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
