﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JasonLib.Web;

namespace JasonLib.Data
{
    /// <summary>
    /// 数据集
    /// </summary>
    public static class JlDataSet
    {
        /// <summary>
        /// 通过Xml产生数据集
        /// </summary>
        /// <param name="xml">文档</param>
        /// <returns>结果</returns>
        public static DataSet FromXml(string xml)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xml);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                return dataSet;
            }
            return null;
        }

        /// <summary>
        /// 下载Xml产生数据集
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        public static DataSet DownloadXml(string url, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            var xml = JlRequest.GetHtml(url, encoding);

            return FromXml(xml);
        }
    }
}
