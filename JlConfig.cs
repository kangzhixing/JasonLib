using System;
using System.Configuration;
using System.Web;

namespace JasonLib
{
    public class JlConfig
    {

        /// <summary>
        /// 获得值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="isDecode">是否解码</param>
        /// <returns>结果</returns>
        public static T GetValue<T>(string key, bool isDecode = true)
        {
            var value = ConfigurationManager.AppSettings[key];
            var type = typeof(T);
            if (type == typeof(string) && isDecode)
            {
                value = HttpUtility.HtmlDecode(value);
            }
            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, value, true);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}
