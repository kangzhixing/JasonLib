using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace JasonLib
{
    public class JlExcel
    {
        /// <summary>
        /// 导出到CVS文件，如已存在，则覆盖
        /// </summary>
        ///// <param name="path">导出地址</param>
        ///// <param name="fileName">文件名</param>
        ///// <param name="content">每一行用'\n\t'分隔，每一列用','分隔</param>
        ///// <returns>返回地址</returns>
        public static string ExportCvs(string path, string fileName, string content, string format = "csv")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName += "." + format;
            var url = path + fileName;
            if (File.Exists(url))
            {
                File.Delete(url);
            }

            //生成文件在服务器端
            var fs = new FileStream(url, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            sw.WriteLine(content);
            sw.Close();
            return url;
        }

        /// <summary>
        /// 将Excel内容存入DataSet中
        /// </summary>
        /// <param name="fileName">文件地址</param>
        /// <returns></returns>
        public static IWorkbook ExportToWorkbook(DataSet ds, JlWorkbookType wbType = JlWorkbookType.HSSF)
        {
            IWorkbook wb;
            switch (wbType)
            {
                case JlWorkbookType.XSSF: wb = new XSSFWorkbook(); break;
                case JlWorkbookType.HSSF: wb = new HSSFWorkbook(); break;
                default: wb = new HSSFWorkbook(); break;
            }

            var sheetsNum = wb.NumberOfSheets;
            var sheet1 = wb.CreateSheet("sheet1");

            var rowHeader = sheet1.CreateRow(0);

            for (var i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                var cell = rowHeader.CreateCell(i);
                cell.SetCellType(CellType.String);
                cell.SetCellValue(ds.Tables[0].Columns[i].ToString());
            }

            for (var j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                var row = sheet1.CreateRow(j + 1);
                for (var k = 0; k < ds.Tables[0].Columns.Count; k++)
                {
                    var cell = row.CreateCell(k);
                    switch (ds.Tables[0].Columns[k].DataType.Name.ToLower())
                    {
                        case "string":
                            cell.SetCellType(CellType.String);
                            cell.SetCellValue(ds.Tables[0].Rows[j][k].ToString());
                            break;
                        case "bool":
                            cell.SetCellType(CellType.Boolean);
                            cell.SetCellValue(JlConvert.TryToBool(ds.Tables[0].Rows[j][k]));
                            break;
                        case "int32":
                        case "decimal":
                            cell.SetCellType(CellType.Numeric);
                            cell.SetCellValue(JlConvert.TryToDouble(ds.Tables[0].Rows[j][k]));
                            break;
                        default:
                            cell.SetCellType(CellType.String);
                            cell.SetCellValue(ds.Tables[0].Rows[j][k].ToString());
                            break;
                    }
                }
            }

            return wb;
        }

        /// <summary>
        /// 将Excel内容存入DataSet中(需引用NPOI相关DLL)
        /// </summary>
        /// <param name="fileName">文件地址</param>
        /// <returns></returns>
        public static DataSet ImportXlsToDataSet(string fileName)
        {
            var dataSet = new DataSet();

            IWorkbook wb;

            try
            {
                wb = new XSSFWorkbook(new FileStream(fileName, FileMode.Open));
            }
            catch
            {
                wb = new HSSFWorkbook(new FileStream(fileName, FileMode.Open));
            }
            var sheetsNum = wb.NumberOfSheets;

            for (var i = 0; i < sheetsNum; i++)
            {
                var dataTable = new DataTable();
                var sht = wb.GetSheetAt(i);
                dataTable.TableName = sht.SheetName;
                var enu = sht.GetRowEnumerator();

                //获取最大列数
                var maxCellNum = 0;
                while (enu.MoveNext())
                {
                    var currentRow = (XSSFRow)enu.Current;
                    if (maxCellNum < currentRow.LastCellNum)
                    {
                        maxCellNum = currentRow.LastCellNum;
                    }
                }
                enu.Reset();
                // 设置datatable字段
                for (int j = 0, len = sht.GetRow(0).LastCellNum; j < maxCellNum; j++)
                {
                    if (j >= sht.GetRow(0).FirstCellNum && j < sht.GetRow(0).LastCellNum)
                    {
                        dataTable.Columns.Add(sht.GetRow(0).Cells[j].StringCellValue);
                    }
                    else
                    {
                        dataTable.Columns.Add(string.Empty);
                    }
                }
                while (enu.MoveNext())
                {
                    var currentRow = (XSSFRow)enu.Current;
                    var dataRow = dataTable.NewRow();
                    for (var k = currentRow.FirstCellNum; k < currentRow.LastCellNum; k++)
                    {
                        var cell = currentRow.GetCell(k);

                        switch (cell.CellType)
                        {
                            case CellType.Numeric: dataRow[k] = cell.NumericCellValue; break;
                            case CellType.Boolean: dataRow[k] = cell.BooleanCellValue; break;
                            case CellType.String: dataRow[k] = cell.StringCellValue; break;
                            case CellType.Blank: dataRow[k] = string.Empty; break;
                            default:
                                dataRow[k] = null; break;
                        }

                    }
                    dataTable.Rows.Add(dataRow);
                }
                dataSet.Tables.Add(dataTable);
            }

            return dataSet;

        }

        /// <summary>
        /// 将Excel内容存入DataSet中(OFFICE)
        /// </summary>
        /// <param name="fileName">文件地址</param>
        /// <returns></returns>
        public static DataSet ImportXls2DataSet(string fileName)
        {
            if (fileName == string.Empty)
            {
                throw new ArgumentNullException("Excel文件上传失败！");
            }

            //解析Excel文件存入DataSet
            var oleDbConnString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            oleDbConnString += "Data Source=";
            oleDbConnString += fileName;
            oleDbConnString += ";Extended Properties='Excel 12.0;IMEX=1;'";
            var oleDbConn = new OleDbConnection(oleDbConnString);
            oleDbConn.Open();
            OleDbDataAdapter oleAdMaster = null;

            var mTableName = oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (mTableName != null && mTableName.Rows.Count > 0)
            {

                mTableName.TableName = mTableName.Rows[0]["TABLE_NAME"].ToString();

            }
            var sqlMaster = " SELECT *  FROM [" + mTableName.TableName + "]";
            oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDbConn);
            var ds = new DataSet();
            oleAdMaster.Fill(ds, "m_tableName");
            oleAdMaster.Dispose();
            oleDbConn.Close();
            oleDbConn.Dispose();

            return ds;
        }
    }

    public enum JlWorkbookType
    {
        /// <summary>
        /// office2007
        /// </summary>
        XSSF,

        /// <summary>
        /// office2003
        /// </summary>
        HSSF
    }
}
