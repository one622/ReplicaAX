using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Sucarcane
{
    public class clsMasterUtil
    {
        public clsMasterUtil()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void setDataGrid(System.Web.UI.WebControls.DataGrid dataGrid, string[] ColumnWidth, string[] ColumnAlign)
        {
            UnitConverter unitConverter = new UnitConverter();
            for (int index = 0; index <= dataGrid.Columns.Count - 1; ++index)
            {
                dataGrid.Columns[index].HeaderStyle.Width = (Unit)unitConverter.ConvertFromString(ColumnWidth[index]);
                dataGrid.Columns[index].ItemStyle.HorizontalAlign = !(ColumnAlign[index].ToUpper() == "LEFT") ? (!(ColumnAlign[index].ToUpper() == "RIGHT") ? HorizontalAlign.Center : HorizontalAlign.Right) : HorizontalAlign.Left;
            }
        }

        public DataTable AddColumnToDataTable(
          DataTable srcDT,
          string[] ColumnDataType,
          string[] ColumnName)
        {
            for (int index = 0; index < ColumnDataType.Length; ++index)
            {
                switch (ColumnDataType[index].ToLower())
                {
                    case "string":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(string)));
                        break;
                    case "int":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(int)));
                        break;
                    case "float":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(float)));
                        break;
                    case "double":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(double)));
                        break;
                    case "datatable":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(DataTable)));
                        break;
                    case "bool":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(bool)));
                        break;
                    case "char":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(char)));
                        break;
                    case "byte":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(byte)));
                        break;
                    case "date":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(DateTime)));
                        break;
                    case "dataset":
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(DataSet)));
                        break;
                    default:
                        srcDT.Columns.Add(new DataColumn(ColumnName[index], typeof(string)));
                        break;
                }
            }
            return srcDT;
        }

        public DataSet MapFieldsDataTable(
          DataTable dtIN,
          string[] ColumnDataTypeIn,
          string[] ColumnNameIn,
          string[] ColumnDataTypeOut,
          string[] ColumnNameOut)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(dtIN.TableName);
            for (int index = 0; index < ColumnDataTypeOut.Length; ++index)
            {
                switch (ColumnDataTypeOut[index].ToLower())
                {
                    case "string":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(string)));
                        break;
                    case "int":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(int)));
                        break;
                    case "float":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(float)));
                        break;
                    case "double":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(double)));
                        break;
                    case "datatable":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(DataTable)));
                        break;
                    case "bool":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(bool)));
                        break;
                    case "char":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(char)));
                        break;
                    case "byte":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(byte)));
                        break;
                    case "date":
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(DateTime)));
                        break;
                    default:
                        dataTable.Columns.Add(new DataColumn(ColumnNameOut[index], typeof(string)));
                        break;
                }
            }
            dataTable.Rows.Add(dataTable.NewRow());
            for (int index = 0; index < ColumnNameIn.Length; ++index)
            {
                switch (ColumnDataTypeOut[index].ToLower())
                {
                    case "string":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)dtIN.Rows[0][ColumnNameIn[index]].ToString();
                        break;
                    case "int":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)int.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "float":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)double.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "double":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)double.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "datatable":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)(DataTable)dtIN.Rows[0][ColumnNameIn[index]];
                        break;
                    case "bool":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)bool.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "char":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)char.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "byte":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)byte.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    case "date":
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)DateTime.Parse(dtIN.Rows[0][ColumnNameIn[index]].ToString());
                        break;
                    default:
                        dataTable.Rows[0][ColumnNameOut[index]] = (object)dtIN.Rows[0][ColumnNameIn[index]].ToString();
                        break;
                }
            }
            dataSet.Tables.Add(dataTable.Copy());
            dtIN = (DataTable)null;
            return dataSet;
        }

        public DataTable AddRowToDataTable(DataTable srcDT, int numRows)
        {
            for (int index = 0; index < numRows; ++index)
            {
                DataRow row = srcDT.NewRow();
                srcDT.Rows.Add(row);
            }
            return srcDT;
        }

        public DataSet updateTableInDataSet(DataSet ds, DataTable dt, string tblName)
        {
            if (ds.Tables[tblName] != null)
                ds.Tables.Remove(tblName);
            ds.Tables.Add(dt);
            return ds;
        }

        public string writeDataTable(DataTable dt)
        {
            string str1 = "" + "<table border=1>" + "<tr>" + "<td>Row No.</td>";
            for (int index = 0; index < dt.Columns.Count; ++index)
                str1 = str1 + "<td>" + (object)dt.Columns[index] + "</td>";
            string str2 = str1 + "</tr>";
            for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
            {
                string str3 = str2 + "<tr>" + "<td>" + (object)(index1 + 1) + "</td>";
                for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
                    str3 = str3 + "<td>" + dt.Rows[index1][index2] + "</td>";
                str2 = str3 + "<tr>";
            }
            return str2 + "</table><br>";
        }

        public string writeDataSet(DataSet ds)
        {
            string str1 = "" + "DataSet Name : " + ds.DataSetName + "<br>\r\n" + "All Table : " + (object)ds.Tables.Count + "<br>\r\n";
            DataSet ds1 = new DataSet();
            for (int index1 = 0; index1 < ds.Tables.Count; ++index1)
            {
                string str2 = str1 + " Table Name : " + (object)ds.Tables[index1] + " Rows : " + (object)ds.Tables[index1].Rows.Count + "<br>\r\n" + "<table border=1><tr>\r\n";
                for (int index2 = 0; index2 < ds.Tables[index1].Columns.Count; ++index2)
                    str2 = str2 + "<td>" + (object)ds.Tables[index1].Columns[index2] + "</td>\r\n";
                string str3 = str2 + "</tr>\r\n";
                for (int index2 = 0; index2 < ds.Tables[index1].Rows.Count; ++index2)
                {
                    string str4 = str3 + "<tr>\r\n";
                    for (int index3 = 0; index3 < ds.Tables[index1].Columns.Count; ++index3)
                    {
                        if (ds.Tables[index1].Columns[index3].ColumnName.Equals("DETAILLOG"))
                        {
                            StringReader stringReader = new StringReader(ds.Tables[index1].Rows[index2][index3].ToString());
                            int num = (int)ds1.ReadXml((TextReader)stringReader, XmlReadMode.InferSchema);
                            str4 = str4 + "<td>" + writeDataSet(ds1) + "</td>\r\n";
                        }
                        else
                            str4 = str4 + "<td>" + ds.Tables[index1].Rows[index2][index3] + "</td>\r\n";
                    }
                    str3 = str4 + "<tr>\r\n";
                }
                str1 = str3 + "</table><br>\r\n";
            }
            return str1;
        }

        public string getDateString()
        {
            return getDateString(DateTime.Now, "d/M/yyyy", "en-US");
        }

        public string getDateString(DateTime dt)
        {
            return getDateString(dt, "d/M/yyyy", "en-US");
        }

        public string getDateString(DateTime dt, string format)
        {
            return getDateString(dt, format, "en-US");
        }

        public string getDateString(DateTime dt, string format, string culture)
        {
            IFormatProvider provider = (IFormatProvider)new CultureInfo(culture);
            return dt.ToString(format, provider);
        }

       

       

        public string replaceSpecialSQL(string strData)
        {
            strData = strData.Replace("'", "''");
            return strData;
        }
    }
}