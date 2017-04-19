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
        public static string ExportCsv(string path, string fileName, string content)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName += ".csv";
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
        public static DataSet ImportXlsToDataSet(string fileName)
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
}
