using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsRIMA.
	/// </summary>
	public class clsRIMA
	{

		#region "Constructor"
		public clsRIMA()
		{
		}
		#endregion

		#region "Private variables"
		private DateTime pdDt, psInvDate;
		private string psComp, psNm, psActNum, psPhone, psFax, psInvNum, psRmaNum;
		private bool pbNew, pbDef, pbWrn, pbNonWrn, pbAprv;
		private DataTable oDtTables;
		#endregion

		#region "Public Functions"


        public static void SetRMAStatus(int RMAID, int Status)
        {
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                DBConnect db = new DBConnect();
                objCompHash.Add("@RMID", RMAID);
                objCompHash.Add("@Status", Status);
                arrSpFieldSeq = new string[] { "@RMID", "@Status" };
                db.ExecuteNonQuery(objCompHash, "sp_RMA_UpdateStatus", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

        }


		public static DataSet GetRMAInfo(string sRMA)
		{
			string[] arrSpFieldSeq;
			DataSet oDs; 
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				DBConnect db = new DBConnect();
				objCompHash.Add("@RMID", sRMA);
				arrSpFieldSeq = new string[]{"@RMID"};
				oDs = db.GetDataSet(objCompHash, "sp_RMA_Info", arrSpFieldSeq);
				return oDs;
			}
			catch (Exception objExp)
			{
				throw new Exception (objExp.Message.ToString() );
			}
			finally
			{
				objCompHash  = null;
				arrSpFieldSeq = null;
				oDs = null;
			}
			
		}

		public static DataTable GetRMAList(string sDate,string  sComp,string  sAcc,string  sInv,string  sStat)
		{
			string[] arrSpFieldSeq;
			DataTable oDt; 
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				DBConnect db = new DBConnect();
				objCompHash.Add("@RMADt", sDate);
				objCompHash.Add("@AcNum", sAcc);
				objCompHash.Add("@Comp", sComp);
				objCompHash.Add("@InvNum", sInv);
				objCompHash.Add("@Status", sStat);
				arrSpFieldSeq = new string[]{"@RMADt","@AcNum","@Comp","@InvNum","@Status"};
				oDt = db.GetTableRecords(objCompHash, "sp_RMA_List", arrSpFieldSeq);
				return oDt;
			}
			catch (Exception objExp)
			{
				throw new Exception (objExp.Message.ToString() );
			}
			finally
			{
				objCompHash  = null;
				arrSpFieldSeq = null;
				oDt = null;
			}
			
		}

        public static DataTable GetRMAReport(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            string[] arrSpFieldSeq;
            DataTable oDt;
            Hashtable objCompHash = new Hashtable();
            DBConnect db = new DBConnect();
            try
            {
                objCompHash.Add("@rmanumber", rmaNumber);
                objCompHash.Add("@rmaFromdate", sFromDate);
                objCompHash.Add("@rmaTodate", sToDate);
                objCompHash.Add("@rmastatusID", statusID);
                objCompHash.Add("@companyid", companyid);
                objCompHash.Add("@UPC", upc);
                objCompHash.Add("@ESN", esn);
                arrSpFieldSeq = new string[] { "@rmanumber", "@rmaFromdate", "@rmaTodate", "@rmastatusID", "@companyid", "@UPC", "@ESN" };
                oDt = db.GetTableRecords(objCompHash, "av_RMA_Detail_Report", arrSpFieldSeq);
                return oDt;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
                oDt = null;
                db.DBClose();
            }

        }

        public static DataTable GetRMASummary(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            string[] arrSpFieldSeq;
            DataTable oDt;
            Hashtable objCompHash = new Hashtable();
            DBConnect db = new DBConnect();
            try
            {
                objCompHash.Add("@rmanumber", rmaNumber);
                objCompHash.Add("@rmaFromdate", sFromDate);
                objCompHash.Add("@rmaTodate", sToDate);
                objCompHash.Add("@rmastatusID", statusID);
                objCompHash.Add("@companyid", companyid);
                objCompHash.Add("@UPC", upc);
                objCompHash.Add("@ESN", esn);
                arrSpFieldSeq = new string[] { "@rmanumber", "@rmaFromdate", "@rmaTodate", "@rmastatusID", "@companyid", "@UPC", "@ESN" };
                oDt = db.GetTableRecords(objCompHash, "av_RMA_Summary", arrSpFieldSeq);
                return oDt;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
                oDt = null;
                db.DBClose();
            }

        }

        public static string SetRecord(string sXML)
		{

			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				DBConnect db = new DBConnect();
				objCompHash.Add("@xml", sXML);
				arrSpFieldSeq = new string[]{"@xml"};
				db.ExeCommand(objCompHash, "p_RMA_Insert", arrSpFieldSeq,"Code",out sCode);
				
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
			return sCode;
		}

		public static void DeleteRecord(string sRMAID)
		{

			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			try
			{ 
				DBConnect db = new DBConnect();
				objCompHash.Add("@RMID", sRMAID);
				arrSpFieldSeq = new string[]{"@RMID"};
				db.ExeCommand(objCompHash, "sp_RMA_Delete", arrSpFieldSeq);
				
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

		public static DataTable GetRIMA()
		{
			DataTable oDt = new DataTable();
			if (oDt.Columns.Count ==0)
			{
				oDt.Columns.Add("ID");
				oDt.Columns.Add("RMAID");
				oDt.Columns.Add("Brand");
				oDt.Columns.Add("Model");
				oDt.Columns.Add("Esn");
				oDt.Columns.Add("Reason");
				oDt.Columns.Add("CallTime");
				oDt.Columns.Add("Qty");
			}
			return oDt;
		}


		#endregion



	}
}
