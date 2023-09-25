using System;
using System.Data;
using System.Data.SqlClient;

namespace Sucarcane
{



    public class clsMasterData:clsMSSQL
    {

        public DataSet getDataddlVehicle_codeSucarcane()
        {
            DataSet ds;
            String strSQL = " SELECT distinct truckWH,"
                          + " min(RefNo)as RefNo "
                          + " FROM THA_INVENSHMS "
                          + " group by TruckWH "
                             ;
            ds = executeQuery(strSQL);
            return ds;
        }

      

        public DataSet getDataSucarcane(string strDate,string strVehicle_code,string strCropYear)
        {
            DataSet ds;
            String strSQL = " SELECT *"
                             + " FROM THA_INVENSHMS ";
                             //+ " WHERE TransDate ='" + strDate + "'";
                    if(strVehicle_code != "")

                    strSQL += " WHERE TruckWH ='" + strVehicle_code + "' ";
                    //if (strCropYear != "")
                    //strSQL += " AND Dimension like '%" + strCropYear + "%' ";
            
            ds = executeQuery(strSQL);
            return ds;
        }


        public string editSucarcaneData(DataSet ds)
        {
            string strRet = "";
            string strSQL = "";

          
            clsMasterUtil cmUtil = new clsMasterUtil();
            SqlConnection conn;
            SqlTransaction trans;

            conn = getDbConnection();
            trans = conn.BeginTransaction();
            try
            {
               
                    strSQL = "DELETE sugarcanetransoil  ";
                    //strSQL += " WHERE vehicle_code = '" + ds.Tables[0].Rows[0]["vehicle_code"].ToString() + "'";
                    executeUpdate(strSQL, conn, trans);
                    for (int i = 0; i <= ds.Tables["detail"].Rows.Count - 1; i++)
                    {
                     
                        strSQL = "INSERT INTO sugarcanetransoil(truckoil, vehicle_code, transdate, oilamoun, ";
                        strSQL += " uid, approvestatus, approver";
                        strSQL += " ) VALUES ( ";
                        strSQL += " '" + ds.Tables[0].Rows[0]["truckoil"].ToString() + "', ";
                        strSQL += " '" + ds.Tables[0].Rows[0]["vehicle_code"].ToString() + "', ";
                        strSQL += " '" + ds.Tables[0].Rows[0]["transdate"].ToString() + "', ";
                        strSQL += " '" + ds.Tables["detail"].Rows[i]["oilamoun"].ToString() + "', ";
                        strSQL += " '" + ds.Tables["detail"].Rows[i]["uid"].ToString() + "', ";
                        strSQL += " '" + ds.Tables["detail"].Rows[i]["approvestatus"].ToString() + "', ";
                        strSQL += " '" + ds.Tables[0].Rows[0]["approver"].ToString() + "' ";
                        strSQL += " ) ";
                        executeUpdate(strSQL, conn, trans);
                    }
               
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