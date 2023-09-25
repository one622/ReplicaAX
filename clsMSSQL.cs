using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Sucarcane
{
    public class clsMSSQL
    {
        protected SqlConnection oDbConn;
        protected SqlCommand oDbComm;
        protected SqlDataAdapter oDbDap;
        protected SqlTransaction oDbTransaction;
        protected string strConnection;
        protected string ConnectionString;

        public clsMSSQL()
        {
            //
            // TODO: Add constructor logic here
            //
            this.ConnectionString = ConfigurationSettings.AppSettings["ConStr"];
            // this.ConnectionString = string.Empty;
        }
        protected SqlConnection getDbConnection()
        {
            this.oDbConn = new SqlConnection();
            //try
            //{
            //    if (this.ConnectionString == string.Empty || this.ConnectionString == "")
                  
            //        oDbConn.ConnectionString = "Source=CTL-G4G41T3\\SQLEXPRESS;Initial Catalog=SugarcaneDB;User ID=sa;Password=123456789";
            //    else
            //        this.oDbConn.ConnectionString = "Source=CTL-G4G41T3\\SQLEXPRESS;Initial Catalog=SugarcaneDB;User ID=sa;Password=123456789";
            //}
            //catch
            //{
               
                this.oDbConn.ConnectionString = ConfigurationSettings.AppSettings["ConStr"] ;
            //}
            this.oDbConn.Open();
            return this.oDbConn;
        }
        protected void closeDbConnection()
        {
            this.oDbConn.Close();
        }
        protected DataSet executeQuery(string cmdString)
        {
            DataSet dataSet = new DataSet();
            try
            {
                this.oDbComm = new SqlCommand();
                this.oDbComm.Connection = this.getDbConnection();
                this.oDbComm.CommandText = cmdString;
                this.oDbComm.CommandTimeout = 0;
                this.oDbDap = new SqlDataAdapter();
                this.oDbDap.SelectCommand = this.oDbComm;
                this.oDbDap.Fill(dataSet, "Table");
                this.oDbDap.Dispose();
                this.oDbComm.Dispose();
                this.closeDbConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.oDbDap.Dispose();
                this.oDbComm.Dispose();
                if (this.oDbConn != null)
                    this.closeDbConnection();
            }
            return dataSet;
        }

        protected void executeUpdate(string cmdString)
        {
            try
            {
                this.oDbComm = new SqlCommand();
                this.oDbComm.Connection = this.getDbConnection();
                this.oDbComm.CommandText = cmdString;
                this.oDbComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.oDbComm.Dispose();
                if (this.oDbConn != null)
                    this.closeDbConnection();
            }
        }
        protected void BeginTransaction(SqlConnection oDbCon)
        {
            this.oDbTransaction = oDbCon.BeginTransaction();
        }
        protected void CommitTransaction(SqlConnection oDbCon)
        {
            this.oDbTransaction.Commit();
        }

        protected void RollbackTransaction(SqlConnection oDbCon)
        {
            this.oDbTransaction.Rollback();
        }

        protected void closeDbConnection(SqlConnection oDbCon)
        {
            oDbCon.Dispose();
            oDbCon.Close();
        }

        protected DataSet executeQuery(string cmdString, SqlConnection Cnn)
        {
            DataSet dataSet = new DataSet();
            try
            {
                this.oDbComm = new SqlCommand();
                this.oDbComm.Connection = Cnn;
                this.oDbComm.CommandText = cmdString;
                this.oDbComm.CommandTimeout = 0;
                this.oDbDap = new SqlDataAdapter();
                this.oDbDap.SelectCommand = this.oDbComm;
                this.oDbDap.Fill(dataSet, "Table");
                this.oDbDap.Dispose();
                this.oDbComm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }
            return dataSet;
        }

        protected DataSet executeQuery(
          string cmdString,
          SqlConnection Cnn,
          SqlTransaction cnnTransaction)
        {
            DataSet dataSet = new DataSet();
            try
            {
                this.oDbComm = new SqlCommand();
                this.oDbComm.Connection = Cnn;
                this.oDbComm.Transaction = cnnTransaction;
                this.oDbComm.CommandText = cmdString;
                this.oDbComm.CommandTimeout = 0;
                this.oDbDap = new SqlDataAdapter();
                this.oDbDap.SelectCommand = this.oDbComm;
                this.oDbDap.Fill(dataSet, "Table");
                this.oDbDap.Dispose();
                this.oDbComm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
            }
            return dataSet;
        }

        protected void executeUpdate(
          string cmdString,
          SqlConnection Cnn,
          SqlTransaction cnnTransaction)
        {
            try
            {
                this.oDbComm = new SqlCommand();
                this.oDbComm.Connection = Cnn;
                this.oDbComm.Transaction = cnnTransaction;
                this.oDbComm.CommandText = cmdString;
                this.oDbComm.ExecuteNonQuery();
                this.oDbComm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsTableExist(string tableName)
        {
            return this.executeQuery("SELECT name FROM sysobjects WHERE xtype = 'U' AND name = '" + tableName + "'").Tables[0].Rows.Count > 0;
        }

        protected string getMaxID(string columnName, string tableName)
        {
            return this.executeQuery("SELECT ISNULL(MAX(" + columnName + "),0)+1 MAX_ID FROM " + tableName + " ").Tables[0].Rows[0]["MAX_ID"].ToString();
        }

        protected string getMaxID(
          string columnName,
          string tableName,
          SqlConnection Cnn,
          SqlTransaction cnnTransaction)
        {
            return this.executeQuery("SELECT ISNULL(MAX(" + columnName + "),0)+1 MAX_ID FROM " + tableName + " ", Cnn, cnnTransaction).Tables[0].Rows[0]["MAX_ID"].ToString();
        }

        protected string getMaxID(
          string columnName,
          string tableName,
          string prefix,
          int numberOfDigit,
          bool haveYear,
          bool haveMonth)
        {
            string str1 = "";
            string str2 = "";
            string format = "";
            for (int index = 0; index < numberOfDigit; ++index)
                format += "0";
            int num1 = numberOfDigit;
            string cmdString1 = "";
            string str3 = "";
            if (haveYear)
            {
                num1 += 2;
                if (haveMonth)
                {
                    num1 += 2;
                    cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE()) AS varchar), 2)+ CASE LEN(CAST(MONTH(GETDATE()) AS varchar)) WHEN 1 THEN '0'+CAST(MONTH(GETDATE()) AS varchar) WHEN 2 THEN CAST(MONTH(GETDATE()) AS varchar) END ";
                    try
                    {
                        //str3 = ConfigurationSettings.AppSettings["yearCode"].ToString();
                    }
                    catch
                    {
                    }
                    if (str3 == "BB")
                        cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE())+543 AS varchar), 2)+ CASE LEN(CAST(MONTH(GETDATE()) AS varchar)) WHEN 1 THEN '0'+CAST(MONTH(GETDATE()) AS varchar) WHEN 2 THEN CAST(MONTH(GETDATE()) AS varchar) END ";
                }
                else
                {
                    cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE()) AS varchar), 2) ";
                    try
                    {
                        //str3 = ConfigurationSettings.AppSettings["yearCode"].ToString();
                    }
                    catch
                    {
                    }
                    if (str3 == "BB")
                        cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE())+543 AS varchar), 2) ";
                }
            }
            int num2 = num1 + prefix.Length;
            string cmdString2 = "SELECT MAX(" + columnName + ") FROM " + tableName + " WHERE " + columnName + " LIKE '" + prefix + "%' AND LEN(" + columnName + ") = " + num2.ToString() + "";
            try
            {
                DataSet dataSet1 = new DataSet();
                if (haveYear)
                {
                    DataSet dataSet2 = this.executeQuery(cmdString1);
                    str2 = (string)dataSet2.Tables[0].Rows[0][0];
                    dataSet2.Clear();
                }
                string str4 = prefix + str2;
                int length = str4.Length;
                string str5 = this.executeQuery(cmdString2).Tables[0].Rows[0][0].ToString();
                if (str5.IndexOf("/") > 0)
                    str5 = str5.Substring(0, str5.IndexOf("/"));
                if (str5.Equals(""))
                    str1 = str4 + 1.ToString(format);
                else if (str5.Substring(0, length).Equals(str4))
                {
                    int num3 = Convert.ToInt32(str5.Substring(length)) + 1;
                    str1 = str4 + num3.ToString(format);
                }
                else
                    str1 = str4 + 1.ToString(format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                if (this.oDbConn != null)
                    this.closeDbConnection();
            }
            return str1;
        }

        protected string getMaxID(
          string columnName,
          string tableName,
          string prefix,
          int numberOfDigit,
          bool haveYear,
          bool haveMonth,
          SqlConnection Cnn,
          SqlTransaction cnnTransaction)
        {
            string str1 = "";
            string str2 = "";
            string format = "";
            for (int index = 0; index < numberOfDigit; ++index)
                format += "0";
            string cmdString1 = "";
            string str3 = "";
            if (haveYear)
            {
                if (haveMonth)
                {
                    cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE()) AS varchar), 2)+ CASE LEN(CAST(MONTH(GETDATE()) AS varchar)) WHEN 1 THEN '0'+CAST(MONTH(GETDATE()) AS varchar) WHEN 2 THEN CAST(MONTH(GETDATE()) AS varchar) END ";
                    try
                    {
                        //str3 = ConfigurationSettings.AppSettings["yearCode"].ToString();
                    }
                    catch
                    {
                    }
                    if (str3 == "BB")
                        cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE())+543 AS varchar), 2)+ CASE LEN(CAST(MONTH(GETDATE()) AS varchar)) WHEN 1 THEN '0'+CAST(MONTH(GETDATE()) AS varchar) WHEN 2 THEN CAST(MONTH(GETDATE()) AS varchar) END ";
                }
                else
                {
                    cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE()) AS varchar), 2) ";
                    try
                    {
                        //str3 = ConfigurationSettings.AppSettings["yearCode"].ToString();
                    }
                    catch
                    {
                    }
                    if (str3 == "BB")
                        cmdString1 = "SELECT RIGHT(CAST(YEAR(GETDATE())+543 AS varchar), 2) ";
                }
            }
            string cmdString2 = "SELECT MAX(" + columnName + ") FROM " + tableName + " WHERE " + columnName + " LIKE '" + prefix + "%' ";
            try
            {
                DataSet dataSet1 = new DataSet();
                if (haveYear)
                {
                    DataSet dataSet2 = this.executeQuery(cmdString1, Cnn, cnnTransaction);
                    str2 = (string)dataSet2.Tables[0].Rows[0][0];
                    dataSet2.Clear();
                }
                string str4 = prefix + str2;
                int length = str4.Length;
                string str5 = this.executeQuery(cmdString2, Cnn, cnnTransaction).Tables[0].Rows[0][0].ToString();
                if (str5.IndexOf("/") > 0)
                    str5 = str5.Substring(0, str5.IndexOf("/"));
                if (str5.Equals(""))
                    str1 = str4 + 1.ToString(format);
                else if (str5.Substring(0, length).Equals(str4))
                {
                    int num = Convert.ToInt32(str5.Substring(length)) + 1;
                    str1 = str4 + num.ToString(format);
                }
                else
                    str1 = str4 + 1.ToString(format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MasterUtil.writeLog("TDSLib :: BLWorker_MSSQL", ex.ToString());
            }
            finally
            {
            }
            return str1;
        }

      

     


       
    }
}