using ClosedXML.Excel;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Utility
{
    public class DataTableUtility
    {
        public static bool IsContainColumn(string columnName, DataTable dataTable)
        {
            DataColumnCollection columns = dataTable.Columns;
            if (columns.Contains(columnName)) return true;
            return false;
        }
        public static DataTable RemoveColumnsInDataTable(string[] listColumnRemove, DataTable dataTable)
        {
            for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
            {
                DataColumn dataColumn = dataTable.Columns[i];
                if (listColumnRemove.Contains(dataColumn.ColumnName))
                {
                    dataTable.Columns.Remove(dataColumn);
                }
            }

            return dataTable;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            string a = pro.PropertyType.Name;
                            if (pro.PropertyType == typeof(DateTime?))
                            {
                                DateTime oDate = DateTime.Parse(dr[column.ColumnName].ToString());
                                pro.SetValue(obj, oDate, null);
                            }
                            if (pro.PropertyType == typeof(long?))
                            {
                                var value = long.Parse(dr[column.ColumnName].ToString());
                                pro.SetValue(obj, value, null);
                            }
                            if (pro.PropertyType == typeof(bool?) || pro.PropertyType.Name == "Boolean")
                            {
                                if (dr[column.ColumnName].ToString() == "1")
                                {
                                    pro.SetValue(obj, true, null);
                                }
                                else
                                {
                                    pro.SetValue(obj, false, null);
                                }
                            }
                            if (pro.PropertyType == typeof(string))
                            {
                                var str = dr[column.ColumnName].ToString();
                                var start = str.Substring(0, 1);
                                var end = str.Substring(str.Length - 1, 1);
                                if (start.Contains('\"') && end.Contains('\"'))
                                {
                                    pro.SetValue(obj, str.Substring(1, str.Length - 2), null);
                                }
                                else
                                {
                                    pro.SetValue(obj, str, null);
                                }
                            }
                            if (pro.PropertyType == typeof(decimal?))
                            {
                                pro.SetValue(obj, decimal.Parse(dr[column.ColumnName]?.ToString(), NumberStyles.AllowThousands), null);
                            }
                            if (pro.PropertyType == typeof(decimal))
                            {
                                pro.SetValue(obj, decimal.Parse(dr[column.ColumnName]?.ToString(), NumberStyles.AllowThousands), null);
                            }
                            if (pro.PropertyType == typeof(long))
                            {
                                pro.SetValue(obj, long.Parse(dr[column.ColumnName]?.ToString()), null);
                            }
                            //if (pro.Name == "CompanyShareds")
                            //{
                            //    pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]), null);
                            //}
                        }
                        catch (Exception)
                        {
                            Log.Fatal("Error : Name - " + pro.Name + " DataType:" + pro.PropertyType.Name + " -value- " + dr[column.ColumnName]);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        public static List<dynamic> DataTableToList(DataTable dt)
        {
            var listaData = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                listaData.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dictionary = (IDictionary<string, object>)dyn;
                    if (row[column] is DBNull)
                    {
                        dictionary[column.ColumnName] = default;
                    }
                    else
                    {
                        dictionary[column.ColumnName] = row[column];
                    }

                }
            }
            return listaData;
        }

        public static DataTable ListObjectToDataTable<T>(IEnumerable<T> data, string tableName)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            table.TableName = tableName;
            return table;
        }
        public static DataTable RemoveColumnFieldAndValuesOfColumn(DataTable dataTable, string tableFileNameRemove)
        {
            dataTable.Columns.Remove(tableFileNameRemove);
            return dataTable;
        }

        public static DataTable ConvertCsVtoDataTable(string strFilePath)
        {
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                DataTable dt = new DataTable();
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        public static DataTable ConvertExeLtoDataTable(string strFilePath)
        {

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(strFilePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
                return dt;
            }
        }
    }
}
