using System;
using System.Collections;
using System.Data;
using System.Text;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsItem.
	/// </summary>
	public class clsItem :IDisposable
	{
		private DBConnect db = new DBConnect();
		private string strDefaultValue = "Not Available";
		
		#region PRIVATE VARIABLES/OBJECTS
		private DataTable ptblPrices;
		private DataTable ptblFeatures;
		private DataTable ptblSP;
		private DataTable ptblItemInt = null;

		private string psModel;
		private string psManuf;
		private string psType;
		private string psName;
		private string psDesc;
		private string psImage;
		private string psAvail;
		private string psItemID;
		private string psMemo;
		private string psSpecial;
		private string psNewArrival;
		private string psNew;
		private string psOld;
		private string psRef;
		private string psSite;
		private string psWrnty;
		private string psDim;
		private string psBat;
		private string psHidePrice;
		private string psInActive;
		#endregion

		#region PROPERTIES
 
		public string HidePrice
		{
			set 
			{ psHidePrice = value ; 	}
			get { return psHidePrice; }
		}
		public string InActive
		{
			set 
			{ psInActive = value ; 	}
			get { return psInActive; }
		}
		public string Warrenty
		{
			set 
			{ psWrnty = value ; 	}
			get { return psWrnty; }
		}
		public string Dimension
		{
			set 
			{ psDim = value ; 	}
			get { return psDim; }
		}
		public string Battery
		{
			set 
			{ psBat = value ; 	}
			get { return psBat; }
		}
		public DataTable Features
		{
			set 
			{ ptblFeatures = value ; }
			get { return ptblFeatures; }
		}
		public DataTable Prices
		{
			set 
			{ ptblPrices = value ; }
			get { return ptblPrices; }
		}

		public DataTable SPs
		{
			set 
			{ ptblSP = value ; }
			get { return ptblSP; }
		}

		public string ItemID
		{
			set 
			{ 
			 	psItemID = value ;
			}
			get { return psItemID; }
		}

		public string Type
		{
			set 
			{ 
				if (value.Length > 0) 
					psType = value ;
				else
					throw new Exception ("Phone Type can't be left blank");
			}
			get { return psType; }
		}

		public string Model
		{
			set 
			{ 
				if (value.Length > 0) 
					psModel = value ;
				else
					throw new Exception ("Phone Model can't be left blank");
			}
			get { return psModel; }
		}

		public string Manufacturar	
		{
			set 
			{ 
				if (value.Length > 0) 	psManuf = value ;
				else throw new Exception("Please enter Phone Manufacturor"); 
			}
			get { return psManuf; }
		}

		public string Name	
		{
			set 
			{ 
				if (value.Length > 0) 	psName = value ;
				else throw new Exception("Please enter Phone title"); 
			}
			get { return psName; }
		}

		public string Description	
		{
			set 
			{ 
				if (value.Length > 0) 	psDesc = value ;
				else psDesc = strDefaultValue; 
			}
			get { return psDesc; }
		}

		public string Image
		{
			set 
			{ 
				if (value.Length > 0) 	psImage = value ;
				else psImage = strDefaultValue; 
			}
			get { return psImage; }
		}

		public string Availability
		{
			set 
			{ 
				if (value.Length > 0) 	psAvail = value ;
				else psAvail = strDefaultValue; 
			}
			get { return psAvail; }
		}

		public string Memo
		{
			set 
			{
				if (value.Length > 0) 	psMemo = value ;
				else psMemo = strDefaultValue; 
			}
			get { return psMemo; }
		}

		public string SpecialItem
		{
			set	{ psSpecial = value;}
			get	{ return psSpecial;}
		}

		public string NewArrival
		{
			set	{ psNewArrival = value;}
			get	{ return psNewArrival;}
		}

		public string Cond_New
		{
			set	{ psNew = value;}
			get	{ return psNew;}
		}

		public string Cond_Old
		{
			set	{ psOld = value;}
			get	{ return psOld;}
		}
		public string Cond_Ref
		{
			set	{ psRef = value;}
			get	{ return psRef;}
		}

		public string theSite
		{
			set	{ psSite = value;}
			get	{ return psSite;}
		}


		#endregion

		#region DISPOSE
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.DBClose();
				db=null;
				strInsertSQL =null;
				strUpdateSQL =null;
				strDeleteSQL =null;
				ptblPrices = null;
				ptblFeatures = null;
			}
		}
		#endregion
		
		#region CONSTRUCTURE
		public clsItem()
		{	}
		#endregion

		#region SQLSTATEMENT
		private  string  strInsertSQL = "dbo.sp_Item_Insert";
		public  string strUpdateSQL = "dbo.sp_Item_Update";
		private  string strDeleteSQL = "dbo.sp_Item_Delete";
		private  string strSelectSQL = "dbo.sp_Item_Select";
		#endregion


		public void DeleteItem(string tid)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{
				objCompHash.Add ("@ItemID", tid);
				arrSpFieldSeq = new string[]{"@ItemID"};
				db.ExeCommand(objCompHash, "sp_item_Delete", arrSpFieldSeq);
					
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
		public string SetRecords(int method)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						objCompHash.Add ("@ItemID", psItemID);
						objCompHash.Add ("@PhoneType", psType);
						objCompHash.Add ("@PhoneManuf", psManuf);
						objCompHash.Add ("@PhoneModel", psModel);
						objCompHash.Add ("@PhoneTitle", psName);
						objCompHash.Add ("@PhoneDesc", psDesc);
						objCompHash.Add ("@PhoneImage", psImage);
						objCompHash.Add ("@Special",  Convert.ToByte(psSpecial));
						objCompHash.Add ("@NewArrival", (psNewArrival == null?0:1));
						objCompHash.Add ("@Cond_New", (psNew == null?0:1));
						objCompHash.Add ("@Cond_Old", (psOld == null?0:1));
						objCompHash.Add ("@Cond_Ref", (psRef == null?0:1));
						objCompHash.Add ("@Memo", psMemo);
						objCompHash.Add ("@theSite", psSite);
						objCompHash.Add ("@Available", psAvail);
						objCompHash.Add ("@Feature", GetFeatureText());
						objCompHash.Add ("@Price", GetPriceText());
						objCompHash.Add ("@SP", GetSPText());
						objCompHash.Add ("@HidePrice", psHidePrice);
						objCompHash.Add ("@Active", psInActive);

						//objCompHash.Add ("@warrenty", psWrnty);
						//objCompHash.Add ("@PDimension", psDim);
						//objCompHash.Add ("@Battery", psBat);
					   
						arrSpFieldSeq = new string[]{"@ItemID","@PhoneType","@PhoneManuf","@PhoneModel","@PhoneTitle","@PhoneDesc","@PhoneImage","@Special","@NewArrival","@Cond_New","@Cond_Old","@Cond_Ref","@Memo","@theSite","@Available","@Feature","@Price","@SP","@HidePrice","@Active"};
														//,"@warrenty","@PDimension","@Battery"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq,"Code",out sCode);
						break;
					case 3:
						objCompHash.Add ("@ItemID", ItemID);
						arrSpFieldSeq = new string[]{"@ItemID"};
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

		public string SetRelatedItems(string psItemID, string psRelItems, string psIntItems)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 			objCompHash.Add ("@ItemID", psItemID);
						objCompHash.Add ("@RItems", psRelItems);
						objCompHash.Add ("@AccItems", psIntItems);
						arrSpFieldSeq = new string[]{"@ItemID","@RItems","@AccItems"};
						db.ExeCommand(objCompHash, "sp_Item_Int_Insert", arrSpFieldSeq);
					
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

		public DataSet GetItemDetail(string  sItemID)
		{
			if (sItemID != null)
			{
				string[] arrSpFieldSeq;
				Hashtable objCompHash = new Hashtable();
				objCompHash.Add ("@ItemID", sItemID);
				arrSpFieldSeq = new string[]{"@ItemID"};
				return db.GetDataSet(objCompHash,strSelectSQL,arrSpFieldSeq); 
			}
			else
				return null;
		}

		public DataTable GetFeatures()
		{
			if (ptblFeatures != null)
				return ptblFeatures;
			else
			{
				ptblFeatures  = new DataTable();
				ptblFeatures.Columns.Add("ItemID");
				ptblFeatures.Columns.Add("FeatureText");
				//ptblFeatures.Columns.Add("ID");
				ptblFeatures.Rows.Add(ptblFeatures.NewRow());
				return ptblFeatures;
			}
		}

		public DataTable GetPrice()
		{
			if (ptblPrices != null)
				return ptblPrices;
			else
			{ 
				ptblPrices  = new DataTable();
				ptblPrices  = db.GetTableRecords("sp_CustType_List","CustPrice");
				return ptblPrices;
			}
		}

		public DataTable GetServiceProvider()
		{
			if (ptblSP != null)
				return ptblSP;
			else
			{ 
				ptblSP  = new DataTable();
				ptblSP  = db.GetTableRecords("sp_SP_List","SP");
				return ptblSP;
			}
		}

		private string GetFeatureText()
		{
			StringBuilder osb = new StringBuilder();
			if (ptblFeatures != null)
			{
				if (ptblFeatures.Rows.Count > 0)
				{
					foreach (DataRow dRow in ptblFeatures.Rows)
					{
						osb.Append(dRow["FeatureText"] + "#");
					}
					return osb.ToString();
				}
				else
					return null;
			}
			else
				return null;
		}

		private string GetPriceText()
		{
			
			StringBuilder osb = new StringBuilder();
			if (ptblPrices != null)
			{
				if (ptblPrices.Rows.Count > 0)
				{
					foreach (DataRow dRow in ptblPrices.Rows)
					{
						osb.Append(dRow["CustTypeID"] + "|" + (dRow["Price"] != DBNull.Value?dRow["Price"]:"0") + "#");
					}
					return osb.ToString();
				}
				else
					return null;
			}
			else
				return null;
		}

		private string GetSPText()
		{
			
			StringBuilder osb = new StringBuilder();
			if (ptblSP != null)
			{
				if (ptblSP.Rows.Count > 0)
				{
					foreach (DataRow dRow in ptblSP.Rows)
					{
						if (dRow["chk"] != DBNull.Value)
							if (dRow["chk"].ToString().ToUpper().Equals("TRUE"))
								osb.Append(dRow["SPID"] + "#");
					}
					return osb.ToString();
				}
				else
					return null;
			}
			else
				return null;
		}
		private string GetItemIntText()
		{
			
			StringBuilder osb = new StringBuilder();
			if (ptblItemInt != null)
			{
				if (ptblItemInt.Rows.Count>0)
				{
					foreach (DataRow dRow in ptblItemInt.Rows)
					{
						if (dRow["RItemID"] != DBNull.Value)
							osb.Append(dRow["RItemID"] + "#");
					}
					return osb.ToString();
				}
				else
					return null;
			}
			else
				return null;
		}


        public System.Data.SqlClient.SqlDataReader GetPhoneList(string psPhoneType, string psManu, string psModel, string psNew, string psOld, string psRef)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@PType", psPhoneType);
			objCompHash.Add ("@PManu", psManu);
			objCompHash.Add ("@PModel", psModel);
			objCompHash.Add ("@PNew", psNew);
			objCompHash.Add ("@POld", psOld);
			objCompHash.Add ("@PRef", psRef);

			arrSpFieldSeq = new string[]{"@PType","@PManu","@PModel","@PNew","@POld","@PRef"};
            System.Data.SqlClient.SqlDataReader oReader;
			oReader = db.GetReaderRecords(objCompHash,"sp_item_List",arrSpFieldSeq);
			if (oReader.HasRows == false)
				return null;
			else
				return oReader;
		}

		public DataTable GetPhones()
		{
			DataTable dt = new DataTable();
			dt = db.GetTableRecords("sp_phone_List","Items");
			return dt;
		}

		public DataTable GetPTypes(string psPType)
		{
			DataTable dt = new DataTable();
			if (psPType != null)
			{
				string[] arrSpFieldSeq;
				Hashtable objCompHash = new Hashtable();
				objCompHash.Add ("@PType", psPType);
				arrSpFieldSeq = new string[]{"@PType"};
				dt = db.GetTableRecords(objCompHash,"sp_PType_List",arrSpFieldSeq);
			}
			else
			{
				dt = db.GetTableRecords("sp_PType_List","PTypes");
			}
			return dt;
		}

		public DataTable GetSimilarItems(string psItemID)
		{
			DataTable dt = new DataTable();
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@ItemID", psItemID);

			arrSpFieldSeq = new string[]{"@ItemID"};
			return  db.GetTableRecords(objCompHash,"sp_Phone_Similar_List",arrSpFieldSeq);
//			return dt;
		}

		public DataTable GetAccessories(string psItemID)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@ItemID", psItemID);

			arrSpFieldSeq = new string[]{"@ItemID"};
			return  db.GetTableRecords(objCompHash,"sp_Phone_Acc_List",arrSpFieldSeq);
		}

		public static DataSet GetPhones(string psPhoneType, string psManu, string psModel, string psNew, string psOld, string psRef)
		{
			DBConnect db = new DBConnect();
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@PType", psPhoneType);
			objCompHash.Add ("@PManu", psManu);
			objCompHash.Add ("@PModel", psModel);
			objCompHash.Add ("@PNew", psNew);
			objCompHash.Add ("@POld", psOld);
			objCompHash.Add ("@PRef", psRef);

			arrSpFieldSeq = new string[]{"@PType","@PManu","@PModel","@PNew","@POld","@PRef"};
			return db.GetDataSet(objCompHash,"sp_item_List",arrSpFieldSeq);

		}
	}

	
	public class clsCustType
	{
		private DBConnect db = new DBConnect();
		private string strInsertSQL = "sp_Cust_Insert" ;
		private string strDeleteSQL = "sp_Cust_Delete" ;
		public DataTable GetCustType()
		{ 
				return  db.GetTableRecords("sp_CustType_List","CustPrice"); 
		}

		public string SetRecords(int method, int iCustID, string sCustType)
		{	
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				switch(method)
				{ 
					case 1:
						if (iCustID != 0)
							objCompHash.Add ("@ivCustID", iCustID);
						
						objCompHash.Add ("@ivCustType", sCustType);
						arrSpFieldSeq = new string[]{"@ivCustID","@ivCustType"};
						db.ExeCommand(objCompHash, strInsertSQL, arrSpFieldSeq);
						break;
					case 2:
						objCompHash.Add ("@ivCustID", iCustID);
						arrSpFieldSeq = new string[]{"@ivCustID"};
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

		public void SetDefault(string sCustID)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@ivCustID", sCustID);
			arrSpFieldSeq = new string[]{"@ivCustID"};
			db.ExeCommand(objCompHash, "sp_Cust_Default", arrSpFieldSeq);
		}

		public string GetDefault()
		{
			string sDefCode;
			db.ExeCommand(null, "sp_Get_Cust_Default", null,"@DefCustID",out sDefCode);
			return sDefCode;
		}
	




	}


	public class clsContent
	{
		private DBConnect db = new DBConnect();
		public DataSet GetContent(string PhoneType, string Manuf, string Special, string sNew, string sOld, string sRef, string sPhoneText,string sDefCust,string sSP,string sAcc)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@PhoneType", PhoneType);
			objCompHash.Add ("@Manuf", Manuf);
			objCompHash.Add ("@Special", Special);
			objCompHash.Add ("@Cond_New", sNew);
			objCompHash.Add ("@Cond_Used", sOld);
			objCompHash.Add ("@Cond_Ref", sRef);
			objCompHash.Add ("@PhoneText", sPhoneText);
			objCompHash.Add ("@CustTypeID", sDefCust);
			objCompHash.Add ("@SP", sSP);
			objCompHash.Add ("@Acc", sAcc);
			arrSpFieldSeq = new string[]{"@PhoneType","@Manuf","@Special","@Cond_New","@Cond_Used","@Cond_Ref","@PhoneText","@CustTypeID","@SP","@Acc"};
			return db.GetDataSet(objCompHash, "sp_Content_select", arrSpFieldSeq);
		}
	}
}
