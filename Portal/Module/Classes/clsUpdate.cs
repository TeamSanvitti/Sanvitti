using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsUpdate.
	/// </summary>
	public class clsUpdate
	{
		#region "Private Objects"
		private DBConnect db= new DBConnect();
		
		#endregion

		#region "Constructor"
		public clsUpdate()
		{
		}
		#endregion

		public void SetRecords(string sUid, string sTitle, string sUDesc, string sImage, string sDate, string  sActive, string  sType,string sDoc,string sLink)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				objCompHash.Add("@Uid", sUid);
				objCompHash.Add ("@Title", sTitle);
				objCompHash.Add ("@UDesc", sUDesc);
				objCompHash.Add ("@UImage", sImage);
				objCompHash.Add ("@sDate", sDate);
				objCompHash.Add ("@Active", sActive);
				objCompHash.Add ("@UType", sType);
				objCompHash.Add ("@UDoc", sDoc);
				objCompHash.Add ("@ULink", sLink);
			
				arrSpFieldSeq = new string[]{"@Uid","@Title", "@UDesc", "@UImage", "@sDate", "@Active","@UType","@UDoc","@ULink"};
				db.ExeCommand(objCompHash, "sp_Update_Insert", arrSpFieldSeq);
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

		public DataTable SrchUpdate(string sTitle, string sDocType)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				objCompHash.Add("@Title", sTitle);
				objCompHash.Add ("@sDoc", sDocType);
			
				arrSpFieldSeq = new string[]{"@Title","@sDoc"};
				return db.GetTableRecords(objCompHash, "sp_Update_Select", arrSpFieldSeq);
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

		public DataTable GetUpdates(string sUID)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				objCompHash.Add("@Title", null);
				objCompHash.Add("@sDoc", null);
				objCompHash.Add("@UID", sUID);
				arrSpFieldSeq = new string[]{"@Title","@sDoc","@UID"};
				return db.GetTableRecords(objCompHash, "sp_Update_Select", arrSpFieldSeq);
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
