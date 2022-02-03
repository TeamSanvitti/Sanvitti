using System;
//using System.Data.OleDb;
using System.Collections;
using System.Data;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsCust.
	/// </summary>
	 public class clsCust
	{
		 #region "Private Objects"
		 private DBConnect db= new DBConnect();
//		 private clsCommon objCls ;
		 #endregion

		 #region "Private Variables"
		 private string strUserName;
		 private string strPassword;
		 private string strConfirmPassword;
		 private long lngLoginID;

		 private string strFirstName;
		 private string strLastName;
		 private string strMiddleName;
		 private string strAddress1;
		 private string strAddress2;
		 private string strCity;
		 private string strState;
		 private string strZip;
		 private string strAddressType;

		 private string strBillFirstName;
		 private string strBillLastName;
		 private string strBillMiddleName;
		 private string strBillAddress1;
		 private string strBillAddress2;
		 private string strBillCity;
		 private string strBillState;
		 private string strBillZip;

		 private string strHomePhone;
		 private string strOfficePhone;
		 private string strCellPhone;
		 private string strEmailAddress;
		 #endregion

		 #region "Constructor"
			public clsCust()
		{
			
		}

		 #endregion

		 #region "Public Properties"
		 public string UserName
		 {
			 get { return strUserName;}
			 set { strUserName = value;}
		 }

		 public string Password
		 {
			 get { return strPassword;}
			 set { strPassword = value;}
		 }

		 public string ConfirmPassword
		 {
			 get { return strConfirmPassword;}
			 set { strConfirmPassword = value;}
		 }

		 public string FirstName
		 {
			 get { return strFirstName;}
			 set { strFirstName = value;}
		 }

		 public string LastName
		 {
			 get { return strLastName;}
			 set { strLastName = value;}
		 }

		 public string MiddleName
		 {
			 get { return strMiddleName;}
			 set { strMiddleName = value;}
		 }

		 public string Address1
		 {
			 get { return strAddress1;}
			 set { strAddress1 = value;}
		 }

		 public string Address2
		 {
			 get { return strAddress2;}
			 set { strAddress2 = value;}
		 }
		 public string City
		 {
			 get { return strCity;}
			 set { strCity = value;}
		 }
		 public string State
		 {
			 get { return strState;}
			 set { strState = value;}
		 }
		 public string Zip
		 {
			 get { return strZip;}
			 set { strZip = value;}
		 }
		 public string AddressType
		 {
			 get { return strAddressType;}
			 set { strAddressType = value;}
		 }

		 public string BillFirstName
		 {
			 get { return strBillFirstName;}
			 set { strBillFirstName = value;}
		 }

		 public string BillLastName
		 {
			 get { return strBillLastName;}
			 set { strBillLastName = value;}
		 }

		 public string BillMiddleName
		 {
			 get { return strBillMiddleName;}
			 set { strBillMiddleName = value;}
		 }

		 public string BillAddress1
		 {
			 get { return strBillAddress1;}
			 set { strBillAddress1 = value;}
		 }

		 public string BillAddress2
		 {
			 get { return strBillAddress2;}
			 set { strBillAddress2 = value;}
		 }
		 public string BillCity
		 {
			 get { return strBillCity;}
			 set { strBillCity = value;}
		 }
		 public string BillState
		 {
			 get { return strBillState;}
			 set { strBillState = value;}
		 }
		 public string BillZip
		 {
			 get { return strBillZip;}
			 set { strBillZip = value;}
		 }

		 public string HomePhone
		 {
			 get { return strHomePhone;}
			 set { strHomePhone = value;}
		 }
		 public string OfficePhone
		 {
			 get { return strOfficePhone;}
			 set { strOfficePhone = value;}
		 }
		 public string CellPhone
		 {
			 get { return strCellPhone;}
			 set { strCellPhone = value;}
		 }
		 public string EmailAddress
		 {
			 get { return strEmailAddress;}
			 set { strEmailAddress = value;}
		 }


		 public long LoginID
		 {
			 get { return lngLoginID;}
			 set { lngLoginID = value;}
		 }
		 #endregion

		 public DataTable ValidateUser(string sLogOn, string sPwd)
		 {
			 string[] arrSpFieldSeq;
			 string sCode = string.Empty;
			 Hashtable objCompHash = new Hashtable();
			 try
			 { 
				 if (sLogOn != null && sPwd != null)
				 {
					 objCompHash.Add ("@Logon", sLogOn);
					 objCompHash.Add ("@Pwd", sPwd);
					 arrSpFieldSeq = new string[]{"@Logon","@Pwd"};
					 return db.GetTableRecords (objCompHash, "sp_User_Logon", arrSpFieldSeq);
				 }	
				 else
					 throw new Exception("Please enter Username and Password"); 
			 }
			 catch (Exception exp)
			 {
				 throw exp;
			 }
		 }


		 public DataTable ValidateAdm(string sLogOn, string sPwd)
		 {
			 string[] arrSpFieldSeq;
			 string sCode = string.Empty;
			 Hashtable objCompHash = new Hashtable();
			 try
			 { 
				 if (sLogOn != null && sPwd != null)
				 {
					 objCompHash.Add ("@Logon", sLogOn);
					 objCompHash.Add ("@Pwd", sPwd);
					 arrSpFieldSeq = new string[]{"@Logon","@Pwd"};
					 return db.GetTableRecords (objCompHash, "pAdmLogon", arrSpFieldSeq);
				 }	
				 else
					 throw new Exception("Please enter Username and Password"); 
			 }
			 catch (Exception exp)
			 {
				 throw exp;
			 }
		 }
		 public DataSet GetCustInfo(string sCustID)
		 {
			 string[] arrSpFieldSeq;
			 string sCode = string.Empty;
			 Hashtable objCompHash = new Hashtable();
			 try
			 { 
				 if (sCustID != null)
				 {
					 objCompHash.Add ("@ivCustID", sCustID);
					 arrSpFieldSeq = new string[]{"@ivCustID"};
					 return db.GetDataSet(objCompHash, "sp_Cust_List", arrSpFieldSeq);
				 }	
				 else
					return  db.GetDataSet("sp_Cust_List"); 
			 }
			 catch (Exception exp)
			 {
				 throw exp;
			 }
		 }

		 public  void DelCust(string sCustID)
		 {
			 string[] arrSpFieldSeq;
			 string sCode = string.Empty;
			 Hashtable objCompHash = new Hashtable();
			 try
			 { 
				 if (sCustID != null)
				 {
					 objCompHash.Add ("@LoginID", sCustID);
					 arrSpFieldSeq = new string[]{"@LoginID"};
					 db.GetDataSet(objCompHash, "sp_Regis_Delete", arrSpFieldSeq);
				 }	
			 }
			 catch (Exception exp)
			 {
				 throw exp;
			 }
		 }
		
		 public void SetRecords(string psCustID, string psCustTypeID, string psSalesID)
		 {	
			 string[] arrSpFieldSeq;
			 string sCode = string.Empty;
			 Hashtable objCompHash = new Hashtable();
			 try
			 { 
				objCompHash.Add ("@ivCustID", psCustID);
				 objCompHash.Add ("@ivCustTypeID", psCustTypeID);
				 objCompHash.Add ("@ivSalesID", (psSalesID.Length >0?psSalesID:null));
			
				arrSpFieldSeq = new string[]{"@ivCustID","@ivCustTypeID","@ivSalesID"};
				db.ExeCommand(objCompHash, "sp_Cust_Update", arrSpFieldSeq);
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
