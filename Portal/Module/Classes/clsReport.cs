//using System.Data.OleDb;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsReport.
	/// </summary>
	public class clsReport
	{
		private static DBConnect db = new DBConnect();


		public clsReport()
		{
		}

		public static DataTable GetSales(string sSDate, string sEDate, string sDur, string sDType, string sPType, string sManuf, string sPhone, string sSP, string sReportType, string sCust, string sSale)
		{
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			objCompHash.Add ("@PhoneType", sPType);
			objCompHash.Add ("@Manuf", sManuf);
			objCompHash.Add ("@SP", sSP);
			objCompHash.Add ("@Title", sPhone);
			objCompHash.Add ("@SDate", sSDate);
			objCompHash.Add ("@EDate", sEDate);
			objCompHash.Add ("@Dur", sDur);
			objCompHash.Add ("@RType", sReportType);
			objCompHash.Add ("@Cust", sCust);
			objCompHash.Add ("@SalePerson", sSale);

			arrSpFieldSeq = new string[]{"@PhoneType","@Manuf","@SP","@Title","@SDate","@EDate","@Dur","@RType","@Cust","@SalePerson"};
			return  db.GetTableRecords(objCompHash,"sp_Rep_Sales",arrSpFieldSeq);
		}
	
       
    }
}
