using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SV.Framework.DAL.Catalog
{

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    public class DBConnect : IDisposable
	{
		//private OleDbConnection Conn;
        private SqlConnection Conn;
		private string ConnectionString;
        private string ConnectionString2;
        private string ConnectionString3;
        private bool Disposed = false;
		//private OleDbDataAdapter sqlDataAdptr;
        private SqlDataAdapter sqlDataAdptr;

        #region DISPOSE

        //Never mark this class as virtual as you do not want derived 
        //classes to be able to override it.
        public void Dispose()
        {
            //if this class is derived then call the base
            //class dispose.
            //base.Dispose();
            //Call the overloaded version of dispose
            Dispose(true);
            //Tell the CLR not to run the finalizer this way
            //you do not free unmanaged resources twice
            GC.SuppressFinalize(this);


        }

        private void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    DBClose();
                }
                if (sqlDataAdptr != null)
                {
                    sqlDataAdptr.Dispose();
                }
            }

            Disposed = true;
        }

        ~DBConnect()
        {
              Dispose(false);
        }

        #endregion
		

		public DBConnect()
		{
           //ConnectionString = "Data Source=sql5018.smarterasp.net;Initial Catalog=DB_A1EC2D_av;User ID=DB_A1EC2D_av_admin;Password=Test@12345;Integrated Security=False;";
            //ConnectionString = "Server=tcp:lansqldb.database.windows.net,1433;Initial Catalog=lanDB;Persist Security Info=False;User ID=lansqldbadmin;Password=lan@91605;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            if (ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString != null)
            {
                ConnectionString =  ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString; 
            }
            //else
            //{
            //    ConnectionString = "Server=tcp:lansqldb.database.windows.net,1433;Initial Catalog=lanDB;Persist Security Info=False;User ID=lansqldbadmin;Password=lan@91605;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            //    //throw new Exception("Connection string is not initialized");
            //}
        }

        public string DBProvider
		{
			get {		return ConnectionString;			}
			set	{		ConnectionString = value;			}
		}

        public SqlConnection DBConnection()
		{
            Conn = new System.Data.SqlClient.SqlConnection(ConnectionString);
			Conn.Open();
			return Conn;
		}

        private SqlConnection DBConnection2()
        {
            Conn = new System.Data.SqlClient.SqlConnection(ConnectionString2);
            Conn.Open();
            return Conn;
        }
        private SqlConnection DBConnection3()
        {
            Conn = new System.Data.SqlClient.SqlConnection(ConnectionString3);
            Conn.Open();
            return Conn;
        }

		public void ExeCommand(Hashtable HshParameters, string SQLString)
		{	
			IDictionaryEnumerator objDicEnum = HshParameters.GetEnumerator();
			SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
			try
			{
				if (HshParameters.Count > 0 )
				{
					while (objDicEnum.MoveNext())
					{
						//objCmd.Parameters.Add(objDicEnum.Key.ToString(), objDicEnum.Value);
                        objCmd.Parameters.AddWithValue(objDicEnum.Key.ToString(), objDicEnum.Value);
					}
					
				}
				objCmd.ExecuteNonQuery();
			}
			catch (Exception objEx)
			{
				throw objEx;
			}		
			finally
			{
				objDicEnum = null;
				objCmd = null;
                DBClose();
            }
		}

        public void ExeCommand2(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection2());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                        objCmd.ExecuteNonQuery();
                    }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return objCmd.CommandText.ToString();
        }

        public void ExeCommand3(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection3());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                        objCmd.ExecuteNonQuery();
                    }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return objCmd.CommandText.ToString();
        }
       
        public void ExeCommand(string SQLString)
        {
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            try
            {
                objCmd.ExecuteNonQuery();
            }
            catch (Exception objEx)
            {
                throw new Exception(objEx.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
        }

        public void ExeCommand(Hashtable HshParameters, string SQLString, ref SqlDataReader fnResult)
		{	
			IDictionaryEnumerator objDicEnum = HshParameters.GetEnumerator();
             SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
			try
			{	
				if (HshParameters.Count > 0 )
				{
					while (objDicEnum.MoveNext())
					{
						//objCmd.Parameters.Add(objDicEnum.Key.ToString(), objDicEnum.Value);
                        objCmd.Parameters.AddWithValue(objDicEnum.Key.ToString(), objDicEnum.Value);
					}
					
				}
				fnResult = objCmd.ExecuteReader();
				
			}
			catch (Exception exception)
            {
                throw exception;
            }
			finally
			{
				objDicEnum = null;
				objCmd = null;
                DBClose();
			}
		}

        public object ExecuteScalar(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            object resultObject = null;
            IDictionaryEnumerator objDicEnum = HshParameters.GetEnumerator();
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters.Count > 0)
                {
                    while (objDicEnum.MoveNext())
                    {
                        //objCmd.Parameters.Add(objDicEnum.Key.ToString(), objDicEnum.Value);
                        objCmd.Parameters.AddWithValue(objDicEnum.Key.ToString(), objDicEnum.Value);
                    }
                }

                resultObject = objCmd.ExecuteScalar();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objDicEnum = null;
                objCmd = null;
                DBClose();
            }
            return resultObject;
        }

        public int ExecuteNonQuery(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            int resultRows = 0;
            IDictionaryEnumerator objDicEnum = HshParameters.GetEnumerator();
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters.Count > 0)
                {
                    while (objDicEnum.MoveNext())
                    {
                        //objCmd.Parameters.Add(objDicEnum.Key.ToString(), objDicEnum.Value);
                        objCmd.Parameters.AddWithValue(objDicEnum.Key.ToString(), objDicEnum.Value);
                    }
                }

                int.TryParse(objCmd.ExecuteNonQuery().ToString(), out resultRows);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                objDicEnum = null;
                objCmd = null;
                DBClose();
            }
            return resultRows;
        }


		public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{	
			SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
			objCmd.CommandType = CommandType.StoredProcedure;
			try
			{
				if (HshParameters !=null && arrFieldsSeq !=null)
				if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
				{
					foreach(string strField in arrFieldsSeq)
					{				
						//objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
					}
					objCmd.ExecuteNonQuery();
				}
			}
			catch(Exception objExp) 
			{
				throw new Exception( objExp.Message.ToString());
			}
			finally
			{
				objCmd = null;
                DBClose();
			}
			//return objCmd.CommandText.ToString();
		}

        public void ExeCommand(SqlConnection sqlComm, Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            SqlCommand objCmd = new SqlCommand(SQLString, sqlComm);
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                        objCmd.ExecuteNonQuery();
                    }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
        }

		
		public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq,string sOutParam, out string sCode)
		{	
			SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
			objCmd.CommandType = CommandType.StoredProcedure;
			try
			{
				if (HshParameters !=null && arrFieldsSeq !=null)
				if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
				{
					foreach(string strField in arrFieldsSeq)
					{				
						//objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
					}
				}
				objCmd.Parameters.Add(sOutParam,SqlDbType.VarChar,20);
				objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output ;
				objCmd.ExecuteNonQuery();
				sCode = objCmd.Parameters[sOutParam].Value.ToString();

				//sCode = (string)objCmd.ExecuteScalar();
			}
			catch(Exception objExp) 
			{
				throw new Exception( objExp.Message.ToString());
			}
			finally
			{
                objCmd = null;
                DBClose();
			}
			//return objCmd.CommandText.ToString();
		}
        public int ExecCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                        DbParameter returnValue;
                        returnValue = objCmd.CreateParameter();
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        objCmd.Parameters.Add(returnValue);

                        objCmd.ExecuteNonQuery();
                        result = Convert.ToInt32(returnValue.Value);
                    }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            return result;
        }
        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, out int sCode1, out int sCode2)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }

        public int ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out string sCode)
        {
            int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandTimeout = 0;
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 50);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                sCode = objCmd.Parameters[sOutParam].Value.ToString();
                result = Convert.ToInt32(returnValue.Value);

                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            return result;
            //return objCmd.CommandText.ToString();
        }
        public int ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, out string sCode, out string sCode2)
        {
            int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandTimeout = 0;
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 50);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 20);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                sCode = objCmd.Parameters[sOutParam].Value.ToString();
                sCode2 = objCmd.Parameters[sOutParam2].Value.ToString();
                result = Convert.ToInt32(returnValue.Value);

                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            return result;
            //return objCmd.CommandText.ToString();
        }

        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                
                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                

                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, out int sCode1, out int sCode2)
        {
            //int result = 0;
            sCode1 = 0;
            sCode2 = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam1, SqlDbType.Int);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, out int sCode1, out int sCode2, out string sCode3)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam1, SqlDbType.Int);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 8000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                
                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
               

                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }

        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode, string sOutParam1, out int sCode1, string sOutParam2, out string sCode2)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam1, SqlDbType.Int);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 8000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExecCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode, string sOutParam1, out int sCode1, string sOutParam2, out string sCode2, string sOutParam3, out int sCode3)
        {
           // int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam1, SqlDbType.Int);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 8000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.Int);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();

                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToInt32(objCmd.Parameters[sOutParam3].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
           // return result;
            //return objCmd.CommandText.ToString();
        }

        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode, string sOutParam1, out int sCode1, string sOutParam2, out string sCode2, string sOutParam3, out bool sCode3)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam1, SqlDbType.Int);
             //   objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 8000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.Bit);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                
                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToBoolean(objCmd.Parameters[sOutParam3].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, string sOutParam6, out string sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5, out int sCode6)
        {
            int result = 0;
            sCode1 = string.Empty;
            sCode6 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam6, SqlDbType.Int);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToInt32(objCmd.Parameters[sOutParam6].Value);

                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }

        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, string sOutParam6, string sOutParam7, out string sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5, out string sCode6, out int sCode7)
        {
            int result = 0;
            sCode1 = string.Empty;
            sCode7 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;
            sCode6 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam6, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;
                
                
                objCmd.Parameters.Add(sOutParam7, SqlDbType.Int);
                objCmd.Parameters[sOutParam7].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToString(objCmd.Parameters[sOutParam6].Value);
                sCode7 = Convert.ToInt32(objCmd.Parameters[sOutParam7].Value);

                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }


        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, string sOutParam6, string sOutParam7, string sOutParam8, string sOutParam9, string sOutParam10, string sOutParam11, out string sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5, out string sCode6, out int sCode7, out string sCode8, out string sCode9, out string sCode10, out string sCode11)
        {
            int result = 0;
            sCode1 = string.Empty;
            sCode7 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;
            sCode6 = string.Empty;
            sCode8 = string.Empty;
            sCode9 = string.Empty;
            sCode10 = string.Empty;
            sCode11= string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam6, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;


                objCmd.Parameters.Add(sOutParam7, SqlDbType.Int);
                objCmd.Parameters[sOutParam7].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam8, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam8].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam9, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam9].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam10, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam10].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam11, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam11].Direction = ParameterDirection.Output;


                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToString(objCmd.Parameters[sOutParam6].Value);
                sCode7 = Convert.ToInt32(objCmd.Parameters[sOutParam7].Value);
                sCode8 = Convert.ToString(objCmd.Parameters[sOutParam8].Value);
                sCode9 = Convert.ToString(objCmd.Parameters[sOutParam9].Value);
                sCode10 = Convert.ToString(objCmd.Parameters[sOutParam10].Value);
                sCode11 = Convert.ToString(objCmd.Parameters[sOutParam11].Value);
                
                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }

        public int ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, out int sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5)
        {
            int result = 0;
            sCode1 = 0;
            //sCode2 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;
                
                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                
                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, out string sCode1, out string sCode2, out string sCode3, out string sCode4)
        {
            int result = 0;
            sCode1 = string.Empty;
            //sCode2 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            //sCode5 = string.Empty;
            sCode2 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                //objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 500);
                //objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                //sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);

                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
        public void ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, out string sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5)
        {
            int result = 0;
            sCode1 = string.Empty;
            //sCode2 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 2000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);

                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }

        public int ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, string sOutParam6, out int sCode1, out string sCode2, out string sCode3, out string sCode4, out string sCode5, out string sCode6)
        {
            int result = 0;
            sCode1 = 0;
            //sCode2 = 0;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;
            sCode2 = string.Empty;
            sCode6 = string.Empty;

            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                //objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 500);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 500);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 500);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 500);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam6, SqlDbType.VarChar, 500);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode1 = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                //sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToString(objCmd.Parameters[sOutParam6].Value);

                result = Convert.ToInt32(returnValue.Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            return result;
            //return objCmd.CommandText.ToString();
        }
        
        public void ExeCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode, string sOutParam2, out string sCode2)
        {
            //int result = 0;
            SqlCommand objCmd = new SqlCommand(SQLString, DBConnection());
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 8000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();
                //var outputParam = objCmd.Parameters[sOutParam].Value;
                //if (outputParam != DBNull)
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);


                //sCode = (string)objCmd.ExecuteScalar();
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCmd = null;
                DBClose();
            }
            //return result;
            //return objCmd.CommandText.ToString();
        }
		
		public DataSet GetAllRecords(string SQL,string strTableName)
		{
			sqlDataAdptr = new SqlDataAdapter(SQL, DBConnection());
			DataSet objDS = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				dt.TableName = strTableName;
				objDS.Tables.Add (dt);	
				sqlDataAdptr.Fill(dt);
				
			}
			catch (Exception ex)
			{
                throw ex;
			}
			finally
			{
				//objDS.Dispose();
				dt.Dispose();
                sqlDataAdptr.Dispose();
				sqlDataAdptr = null;
                DBClose();
			}
            return objDS;
		}

		public DataSet GetDataSet(string SQL)
		{
			DataSet ods = new DataSet() ;
			sqlDataAdptr = new SqlDataAdapter(SQL, DBConnection());
			try
			{
				sqlDataAdptr.Fill (ods);
				
			}
			catch (Exception ex)
			{
                throw ex;
			}
			finally
			{
				//ods = null;
                sqlDataAdptr.Dispose();
				sqlDataAdptr = null;
                DBClose();
			}
            return ods;
		}

        public DataSet GetDataSetRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode)
        {
            DataSet ods = new DataSet();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(ods);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return ods;
        }
        public DataSet GetDataSetRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out string sCode)
        {
            DataSet ods = new DataSet();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 20);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(ods);
                sCode = Convert.ToString(objCmd.Parameters[sOutParam].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return ods;
        }

        public DataSet GetDataSet(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{
			DataSet ods = new DataSet() ;
			SqlCommand objCmd;	
			try
			{
                
				objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
				objCmd.CommandType = CommandType.StoredProcedure;
				if (HshParameters !=null && arrFieldsSeq !=null)
				if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
				{
					foreach(string strField in arrFieldsSeq)
					{				
						//objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
					}				
				}
				sqlDataAdptr = new SqlDataAdapter(objCmd);
				sqlDataAdptr.Fill(ods);
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//ods = null;
				objCmd = null;
                sqlDataAdptr.Dispose();
				sqlDataAdptr = null;
                DBClose();
			}
            return ods;
		}
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3,
            string sOutParam4, string sOutParam5, string sOutParam6, string sOutParam7, string sOutParam8, out string sCode1, out string sCode2, out string sCode3, out bool sCode4, out bool sCode5, out int returnResult, out string sCode6, out string sCode7, out string sCode8)
        {
            returnResult = 0;
            sCode4 = false;
            sCode5 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            sCode3 = string.Empty;
            sCode6 = string.Empty;
            sCode7 = string.Empty;
            sCode8 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam4, SqlDbType.Bit);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;


                objCmd.Parameters.Add(sOutParam5, SqlDbType.Bit);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam6, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam7, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam7].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam8, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam8].Direction = ParameterDirection.Output;


                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToBoolean(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToBoolean(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToString(objCmd.Parameters[sOutParam6].Value);
                sCode7 = Convert.ToString(objCmd.Parameters[sOutParam7].Value);
                sCode8 = Convert.ToString(objCmd.Parameters[sOutParam8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }

        public DataTable GetTableRecords(string SQL,string strTableName)
		{
			sqlDataAdptr = new SqlDataAdapter(SQL, DBConnection());
			DataTable dt = new DataTable();
			try
			{
				dt.TableName = strTableName;
				sqlDataAdptr.Fill (dt);
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//objDS.Dispose();
				//dt.Dispose();
                sqlDataAdptr.Dispose();
				sqlDataAdptr = null;
                DBClose();
			}
            return dt;
			
		}
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out int sCode)
        {
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                
                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out string sMessage)
        {
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sMessage = Convert.ToString(objCmd.Parameters[sOutParam].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, out int sCode, out int sCode2, out string sCode3)
        {
            sCode = 0;
            sCode2 = 0;
            sCode3 = string.Empty;
            
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
               // objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
               // sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }

        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, string sOutParam4, out int sCode, out string sCode2, out string sCode3, out string sCode4)
        {
            sCode = 0;
            sCode2 = string.Empty;
            sCode3 = string.Empty;
            sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, string sOutParam3, out int sCode, out string sCode2, out string sCode3)
        {
            sCode = 0;
            sCode2 = string.Empty;
            sCode3 = string.Empty;
            //sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                //objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
                //objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                //sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, out int sCode, out string sCode2)
        {
            sCode = 0;
            sCode2 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, string sOutParam6, out string sCode1, out string sCode2, out string sCode3, out bool sCode4, out bool sCode5, out int returnResult, out string sCode6)
        {
            returnResult = 0;
            sCode4 = false;
            sCode5 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            sCode3 = string.Empty;
            sCode6 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam4, SqlDbType.Bit);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;


                objCmd.Parameters.Add(sOutParam5, SqlDbType.Bit);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam6, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam6].Direction = ParameterDirection.Output;


                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToBoolean(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToBoolean(objCmd.Parameters[sOutParam5].Value);
                sCode6 = Convert.ToString(objCmd.Parameters[sOutParam6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, string sOutParam4, string sOutParam5, out string sCode1, out string sCode2, out string sCode3, out bool sCode4, out bool sCode5, out int returnResult)
        {
            returnResult = 0;
            sCode4 = false;
            sCode5 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            sCode3 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam4, SqlDbType.Bit);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;

                
                objCmd.Parameters.Add(sOutParam5, SqlDbType.Bit);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;


                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToBoolean(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToBoolean(objCmd.Parameters[sOutParam5].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, out string sCode1, out string sCode2, out bool sCode3, out int returnResult)
        {
            returnResult = 0;
            sCode3 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            //sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                
                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.Bit);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                //objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
                //objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();
                

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                //sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode3 = Convert.ToBoolean(objCmd.Parameters[sOutParam3].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, out string sCode1, out string sCode2, out string sCode3)
        {
            //returnResult = 0;
            sCode3 = string.Empty; 
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            //sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;

                //objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
                //objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                //DbParameter returnValue;
                //returnValue = objCmd.CreateParameter();
                //returnValue.Direction = ParameterDirection.ReturnValue;
                //objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                //returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                //sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam3, string sOutParam4, out string sCode1, out string sCode2, out string sCode3, out string sCode4)
        {
            //returnResult = 0;
            sCode3 = string.Empty;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam3, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam3].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                //DbParameter returnValue;
                //returnValue = objCmd.CreateParameter();
                //returnValue.Direction = ParameterDirection.ReturnValue;
                //objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                //returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode3 = Convert.ToString(objCmd.Parameters[sOutParam3].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, string sOutParam2, out int sCode, out int sCode2)
        {
            sCode = 0;
            sCode2 = 0;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                objCmd.Parameters.Add(sOutParam, SqlDbType.Int);
                objCmd.Parameters[sOutParam].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.Int);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                sCode = Convert.ToInt32(objCmd.Parameters[sOutParam].Value);
                sCode2 = Convert.ToInt32(objCmd.Parameters[sOutParam2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }


        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, out string sCode1, out string sCode2, out int returnResult)
        {
            returnResult = 0;
            //sCode3 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            //sCode4 = string.Empty;
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 1000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;
                
                //objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 1000);
                //objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                //sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                //sCode3 = Convert.ToBoolean(objCmd.Parameters[sOutParam3].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam1, string sOutParam2, string sOutParam4, string sOutParam5, out string sCode1, out string sCode2, out string sCode4, out string sCode5, out int returnResult)
        {
            returnResult = 0;
            //sCode3 = false;
            sCode1 = string.Empty;
            sCode2 = string.Empty;
            sCode4 = string.Empty;
            sCode5 = string.Empty;

            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }

                objCmd.Parameters.Add(sOutParam1, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam1].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam2, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam2].Direction = ParameterDirection.Output;

                objCmd.Parameters.Add(sOutParam4, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam4].Direction = ParameterDirection.Output;
                objCmd.Parameters.Add(sOutParam5, SqlDbType.VarChar, 5000);
                objCmd.Parameters[sOutParam5].Direction = ParameterDirection.Output;

                DbParameter returnValue;
                returnValue = objCmd.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                objCmd.Parameters.Add(returnValue);

                //objCmd.ExecuteNonQuery();


                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);

                returnResult = Convert.ToInt32(returnValue.Value);
                sCode1 = Convert.ToString(objCmd.Parameters[sOutParam1].Value);
                sCode2 = Convert.ToString(objCmd.Parameters[sOutParam2].Value);
                sCode4 = Convert.ToString(objCmd.Parameters[sOutParam4].Value);
                sCode5 = Convert.ToString(objCmd.Parameters[sOutParam5].Value);
                //sCode3 = Convert.ToBoolean(objCmd.Parameters[sOutParam3].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        
		public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{
			DataTable odt = new DataTable();
			SqlCommand objCmd;	
			try
			{
				objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandTimeout = 0;
				objCmd.CommandType = CommandType.StoredProcedure;
				if (HshParameters !=null && arrFieldsSeq !=null)
					if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
					{
						foreach(string strField in arrFieldsSeq)
						{				
							//objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
						}				
					}
				sqlDataAdptr = new SqlDataAdapter(objCmd);
				sqlDataAdptr.Fill(odt);
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//odt = null;
				objCmd = null;
                sqlDataAdptr.Dispose();
				sqlDataAdptr = null;
                DBClose();
			}
            return odt;
		}
        public DataTable GetTableRecords2(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection2());
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr.Dispose();
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }
        public DataTable GetTableRecords3(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            DataTable odt = new DataTable();
            SqlCommand objCmd;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection3());
                objCmd.CommandType = CommandType.StoredProcedure;
                if (HshParameters != null && arrFieldsSeq != null)
                    if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                    {
                        foreach (string strField in arrFieldsSeq)
                        {
                            //objCmd.Parameters.Add(strField, HshParameters[strField]);
                            objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                        }
                    }
                sqlDataAdptr = new SqlDataAdapter(objCmd);
                sqlDataAdptr.Fill(odt);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //odt = null;
                objCmd = null;
                sqlDataAdptr = null;
                DBClose();
            }
            return odt;
        }

		public SqlDataReader  GetReaderRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{
			SqlCommand objCmd;
            SqlDataReader dataReader;
			try
			{
				objCmd = new SqlCommand(SQLString, DBConnection());
				objCmd.CommandType = CommandType.StoredProcedure;

				if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
				{
					foreach(string strField in arrFieldsSeq)
					{				
						//objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
					}				
				}
				dataReader = objCmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                //dataReader.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
                
				//dataReader = null;
				objCmd = null;
                DBClose();
			}
            return dataReader;
			
		}

        public XmlReader GetXmlReaderRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            SqlCommand objCmd;
            XmlReader dataReader;
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandType = CommandType.StoredProcedure;

                if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                {
                    foreach (string strField in arrFieldsSeq)
                    {
                        //objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                    }
                }
                dataReader = objCmd.ExecuteXmlReader();

                //dataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //dataReader = null;
                objCmd = null;
                DBClose();
            }
            return dataReader;
        }

        public DataSet GetDataSetRecord(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
        {
            SqlCommand objCmd;
            XmlReader dataReader;
            DataSet ds = new DataSet();
            try
            {
                objCmd = new SqlCommand(SQLString, DBConnection());
                objCmd.CommandType = CommandType.StoredProcedure;

                if (HshParameters.Count > 0 && arrFieldsSeq.Length > 0)
                {
                    foreach (string strField in arrFieldsSeq)
                    {
                        //objCmd.Parameters.Add(strField, HshParameters[strField]);
                        objCmd.Parameters.AddWithValue(strField, HshParameters[strField]);
                    }
                }
                dataReader = objCmd.ExecuteXmlReader();
                
                ds.ReadXmlSchema(dataReader);
                dataReader.Read();
                ds.ReadXml(dataReader);

                dataReader.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //dataReader.Close();
                //dataReader = null;
                objCmd = null;
                DBClose();
            }
            return ds;
        }


		public void DBClose()
		{
            if (Conn != null)
            {
                if (Conn.State == ConnectionState.Open || Conn.State == ConnectionState.Broken)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
		}
        public string serializeObjetToXMLString(object obj, string rootNodeName, string listName)
        {

            XmlSerializer objXMLSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memstr = new MemoryStream();
            XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);
            string sXML = "";
            try
            {

                objXMLSerializer.Serialize(xmltxtwr, obj);


                sXML = Encoding.UTF8.GetString(memstr.GetBuffer());
                sXML = "<" + rootNodeName + ">" + sXML.Substring(sXML.IndexOf("<" + listName + ">"));
                sXML = sXML.Substring(0, (sXML.LastIndexOf(Convert.ToChar(62)) + 1));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmltxtwr.Close();
                memstr.Close();
            }
            return sXML;
        }
	}
}
