using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{

    public class clsCustomer
    {
        private int _companyID;
        private string _companyname;
        private string _companyAccountNumber; 
        private string _addess1;
        private string _address2;
        private string _address3;
        private string _city;
        private string _state;
        private string _zip;
        private string _email;
        private string _country;
        private int _UserID;
        private List<salesPerson> _salesPersonlist;

        public List<salesPerson> SalesPersonlist
        {
            get
            {
                return _salesPersonlist;
            }
            set
            {
                _salesPersonlist = value;
            }
        }

        public int CompanyID
        {
            get 
            { 
               return  _companyID; 
            }
            set
            {
                _companyID = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return _companyname;
            }
            set
            {
                _companyname = value;
            }
        }
        public string CompanyAccountNumber
        {
            get
            {
                return _companyAccountNumber;
            }
            set
            {
                _companyAccountNumber = value;
            }
        }
        public string Address1
        {
            get
            {
                return _addess1;
            }
            set
            {
                _addess1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }
        public string Address3
        {
            get
            {
                return _address3;
            }
            set
            {
                _address3 = value;
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


    }
    public class salesPerson
    {

        private int _userid;
        private string _userName;
        private string _password;
        private string _email;
        private int _companyid;
        private int _accountStatusid;
        private string _userType;
        
        public int AccountStatusID
        {
            get
            {
                return _accountStatusid;
            }
            set
            {
                _accountStatusid = value;
            }
        }

        

        public int CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                _companyid = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

       

        public int UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
            }
        }
        
    }
    public class CustomerUtility
    {
        //select sales person
        public List<salesPerson> getSalesPersonList(int companyid)
        {
            List<salesPerson> objUserList = new List<salesPerson>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyid);

                

                arrSpFieldSeq = new string[] { "@CompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_salesPerson_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        salesPerson objuser = new salesPerson();

                        objuser.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        objuser.Password = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                        objuser.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objuser.AccountStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccountStatus", 0, false));
                        objuser.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userid", 0, false));
                        objuser.UserType = clsGeneral.getColumnData(dataRow, "usertype", string.Empty, false) as string;
                        objuser.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        
                        objUserList.Add(objuser);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objUserList;
        }
        //Delete Customer
        public void DeleteCustomer(int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@CompanyID" };
                objCompHash.Add("@CompanyID", companyID);
                db.ExeCommand(objCompHash, "av_tblcompany_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                arrSpFieldSeq = null;
                db = null;
                objCompHash = null;
            }
        }

        //Add Customer
        public int InsertNewCustomer(int companyID, string companyName, string companyAccNumber, string add1, string add2, string add3,
            string city, string state,string country,string zip,string email, string userIdXml)
        {
            
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                 if (!string.IsNullOrEmpty(companyName))
                {
                    objCompHash.Add("@companyID", companyID);
                    objCompHash.Add("@companyName", companyName);
                    objCompHash.Add("@companyAccNumber", companyAccNumber);
                    objCompHash.Add("@add1", add1);
                    objCompHash.Add("@add2", add2);
                    objCompHash.Add("@add3", add3);
                    objCompHash.Add("@city", city);
                    objCompHash.Add("@state", state);
                    objCompHash.Add("@country", country);
                    objCompHash.Add("@zip", zip);
                    objCompHash.Add("@email", email);
                    objCompHash.Add("@userIdXml", userIdXml);


                    arrSpFieldSeq = new string[] { "@companyID", "@companyName", "@companyAccNumber", "@add1", "@add2", "@add3", "@city", "@state","@country","@zip", "@email", "@userIdXml" };


                    dt = db.GetTableRecords(objCompHash, "av_tblCompany_INSERTupdate", arrSpFieldSeq);
                   
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                arrSpFieldSeq = null;
                db = null;
                objCompHash = null;
            }
            return companyID;
        }
        //Select Customer
        public List<clsCustomer> getCustomerlist(int companyID,string companyName, string companyAccNumber, string state, string salesPerson, int editflag,string email)
        {
            List<clsCustomer> CustomerList = new List<clsCustomer>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@companyName", companyName);
                objCompHash.Add("@companyAccountNumber", companyAccNumber);
                objCompHash.Add("@state", state);
                objCompHash.Add("@salesPerson", salesPerson);
                objCompHash.Add("@editflag", editflag);
                objCompHash.Add("@email", email);




                arrSpFieldSeq = new string[] { "@companyName", "@companyAccountNumber", "@state", "@salesPerson", "@companyID", "@editflag","@email" };

                dataTable = db.GetTableRecords(objCompHash, "av_tblCompany_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    UserUtility objUser = new UserUtility();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        clsCustomer objCustomer = new clsCustomer();
                        objCustomer.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID",0, false));
                        objCustomer.Address1 = clsGeneral.getColumnData(dataRow, "address1",string.Empty, false) as string;
                        objCustomer.Address2 = clsGeneral.getColumnData(dataRow, "address2", string.Empty, false) as string;
                        objCustomer.Address3 = clsGeneral.getColumnData(dataRow, "address3", string.Empty, false) as string;
                        objCustomer.City= clsGeneral.getColumnData(dataRow, "city", string.Empty, false) as string;
                        objCustomer.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "companyAccountNumber", string.Empty, false) as string;
                        objCustomer.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objCustomer.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objCustomer.State = clsGeneral.getColumnData(dataRow, "state", string.Empty, false) as string;
                        objCustomer.Zip = clsGeneral.getColumnData(dataRow, "zip", string.Empty, false) as string;
                        objCustomer.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userid", 0, false));
                        objCustomer.Country = clsGeneral.getColumnData(dataRow, "country", string.Empty, false) as string;
                        //objCustomer.SalesPersonlist = getSalesPersonList(objCustomer.CompanyID);
                        CustomerList.Add(objCustomer);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return CustomerList;
        }

        public clsCustomer getCustomerInfo(int companyID, string companyName, string companyAccNumber, string state, string salesPerson, int editflag, string email)
        {
            clsCustomer objCustomer = new clsCustomer();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@companyName", companyName);
                objCompHash.Add("@companyAccountNumber", companyAccNumber);
                objCompHash.Add("@state", state);
                objCompHash.Add("@salesPerson", salesPerson);
                objCompHash.Add("@editflag", editflag);
                objCompHash.Add("@email", email);


                arrSpFieldSeq = new string[] { "@companyName", "@companyAccountNumber", "@state", "@salesPerson", "@companyID", "@editflag", "@email" };

                dataTable = db.GetTableRecords(objCompHash, "av_tblCompany_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        
                        objCustomer.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID", 0, false));
                        objCustomer.Address1 = clsGeneral.getColumnData(dataRow, "address1", string.Empty, false) as string;
                        objCustomer.Address2 = clsGeneral.getColumnData(dataRow, "address2", string.Empty, false) as string;
                        objCustomer.Address3 = clsGeneral.getColumnData(dataRow, "address3", string.Empty, false) as string;
                        objCustomer.City = clsGeneral.getColumnData(dataRow, "city", string.Empty, false) as string;
                        objCustomer.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "companyAccountNumber", string.Empty, false) as string;
                        objCustomer.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objCustomer.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objCustomer.State = clsGeneral.getColumnData(dataRow, "state", string.Empty, false) as string;
                        objCustomer.Zip = clsGeneral.getColumnData(dataRow, "zip", string.Empty, false) as string;
                        objCustomer.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userid", 0, false));
                        objCustomer.Country = clsGeneral.getColumnData(dataRow, "country", string.Empty, false) as string;
                        objCustomer.SalesPersonlist = getSalesPersonList(objCustomer.CompanyID);
                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return objCustomer;
        }
    }
}
