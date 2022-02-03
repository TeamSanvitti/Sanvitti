using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsOrder.
	/// </summary>
	public class clsOrder
	{
		private DBConnect db = new DBConnect();
		public clsOrder()
		{
		}

		public string SetOrder(string sCustID, string sCTypeId, string sOrderNum,  string sComments, string sItems)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				objCompHash.Add ("@CustID", sCustID);
				objCompHash.Add ("@CtID", sCTypeId);
				objCompHash.Add ("@OrderNum", null);
				objCompHash.Add ("@Comment", sComments);
				objCompHash.Add ("@OrderDet", sItems);
						
				arrSpFieldSeq = new string[]{"@CustID","@CtID","@OrderNum", "@Comment","@OrderDet"};
				db.ExeCommand(objCompHash, "sp_Order_Insert", arrSpFieldSeq,"@Code",out sCode);
				return sCode;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void DeleteOrder(string sOid)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				objCompHash.Add ("@OID", sOid);
				arrSpFieldSeq = new string[]{"@OID"};
				db.ExeCommand(objCompHash, "sp_Order_Delete", arrSpFieldSeq);			
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
		public void SetOrderStatus(string sAction, string sOID,string sOrderNum,  string sComments, string OrdDate)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			if (sAction == "1")
			{
				try
				{ 
					objCompHash.Add ("@OID", sOID);
					objCompHash.Add ("@OrderNum", sOrderNum);
					objCompHash.Add ("@Comment", sComments);
					objCompHash.Add ("@OrderDt", OrdDate);
						
					arrSpFieldSeq = new string[]{"@OID", "@OrderNum", "@Comment","@OrderDt" };
					db.ExeCommand(objCompHash, "sp_OrderStat_Insert", arrSpFieldSeq);			
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else if (sAction == "2")
			{
				try
				{ 
					objCompHash.Add ("@ivID", sOID);			
					arrSpFieldSeq = new string[]{"@ivID"};
					db.ExeCommand(objCompHash, "sp_OrderStat_Delete", arrSpFieldSeq);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		} 
		public DataTable GetOrderStatus(string sOrderNum)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{
				objCompHash.Add ("@ivID", sOrderNum);
				arrSpFieldSeq = new string[]{"@ivID"};
				return db.GetTableRecords(objCompHash, "sp_OrderStat_Select", arrSpFieldSeq);;
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

	
		public DataSet GetOrderDetail(string sOrderNum)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{
				objCompHash.Add ("@ivID", sOrderNum);
				arrSpFieldSeq = new string[]{"@ivID"};
				return db.GetDataSet(objCompHash, "sp_Order_Select", arrSpFieldSeq);;
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

		public DataSet GetOrderPrint(string sOrderNum)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{
				objCompHash.Add ("@ivID", sOrderNum);
				arrSpFieldSeq = new string[]{"@ivID"};
				return db.GetDataSet(objCompHash, "sp_Order_Print", arrSpFieldSeq);;
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

		public DataTable GetOrderSearch(string sOrderNum, string sOrdDate, string sFName, string sLName)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{
				objCompHash.Add ("@ivID", sOrderNum);
				objCompHash.Add ("@ivDt", sOrdDate);
				objCompHash.Add ("@ivFName", sFName);
				objCompHash.Add ("@ivLName", sLName);
				arrSpFieldSeq = new string[]{"@ivID","@ivDt","@ivFName","@ivLName"};
				return db.GetTableRecords(objCompHash, "sp_Order_Search", arrSpFieldSeq);;
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
