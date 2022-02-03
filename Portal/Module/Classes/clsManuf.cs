using System;
using System.Collections;
using System.Data;
namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsManuf.
	/// </summary>
	public class clsManuf
	{
		public clsManuf()
		{

		}

		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_MS_Insert" ;
		private string strDeleteSQL = "sp_MS_Delete" ;

		public DataTable GetMauf()
		{ 
			return  db.GetTableRecords("sp_Manuf_Select","CustPrice"); 
		}

		public string SetRecords(int method, int iMID, string sMName, string sMImage, string sType)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						objCompHash.Add ("@ivID", iMID);
						objCompHash.Add ("@ivName", sMName);
						objCompHash.Add ("@ivImage", sMImage);
						objCompHash.Add ("@ivType", "M");

						arrSpFieldSeq = new string[]{"@ivID","@ivName","@ivImage","@ivType"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						objCompHash.Add ("@ivID", iMID);
						objCompHash.Add ("@ivType", "M");
						arrSpFieldSeq = new string[]{"@ivID","@ivType"};
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

	public class clsSP
	{
		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_MS_Insert" ;
		private string strDeleteSQL = "sp_MS_Delete" ;
		public DataTable GetServiceProvider()
		{
			return db.GetTableRecords("sp_SP_List","SP");
		}

		public string SetRecords(int method, int iMID, string sMName, string sMImage, string sType)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						objCompHash.Add ("@ivID", iMID);
						objCompHash.Add ("@ivName", sMName);
						objCompHash.Add ("@ivImage", sMImage);
						objCompHash.Add ("@ivType", "S");

						arrSpFieldSeq = new string[]{"@ivID","@ivName","@ivImage","@ivType"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						objCompHash.Add ("@ivID", iMID);
						objCompHash.Add ("@ivType", "S");
						arrSpFieldSeq = new string[]{"@ivID","@ivType"};
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
