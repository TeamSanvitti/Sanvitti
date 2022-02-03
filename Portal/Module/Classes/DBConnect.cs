using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
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
            if (System.Configuration.ConfigurationManager.AppSettings["DBServerName"] != null)
            {
                ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBServerName"].ToString();
            }
            else
            {
                throw new Exception("Connection string is not initialized");
            }
            if (System.Configuration.ConfigurationManager.AppSettings["DBServerName2"] != null)
            {
                ConnectionString2 = System.Configuration.ConfigurationManager.AppSettings["DBServerName2"].ToString();
            }
            else
            {
                throw new Exception("Connection string is not initialized");
            }
            if (System.Configuration.ConfigurationManager.AppSettings["DBServerName3"] != null)
            {
                ConnectionString3 = System.Configuration.ConfigurationManager.AppSettings["DBServerName3"].ToString();
            }
            else
            {
                throw new Exception("Connection string is not initialized");
            }
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
				objCmd.Parameters.Add(sOutParam,SqlDbType.VarChar,10);
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
        public int ExCommand(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq, string sOutParam, out string sCode)
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

		public DataSet GetDataSet(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{
			DataSet ods = new DataSet() ;
			SqlCommand objCmd;	
			try
			{
				objCmd = new SqlCommand(SQLString, DBConnection());
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

		public DataTable GetTableRecords(Hashtable HshParameters, string SQLString, string[] arrFieldsSeq)
		{
			DataTable odt = new DataTable();
			SqlCommand objCmd;	
			try
			{
				objCmd = new SqlCommand(SQLString, DBConnection());
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
