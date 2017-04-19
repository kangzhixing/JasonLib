using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace JasonLib.Web
{

    /// <summary>
    /// 请求
    /// </summary>
    public static class JlRequest
    {
        /// <summary>
        /// 用户IP
        /// </summary>
        public static string UserHostAddress
        {
            get
            {
                HttpContext current = HttpContext.Current;
                string ip = "";
                if (current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    ip = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Trim();
                }
                if (ip.Length == 0)
                {
                    ip = current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip.IndexOf(",") > -1)
                {
                    string[] array = ip.Split(new char[] { ',' });
                    ip = array[array.Length - 1].Trim();
                }
                try
                {
                    IPAddress.Parse(ip);
                }
                catch
                {
                    ip = "0.0.0.0";
                }
                return ip;
            }
        }

        /// <summary>
        /// 获取主机端口
        /// </summary>
        public static string UserHostPort
        {
            get
            {
                string port = HttpContext.Current.Request.ServerVariables["HTTP_REMOTE_PORT"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_PORT"];
                return string.IsNullOrEmpty(port) ? string.Empty : port;
            }
        }

        /// <summary>
        /// 获取url里面的html
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="timeout"></param>
        /// <param name="httpActionType"></param>
        /// <param name="postParameter"></param>
        /// <returns></returns>
        public static string GetHtml(string url, Encoding encoding = null, int timeout = 1000, JlHttpActionType httpActionType = JlHttpActionType.Get, string postParameter = "")
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            var request = HttpWebRequest.Create(url);
            request.Proxy = null;
            request.Timeout = timeout;
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            if (httpActionType == JlHttpActionType.Post)
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                if (!string.IsNullOrEmpty(postParameter))
                {
                    var postData = encoding.GetBytes(postParameter);

                    request.ContentLength = postData.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postData, 0, postData.Length);
                    }
                }
            }
            else
            {
                request.Method = "GET";
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (var streamReader = new StreamReader(stream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (var streamReader = new StreamReader(stream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
                else
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取url里面的html
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="timeout"></param>
        /// <param name="httpActionType"></param>
        /// <param name="postParameter"></param>
        /// <returns></returns>
        public static string GetHtmlNoShakeHand(string url, Encoding encoding = null, int timeout = 1000, JlHttpActionType httpActionType = JlHttpActionType.Get, string postParameter = "", bool isHandShaking = true)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            if (!isHandShaking)
            {
                System.Net.ServicePointManager.Expect100Continue = false;
            }
            var request = HttpWebRequest.Create(url);
            request.Proxy = null;
            request.Timeout = timeout;

            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            if (httpActionType == JlHttpActionType.Post)
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                if (!string.IsNullOrEmpty(postParameter))
                {
                    var postData = encoding.GetBytes(postParameter);

                    request.ContentLength = postData.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postData, 0, postData.Length);
                    }
                }
            }
            else
            {
                request.Method = "GET";
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (var streamReader = new StreamReader(stream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (var streamReader = new StreamReader(stream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
                else
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, encoding))
                        {
                            var html = streamReader.ReadToEnd();
                            return html;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取POST方法请求过来的数据
        /// </summary>
        /// <returns>参数字典</returns>
        public static SortedDictionary<string, string> FormatRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> array = new SortedDictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                array.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }
            return array;
        }

        /// <summary>
        /// 获取GET方法的请求过来的数据.
        /// </summary>
        /// <returns>排序字典</returns>
        public static SortedDictionary<string, string> FormatRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> array = new SortedDictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.QueryString;
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                array.Add(requestItem[i], HttpContext.Current.Request.QueryString[requestItem[i]]);
            }

            return array;
        }

        /// <summary>
        /// 获取以GET或POST 请求的数据
        /// </summary>
        /// <returns>排序字典</returns>
        public static SortedDictionary<string, string> FormatRequest()
        {
            int i = 0;
            var sArray = new SortedDictionary<string, string>();
            NameValueCollection collection;
            var method = HttpContext.Current.Request.HttpMethod.ToUpper();
            if (method == "POST")
                collection = HttpContext.Current.Request.Form;
            else
                collection = HttpContext.Current.Request.QueryString;

            String[] requestItem = collection.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                if (method == "POST")
                    sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
                else
                    sArray.Add(requestItem[i], HttpContext.Current.Request.QueryString[requestItem[i]]);
            }
            return sArray;
        }

        /// <summary>
        /// 一网通获取以GET或POST 请求的数据
        /// </summary>
        /// <returns>字典</returns>
        public static Dictionary<string, string> CFormatRequest()
        {
            int i = 0;
            var sArray = new Dictionary<string, string>();
            NameValueCollection collection;
            var method = HttpContext.Current.Request.HttpMethod.ToUpper();
            if (method == "POST")
                collection = HttpContext.Current.Request.Form;
            else
                collection = HttpContext.Current.Request.QueryString;

            String[] requestItem = collection.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                if (method == "POST")
                    sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
                else
                    sArray.Add(requestItem[i], HttpContext.Current.Request.QueryString[requestItem[i]]);
            }
            return sArray;
        }

        /// <summary>
        /// 获取POST方法请求过来的数据（中国银联）
        /// </summary>
        /// <returns>参数字典</returns>
        public static Dictionary<string, string> UnionFormatRequestPost()
        {
            int i = 0;
            Dictionary<string, string> array = new Dictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                array.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }
            return array;
        }

        /// <summary>
        /// 参数筛选
        /// </summary>
        /// <returns>参数字典</returns>
        public static SortedDictionary<string, string> ParmScreening()
        {
            SortedDictionary<string, string> array = new SortedDictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.Form;
            String[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                if (requestItem[i] == "CifAddr" || requestItem[i] == "CifClientId" || requestItem[i] == "CifEnName" ||
                    requestItem[i] == "CifIdExpiredDate" || requestItem[i] == "CifName" || requestItem[i] == "CifPhoneCode" ||
                    requestItem[i] == "CifPostCode" || requestItem[i] == "IdNo" ||
                    requestItem[i] == "IdType" || requestItem[i] == "OperateType")
                {
                    array.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
                }
            }
            return array;

        }


    }
}
