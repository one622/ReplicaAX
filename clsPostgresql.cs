using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ReplicaAX
{
    public class clsPostgresql
    {
        DataSet myDataSet = new DataSet();
        protected SqlConnection oDbConn;
        protected SqlCommand oDbComm;
        protected SqlDataAdapter oDbDap;
        protected SqlTransaction oDbTransaction;
        protected string strConnection;
        protected string ConnectionString;

        public String strFarmerTestDBConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["Cristalla_BI_Test_ConnectionString"].ConnectionString;
            }
        }
        public String strFarmerProDBConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["Cristalla_AXBI_ConnectionString"].ConnectionString;
            }
        }


        public DataTable SelectDataChangeConnectionString(String strSQLCommand, String strChangeConnectionString)
        {
            myDataSet.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(strChangeConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(strSQLCommand, sqlConnection))
                {
                    SqlDataAdapter myAdapter = new SqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    myAdapter.Fill(myDataSet, "tb");
                    sqlConnection.Close();
                    return myDataSet.Tables["Tb"];
                }
            }
        }


        public string strConn
        {
            get
            {
                // return ConfigurationManager.ConnectionStrings["Cristalla_BI_Test_ConnectionString"].ConnectionString;
                return ConfigurationManager.ConnectionStrings["Cristalla_AXBI_ConnectionString"].ConnectionString;
            }
        }


        public String ActionSQL(String SQLCMD)
        {
            using (SqlConnection connectionString = new SqlConnection(strConn))
            {
                connectionString.Open();
                using (SqlCommand myCommand = new SqlCommand(SQLCMD, connectionString))
                {
                    int actionResult = myCommand.ExecuteNonQuery();

                    return actionResult > 0 ? "Complete" : "Incomplete";
                }
            }
        }


        public String strPostgreSQLConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["PostgreSQLConnectiondb"].ConnectionString;
            }
        }


        public String ActionPostgreSQL(String strSQLCommand, String strPostgreSQLConnectionString)
        {
            using (NpgsqlConnection connectionString = new NpgsqlConnection(strPostgreSQLConnectionString))
            {
                connectionString.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(strSQLCommand, connectionString))
                {
                    int actionResult = myCommand.ExecuteNonQuery();
                    String strActionResult = "";
                    if (actionResult == 1)
                    {
                        strActionResult = "Complete";
                    }
                    else
                    {
                        strActionResult = "Incomplete";
                    }
                    return strActionResult;
                }
            }
        }


        public DataTable SelectDataFromPostgreSQL(String strSQLCommand, String strPostgreSQLConnectionString)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection connectionString = new NpgsqlConnection("Server=10.4.89.188;Port=5432;User Id=postgres;Password= Crist@ll@prog!@#;Database=sugarcanedb"))
            {
                connectionString.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(strSQLCommand, connectionString))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(myCommand))
                    {
                        da.Fill(dt);
                        connectionString.Close();
                        return dt;
                    }
                }
            }
        }


        public string getMaxNo(string strMaxColumnName, string strTableName, string locationname, string cropyear)
        {
            string strRet = "0";
            string strSQL = "";

            strSQL = "SELECT isnull(max(CAST(" + strMaxColumnName + " AS Int)),0) + 1 as maxno ";
            strSQL += " FROM " + strTableName + " ";
            strSQL += " WHERE my_location_name = '" + locationname + "' ";
            strSQL += " AND cropyear = '" + cropyear + "' ";
            DataTable dtList = SelectDataChangeConnectionString(strSQL, strFarmerProDBConnection);
            if (dtList.Rows.Count > 0 && dtList.Rows[0]["maxno"].ToString() != "")
            {
                strRet = dtList.Rows[0]["maxno"].ToString();
            }
            return strRet;
        }


        public string getNapierMaxNo(string strMaxColumnName, string strTableName, string locationname)
        {
            string strRet = "0";
            string strSQL = "";

            strSQL = "SELECT isnull(max(CAST(" + strMaxColumnName + " AS Int)),0) + 1 as maxno ";
            strSQL += " FROM " + strTableName + " ";
            strSQL += " WHERE my_location_name = '" + locationname + "' ";
            DataTable dtList = SelectDataChangeConnectionString(strSQL, strFarmerProDBConnection);
            if (dtList.Rows.Count > 0 && dtList.Rows[0]["maxno"].ToString() != "")
            {
                strRet = dtList.Rows[0]["maxno"].ToString();
            }
            return strRet;
        }


        public string getMaxId(string columnName, string tableName, string locationname)
        {
            string str1 = "";
            string format = "";
            string cmdString = "";

            cmdString = "SELECT MAX(" + columnName + ") as fer_no FROM " + tableName + " WHERE my_location_name LIKE '" + locationname + "%'  ";

            DataTable dtList = SelectDataChangeConnectionString(cmdString, strFarmerProDBConnection);
            if (dtList.Rows.Count > 0 && dtList.Rows[0]["fer_no"].ToString() != "")
            {
                int num = Convert.ToInt32(dtList.Rows[0]["fer_no"]);
                str1 = num.ToString(format);
            }
            else
            {
                str1 = 0.ToString(format);
            }
            return str1;
        }


        protected SqlConnection getDbConnection()
        {
            this.oDbConn = new SqlConnection();
            try
            {
                oDbConn.ConnectionString = strConn;
            }
            catch
            {

            }
            this.oDbConn.Open();
            return this.oDbConn;
        }


        protected void closeDbConnection()
        {
            this.oDbConn.Close();
        }


        protected void executeUpdate(string cmdString, SqlConnection Cnn, SqlTransaction cnnTransaction)
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


        public string addData(String strSQL)
        {
            string strRet = "";

            SqlConnection conn;
            SqlTransaction trans;

            conn = getDbConnection();
            trans = conn.BeginTransaction();
            try
            {

                executeUpdate(strSQL, conn, trans);
                trans.Commit();

            }
            catch (Exception e)
            {
                strRet = "ERROR";
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return strRet;
        }



    }

}
