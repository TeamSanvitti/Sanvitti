using System;
using System.Collections;
using System.Data;
using System.Text;
//using System.Data.OleDb;
namespace avii.Classes
{
	/// <summary>
	/// Summary description for Regis.
	/// </summary>
	public class clsRegis
	{
		#region "Private Objects"
		private DBConnect db= new DBConnect();
//		private clsCommon objCls ;
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
		private string strStores;
		private string strCustTypeID;
		private string strSP;
		#endregion

		#region "Public Properties"
		public string ServiceProvider
		{
			get { return strSP;}
			set { strSP = value;}
		}
		public string Stores
		{
			get { return strStores;}
			set { strStores = value;}
		}
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

		public string CustTypeID
		{
			get { return strCustTypeID;}
			set { if (value.Length > 0 )
					  strCustTypeID = value ;
				  else
					  strCustTypeID = null;
				}
		}

		public long LoginID
		{
			get { return lngLoginID;}
			set { lngLoginID = value;}
		}


		#endregion

		public DataTable GetCustInfo(string sCustID)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			arrSpFieldSeq = new string[]{"@ivCustID"};
			try
			{ 
				if (sCustID != null)
				{
					objCompHash.Add ("@ivCustID", sCustID);
					
					return db.GetTableRecords(objCompHash, "sp_Cust_List", arrSpFieldSeq);
				}	
				else
					return  db.GetTableRecords(objCompHash,"sp_Cust_List", arrSpFieldSeq); 
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		
		public string SetRecords(int method)
		{	
			string sCode = string.Empty;
			string[] arrSpFieldSeq;
			Hashtable objCompHash = new Hashtable();
			//			try
			//			{
			switch(method)
			{ 
				case 1:


					objCompHash.Add ("@UserName", strUserName);
					objCompHash.Add ("@Pwd", strPassword);
					objCompHash.Add ("@FirstName", strFirstName);
					objCompHash.Add ("@LastName", strLastName);
					objCompHash.Add ("@MiddleName", strMiddleName);
					objCompHash.Add ("@Address1", strAddress1);
					objCompHash.Add ("@Address2", strAddress2);
					objCompHash.Add ("@City", strCity);
					objCompHash.Add ("@State", strState);
					objCompHash.Add ("@Zip", strZip);
					objCompHash.Add ("@HomePhone", strHomePhone);
					objCompHash.Add ("@OfficePhone", strOfficePhone);
					objCompHash.Add ("@CellPhone", strCellPhone);
					objCompHash.Add ("@Email", strEmailAddress);

					objCompHash.Add ("@BillFirstName", strBillFirstName);
					objCompHash.Add ("@BillLastName", strBillLastName);
					objCompHash.Add ("@BillMiddleName", strBillMiddleName);
					objCompHash.Add ("@BillAddress1", strBillAddress1);
					objCompHash.Add ("@BillAddress2", strBillAddress2);
					objCompHash.Add ("@BillCity", strBillCity);
					objCompHash.Add ("@BillState", strBillState);
					objCompHash.Add ("@BillZip", strBillZip);
					objCompHash.Add ("@LoginID", lngLoginID);
					objCompHash.Add ("@Stores", strStores);
					objCompHash.Add ("@SP", strSP);
					objCompHash.Add ("@CustomerTypeID", strCustTypeID);
					
					arrSpFieldSeq = new string[]{"@UserName","@Pwd","@FirstName","@LastName","@MiddleName","@Address1","@Address2","@City","@State","@Zip","@HomePhone","@OfficePhone","@CellPhone", "@Email","@BillFirstName","@BillLastName","@BillMiddleName","@BillAddress1","@BillAddress2","@BillCity","@BillState","@BillZip","@LoginID","@CustomerTypeID","@Stores","@SP"};
					db.ExeCommand(objCompHash, "sp_Regis_Insert", arrSpFieldSeq,"@Code",out sCode);
					break;
			}
			
			return sCode;
		}


		public Int32 iValidateLogin(string sLoginName)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			arrSpFieldSeq = new string[]{"@LoginID"};
			try
			{ 
				if (sLoginName != null)
				{
					objCompHash.Add ("@LoginID", sLoginName);
					
					return db.GetTableRecords(objCompHash, "sp_Logon_Validate", arrSpFieldSeq).Rows.Count;
				}	
				else
					return  0; 
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		public Int32 iValidateLogin(string sLoginName, string sEmail)
		{
			string[] arrSpFieldSeq;
			string sCode = string.Empty;
			Hashtable objCompHash = new Hashtable();
			arrSpFieldSeq = new string[]{"@LoginID","@Email"};
			try
			{ 
				if (sLoginName != null)
				{
					objCompHash.Add ("@LoginID", sLoginName);
					objCompHash.Add ("@Email", sEmail);
					
					return db.GetTableRecords(objCompHash, "sp_Logon_Validate", arrSpFieldSeq).Rows.Count;
				}	
				else
					return  0; 
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

        public Int32 iValidateFullfillmentLogin(string sLoginName, string sEmail)
        {
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@Username", "@Email" };
            try
            {
                if (sLoginName != null)
                {
                    objCompHash.Add("@Username", sLoginName);
                    objCompHash.Add("@Email", sEmail);

                    return db.GetTableRecords(objCompHash, "Aero_GetPassword", arrSpFieldSeq).Rows.Count;
                }
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


		public static  int ChangePwd(string sLogonName, string sEmail,string sPath)
		{
			string[] arrSpFieldSeq;
			string sPwd = string.Empty;
			string sEmailTXT = string.Empty;
			DBConnect db = new DBConnect();
			Classes.clsGeneral oGen = new Classes.clsGeneral();
			Hashtable objCompHash = new Hashtable();
			arrSpFieldSeq = new string[]{"@LoginID","@Pwd"};
			try
			{ 
				sPwd = GetAutoPwd(16);
				sEmailTXT = Classes.clsGeneral.fnEmailHTML(sPath);
				sEmailTXT = sEmailTXT.Replace("#pass# ",sPwd);
				if (sPwd.Length > 0)
				{
					if (sLogonName != null)
					{
						objCompHash.Add ("@LoginID", sLogonName);
						objCompHash.Add ("@Pwd", sPwd);
					
						db.ExeCommand(objCompHash, "sp_Logon_Pwd", arrSpFieldSeq);
                        //Classes.clsGeneral.SendEmail(sEmail,"j_bhargva@hotmail.com","New Password",sEmailTXT);
						return 0;
					}	
					else
						return  -1; 
				}
				else
					return -1;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


        public static int GetFullfillmentPwd(string Username, string Email, string sPath)
        {
            string[] arrSpFieldSeq;
            //OleDbDataReader fnResult;
            string sEmailTXT = string.Empty;
            string sPwd = string.Empty;
            DBConnect db = new DBConnect();
            Classes.clsGeneral oGen = new Classes.clsGeneral();
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@Username", "@Email", "@Pwd" };
            try
            {
                objCompHash.Add("@Username", Username);
                objCompHash.Add("@Email", Email);
                db.ExeCommand(objCompHash, "Aero_GetPassword", arrSpFieldSeq, "@Password", out sPwd);
                if (sPwd.Length > 0)
                {
                    sEmailTXT = Classes.clsGeneral.fnEmailHTML(sPath);
                    sEmailTXT = sEmailTXT.Replace("#pass# ", sPwd);

                    //Classes.clsGeneral.SendEmail(Email, "j_bhargva@hotmail.com", "New Password", sEmailTXT);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

		public static string  GetAutoPwd(Int32 iLength)
		{
			Random rnd = new Random();
			Int32 iInt;
			StringBuilder oSB = new StringBuilder();
			for (Int32 iCtr = 0;iCtr <= iLength; iCtr++)
			{
				iInt = rnd.Next(48,122);
				if ((iInt >= 48 && iInt <= 57) || (iInt >= 65 && iInt <= 90) || (iInt >= 97 && iInt <= 122))
				{
					oSB.Append((Char)iInt);
					iCtr = iCtr + 1;
				}
			}
			return oSB.ToString();
		}


		
		public clsRegis()
		{
		}
	}
}
